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
- **Platform**: Oculus Quest (Standalone)
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

### 3D Models

### Sound & Audio

### Tutorials & References
- **Gesture Recognition ($1 Recognizer)** – [University of Washington $1 Recognizer](https://depts.washington.edu/madlab/proj/dollar/)  
- **Swimming Mechanic** – Inspired by VR velocity-based motion tutorials from [Valem](https://www.youtube.com/c/ValemVR)  
- **Mirror Vision Logic** – Based on Unity's [`GeometryUtility.CalculateFrustumPlanes`](https://docs.unity3d.com/ScriptReference/GeometryUtility.CalculateFrustumPlanes.html) and dot product visibility math

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

## 📜 License

This project is for academic and non-commercial use only. All credited assets retain their original licenses as per their sources.

---

## 🙏 Acknowledgements

Thanks to our instructors, playtesters, and the open-source and asset community that made this project possible.

---

