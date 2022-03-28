using UnityEngine;

public class DontDestroy : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(GameObject.Find("Spray Noise"));
    }
}
