using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace UnityEngine.XR.Interaction.Toolkit {
    public class PauseMenu : MonoBehaviour {
        [SerializeField] InputActionReference controllerActionMenuButton;
        public static bool gamePaused = false;
        private GameObject pauseMenu;

        void Awake() {
            pauseMenu = GameObject.Find("PauseMenuContainer");
            pauseMenu.SetActive(false);
            controllerActionMenuButton.action.performed += MenuButtonPressPerformed;
        }

        private void MenuButtonPressPerformed(InputAction.CallbackContext obj) {
            if(gamePaused) {
                PlayGame();
            } else {
                pauseMenu.SetActive(true);
                gamePaused = true;
                Time.timeScale = 0f;
            }          
        }   

        public void PlayGame(){
            pauseMenu.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1f;
        }

        public void MainMenu(){
            PlayGame();
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame(){
            Application.Quit();
        } 
    }
}
