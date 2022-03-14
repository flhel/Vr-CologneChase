using UnityEngine;

// This script manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class DontDestroy : MonoBehaviour {
    void Awake() {
        GameObject obj = GameObject.Find("Spray Noise");
        DontDestroyOnLoad(obj);
    }
}
