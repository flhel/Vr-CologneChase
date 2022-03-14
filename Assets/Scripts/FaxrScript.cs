using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

namespace UnityEngine.XR.Interaction.Toolkit {
    public class FaxrScript : MonoBehaviour {
        public AudioSource audioSource; 
        public AIPath aiPath;
        public Animator animator;
        public PauseMenu pauseMenu;

        void Start() {
            aiPath = GetComponentInParent<AIPath>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            GameObject pauseMenuObject = GameObject.Find("PauseMenuContainer");
            pauseMenu = pauseMenuObject.GetComponent<PauseMenu>();
            pauseMenu.PlayGame();
            audioSource.Play();
        }

        void Update() {
            if(aiPath.reachedEndOfPath) {
                animator.SetBool("IsOnPhone", true);
                audioSource.Stop();
                pauseMenu.GameOver();
            }
        }

        public void LoadMainMenu() {
            SceneManager.LoadScene("Menu");
        }
    }
}
