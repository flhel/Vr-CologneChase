using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace UnityEngine.XR.Interaction.Toolkit {
    public class PauseMenu : MonoBehaviour {
        [SerializeField] InputActionReference controllerActionMenu;
        public static bool gamePaused = false;
        private GameObject pauseMenu;
        private GameObject gameOverText;
        private GameObject playButton;
        private GameObject rightControllerRaycast;

        void Awake() {
            GameObject parent = GameObject.Find("XR Rig");
            pauseMenu = FindObjectByParent(parent, "PauseMenuCanvas");
            gameOverText = FindObjectByParent(parent, "GameOverText");
            playButton = FindObjectByParent(parent, "PlayButton");
            rightControllerRaycast = FindObjectByParent(parent, "RaycastTransformRight");
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

        public void PlayGame() {
            rightControllerRaycast.GetComponent<XRInteractorLineVisual>().enabled = false;
            pauseMenu.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1f;
        }

        public void StopGame() {
            rightControllerRaycast.GetComponent<XRInteractorLineVisual>().enabled = true;
            pauseMenu.SetActive(true);
            gamePaused = true;
            Time.timeScale = 0f;
        }

        public void GameOver() {
            // get the locomotion system and disable movement
            GameObject moveProvider = GameObject.Find("Locomotion System");
            moveProvider.GetComponent<CustomMoveProvider>().DisableMoveAll();

            rightControllerRaycast.GetComponent<XRInteractorLineVisual>().enabled = true;
            pauseMenu.SetActive(true);
            gameOverText.SetActive(true);
            playButton.SetActive(false);
            //gamePaused = true;
            //Time.timeScale = 0f;
        }

        public void MainMenu() {
            Time.timeScale = 1f;
            //Unsubscribe now unused button event 
            controllerActionMenu.action.performed -= MenuButtonPressPerformed;
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame() {
            Application.Quit();
        } 

        //Finds inactive GameObjects by the parent object
        public static GameObject FindObjectByParent(GameObject parent, string name) {
            Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
            foreach(Transform t in trs){
                if(t.name == name){
                    return t.gameObject;
                }
            }
            return null;
        }
    }
}
