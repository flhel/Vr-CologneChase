using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class FaxrScript : MonoBehaviour
{
    public AudioSource audioSource; 
    public Seeker seeker;

    public Path path;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        audioSource = GetComponent<AudioSource>();
        PlayRunningSound();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void PlayRunningSound(){
        audioSource.Play();
    }
}
