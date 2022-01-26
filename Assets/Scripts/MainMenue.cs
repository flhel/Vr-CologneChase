using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayTutorial(){
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
