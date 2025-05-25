# Phantom Echo 🎮

**Phantom Echo** is a psychological horror VR game built in Unity. In this immersive experience, players navigate a nightmarish mansion as a schizophrenic protagonist haunted by disembodied voices and fragmented memories. Monsters lurk only in reflective surfaces, and survival depends on mastering physical gestures, managing sanity, and escaping a reality that breaks with every mirror.

---

## 📌 Features

- 🪞 **Mirror-Based Monsters**  
  Enemies are only visible in mirrors, reacting to what the reflection sees—not the player directly.

- 🧠 **Sanity System**  
  A sanity meter, viewable only in mirrors, drains over time or when monsters appear. Pills help restore balance.

- ✍️ **Gesture-Based Combat**  
  Draw an “S” midair using the controller trigger to dispel monsters. Gesture detection uses a 3D-adapted $1 recognizer.

- 🛠️ **Physical Crafting**  
  Combine and shake object parts in-world to create key items (e.g., screwdriver for progression).

- 🏊 **Intuitive Swimming Controls**  
  Enter a pool zone and swim using realistic breaststroke-like motion with VR controllers.

- 🔄 **XR Interaction Toolkit (XRIT)**  
  Core VR interaction features like teleportation, continuous movement, climbing, and object manipulation.

---

## 🛠️ Technologies

- **Engine**: Unity 2022.3 (URP)
- **Platform**: Oculus Quest 1 (Standalone)
- **Frameworks**: Unity XR Interaction Toolkit (v2.3)
- **Gesture Recognition**: Adapted $1 Unistroke Recognizer
- **Physics**: Rigidbody-based custom movement for swimming and interaction

---

## 🚀 Getting Started

### Prerequisites
- Unity 2022.3 or later
- XR Plugin Management enabled
- Oculus SDK installed and configured

### Installation
1. Clone the repository.
2. Open the project in Unity Hub.
3. Connect an Oculus Quest via Link or build to device.
4. Load the `TutorialScene` to begin testing.

---

## 📦 Asset Credits

### 🧱 3D Models

- **Mirror-Based Monster Animation**  
  [Maynard-Mixamo by Vladislav Cvijovic (ArtStation)](https://www.artstation.com/artwork/q9AWRy)  
  Used for the mirror monster character and animation behavior.

- **Bathtub**  
  [Bathtub by talgat92 (CGTrader)](https://www.cgtrader.com/free-3d-models/interior/bathroom/bathtub-7be0c48a-30c2-4398-96aa-e02c888b4c48)  
  Used in the bathroom scene layout.

- **Bathroom Basin & Mixer Tap**  
  [Cooke and Lewis Bathroom Set by anthony-baker1979 (CGTrader)](https://www.cgtrader.com/free-3d-models/interior/bathroom/cooke-and-lewis-bathroom-basins-and-mixer-tap)

- **Oxygen Tank**
  [Oxygen Tank by Ketshop (CGTrader)](https://www.cgtrader.com/free-3d-models/industrial/tool/oxygen-tank-62f1b728-5114-4714-86f7-fea2e52021d1)

- **Red Pill Prop**  
  [Medical Pills by vals3d (CGTrader)](https://www.cgtrader.com/free-3d-models/science/medical/medical-pills-72b0db89-be9c-4253-b11c-62393c914dc7)  
  Used for sanity recovery interaction.

- **Worn Key Prop**  
  [Worn Key by MrWolf (CGTrader)](https://www.cgtrader.com/free-3d-models/household/other/worn-key-730dba2058ea7346f8855d825410d1c7)

- **Kitchen & Living Room Assets**  
  Various furniture and props sourced using the [BlenderKit Plugin](https://www.blenderkit.com/asset-gallery?query=category_subtree:model%20order:-created)

---

### 🔊 Sound & Audio Effects

- **“Hello” Voice Line**  
  Extracted from [audioadventure90 (YouTube Shorts)](https://www.youtube.com/shorts/h-rBf88Jhdw)

- **Creepy Male Laughter**  
  Extracted from [afroninjamedia (YouTube Shorts)](https://www.youtube.com/shorts/lA401IpD3Qs)

- **Creepy Girl Laughter**  
  Extracted from [LARASHORRORSOUNDS (YouTube Shorts)](https://www.youtube.com/shorts/pOyul7ASRt0)

- **“I See You” Voice Clip**  
  Extracted from [Sound Effect Database (YouTube)](https://www.youtube.com/watch?v=pUDFTYkk0rk)

- **Splash Sound Effect**  
  Extracted from [QualityControlledInstrumentals (YouTube)](https://www.youtube.com/watch?v=bpBLyDZRJDU)

- **Eating Sound Effect**  
  Downloaded from [Pixabay – Eating Sound](https://pixabay.com/sound-effects/eating-sound-effect-36186/)

- **Carrying Sound Effect**  
  Downloaded from [Pixabay – Carry Sound](https://pixabay.com/sound-effects/sound-effect-006-backpack-handling-278069/)
---

### 📚 Tutorials & References

- **Gesture Recognition ($1 Unistroke Recognizer)**  
  [University of Washington – $1 Recognizer](https://depts.washington.edu/madlab/proj/dollar/)  
  Used to detect drawn “S” gestures in 3D via 2D projection.

- **Swimming Mechanic**  
  Inspired by [Justin P Barnett – Swimming in VR](https://www.youtube.com/watch?v=ViQzKZvYdgE)  
  Custom implementation based on controller velocity.

- **Mirror FOV Logic**  
  Based on Unity’s [`GeometryUtility.CalculateFrustumPlanes`](https://docs.unity3d.com/ScriptReference/GeometryUtility.CalculateFrustumPlanes.html) with dot product checks for precise mirror visibility detection.

- **Tutorial Start Menu**  
  Inspired by [Valem – XR VR Start Menu Tutorial](https://www.youtube.com/watch?v=6WfowlZ51i8&t=323s)

- **Mirror Setup**  
  Inspired by [BudGames – Mirror Tutorial in Unity](https://www.youtube.com/watch?v=3OkcTkdgC5A)


---

## 👨‍👩‍👧‍👦 Team

| Name                    |
|-------------------------|
| **Chan Hin Wai Howell** |
| **Lee Ern Qi Eunice**   |
| **Wu Yifan**            |

---

## 🧪 Playtesting Improvements

- Added a **tutorial scene** with instructional TV screens showing real-life videos of controller usage
- Clarified gesture and crafting mechanics using visual and particle feedback
- Refined pool entry logic and movement cooldowns for realism

---

## 🙏 Acknowledgements

Thanks to our instructors, playtesters, and the open-source and asset community that made this project possible.

---

