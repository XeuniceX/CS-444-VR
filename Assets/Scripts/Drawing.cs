using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class AirSketchWithS : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private Material lineMaterial;
    [SerializeField] private float    lineWidth   = 0.01f;
    [SerializeField] private Color    lineColor   = Color.white;
    [SerializeField] private float    minDistance = 0.015f;

    [Header("Geometry")]
    [Tooltip("Point is cast this far in front of controller.")]
    [SerializeField] private float    drawDistance = 0.40f;

    [Header("S‑shape recognition")]
    [SerializeField] private float    mergeTimeout  = 1.0f;   // seconds between strokes
    [SerializeField] private float    matchThreshold = 0.80f; // $1 recognizer score threshold

    public static System.Action OnSRecognized;

    // ────────────────────────────────── runtime vars
    private LineRenderer               currentLine;
    private Vector3                    lastPoint;
    private bool                       isDrawing;
    private readonly List<Vector3>     strokePoints = new();  // current stroke (world)
    private readonly List<Vector3>     accumulated  = new();  // merged strokes buffer
    private float                      lastStrokeEndTime;

    private DrawingActions             inputActions;
    private DollarRecognizer           dollar;

    void Awake()
    {
        // 1) Input
        inputActions = new DrawingActions();

        // 2) $1 recognizer
        dollar = new DollarRecognizer();
        // register the original prototype "S"
        Vector2[] sTemplate = new Vector2[] {
            new Vector2(584, 191),  new Vector2(541,  153),
            new Vector2(475, 147), new Vector2(405, 157),
            new Vector2(348,216), new Vector2(317,301),
            new Vector2(333, 374), new Vector2(401, 384),
            new Vector2(459,  402), new Vector2(513,426),
            new Vector2(555, 567), new Vector2(511, 622),
            new Vector2(428, 618), new Vector2(359, 597),
            new Vector2(297, 559)
        };
        dollar.SavePattern("S", sTemplate);
    }

    void OnEnable()
    {
        inputActions.Gameplay.Enable();
        inputActions.Gameplay.TriggerPull.performed += OnTriggerPressed;
        inputActions.Gameplay.TriggerPull.canceled  += OnTriggerReleased;
    }

    void OnDisable()
    {
        inputActions.Gameplay.TriggerPull.performed -= OnTriggerPressed;
        inputActions.Gameplay.TriggerPull.canceled  -= OnTriggerReleased;
        inputActions.Gameplay.Disable();
    }

    void Update()
    {
        if (!isDrawing || currentLine == null) return;

        Vector3 p = transform.position + transform.forward * drawDistance;
        if (Vector3.Distance(p, lastPoint) >= minDistance)
        {
            AddPoint(p);
            lastPoint = p;
        }
    }

    private void OnTriggerPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() < 0.1f || isDrawing) return;
        if (Time.time - lastStrokeEndTime > mergeTimeout) accumulated.Clear();
        StartStroke();
    }

    private void OnTriggerReleased(InputAction.CallbackContext ctx)
    {
        if (!isDrawing) return;
        EndStroke();
    }

    private void StartStroke()
    {
        strokePoints.Clear();
        GameObject go = new($"Stroke_{Time.time:F1}");
        currentLine = go.AddComponent<LineRenderer>();
        currentLine.material       = lineMaterial;
        currentLine.widthCurve     = AnimationCurve.Constant(0, 1, lineWidth);
        currentLine.startColor     = currentLine.endColor = lineColor;
        currentLine.numCapVertices = 4;
        currentLine.useWorldSpace  = true;
        currentLine.positionCount  = 0;

        // 🔥 Attach fade script
        go.AddComponent<FadeAndDestroy>();

        isDrawing  = true;
        lastPoint  = transform.position + transform.forward * drawDistance;
        AddPoint(lastPoint);
    }

    private void EndStroke()
    {
        isDrawing         = false;
        lastStrokeEndTime = Time.time;
        currentLine       = null;

        // store for recognition
        accumulated.AddRange(strokePoints);

        // perform recognition
        bool isS = RecogniseS(accumulated);
        Debug.Log(isS
            ? "<color=lime>S shape recognised!</color>"
            : "<color=red>Not an S.</color>");

        // 🔥 If S detected, broadcast event
        if (isS && OnSRecognized != null)
        {
            OnSRecognized.Invoke();
        }
    }

    private void AddPoint(Vector3 worldPos)
    {
        strokePoints.Add(worldPos);
        currentLine.positionCount++;
        currentLine.SetPosition(currentLine.positionCount - 1, worldPos);
    }

    private Vector2[] ProjectTo2D(List<Vector3> raw)
    {
        var (o, xAxis, yAxis) = FitPlane(raw);
        var pts2D = new Vector2[raw.Count];
        for (int i = 0; i < raw.Count; i++)
        {
            var d = raw[i] - o;
            pts2D[i] = new Vector2(
                Vector3.Dot(d, xAxis),
                Vector3.Dot(d, yAxis)
            );
        }
        return pts2D;
    }

    /*
    private void SaveTemplatesToFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "STemplates.txt");
        var sb = new StringBuilder();
        foreach (var tpl in customTemplates)
        {
            for (int i = 0; i < tpl.Length; i++)
            {
                sb.Append(tpl[i].x.ToString("F2")).Append(',').Append(tpl[i].y.ToString("F2"));
                if (i < tpl.Length - 1) sb.Append(';');
            }
            sb.AppendLine();
        }
        File.WriteAllText(path, sb.ToString());
        Debug.Log($"Saved {customTemplates.Count} templates to {path}");
    }
    */

    private bool RecogniseS(List<Vector3> raw)
    {
        if (raw.Count < 5) return false;
        var pts2D = ProjectTo2D(raw);
        var res   = dollar.Recognize(pts2D);
        return res.Match != null
            && res.Match.Name  == "S"
            && res.Score       >= matchThreshold;
    }

    static Vector3 Centroid(IReadOnlyList<Vector3> pts)
    {
        Vector3 sum = Vector3.zero;
        foreach (var p in pts) sum += p;
        return sum / pts.Count;
    }

    static (Vector3 o, Vector3 x, Vector3 y) FitPlane(IReadOnlyList<Vector3> pts)
    {
        Vector3 o = Centroid(pts), n = Vector3.zero;
        for (int i = 0; i < pts.Count; i++)
        {
            var p0 = pts[i] - o;
            var p1 = pts[(i + 1) % pts.Count] - o;
            n.x += (p0.y - p1.y) * (p0.z + p1.z);
            n.y += (p0.z - p1.z) * (p0.x + p1.x);
            n.z += (p0.x - p1.x) * (p0.y + p1.y);
        }
        if (n.sqrMagnitude < 1e-6f) n = Vector3.up;
        n.Normalize();
        var xAxis = Vector3.Cross(
            Mathf.Abs(n.y) < 0.99f ? Vector3.up : Vector3.right,
            n
        ).normalized;
        var yAxis = Vector3.Cross(n, xAxis).normalized;
        return (o, xAxis, yAxis);
    }
}
