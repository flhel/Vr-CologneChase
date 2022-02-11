using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
     public void LoadGame() {
        SceneManager.LoadScene("Game");
    }
}
