using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace UnityEngine.XR.Interaction.Toolkit {
    public class PauseMenu : MonoBehaviour {
        [SerializeField] InputActionReference controllerActionMenu;
        public static bool gamePaused = false;
        private GameObject pauseMenu;
        private GameObject gameOverText;
        private GameObject playButton;

        void Awake() {
            pauseMenu = GameObject.Find("PauseMenuCanvas");
            gameOverText = GameObject.Find("GameOverText");
            playButton = GameObject.Find("PlayButton");
            controllerActionMenu.action.performed += MenuButtonPressPerformed;
        }

        private void MenuButtonPressPerformed(InputAction.CallbackContext obj) {
            gameOverText.SetActive(false);
            if(gamePaused) {
                PlayGame();
            } else {
                StopGame(); 
            }          
        }   

        public void PlayGame(){
            pauseMenu.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1f;
        }

        public void StopGame(){
            pauseMenu.SetActive(true);
            gamePaused = true;
            Time.timeScale = 0f;
        }

        public void GameOver(){
            pauseMenu.SetActive(true);
            gameOverText.SetActive(true);
            playButton.SetActive(false);
            gamePaused = true;
            Time.timeScale = 0f;
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
