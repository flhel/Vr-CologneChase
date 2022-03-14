using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class FaxrScript : MonoBehaviour
{
    public AudioSource audioSource; 
    public AIPath aiPath;
    public Animator animator;

    void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        PlayRunningSound();
    }

    void Update() {
        if(aiPath.reachedEndOfPath) {
            animator.SetBool("IsOnPhone", true);
        }
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void PlayRunningSound(){
        audioSource.Play();
    }
}
