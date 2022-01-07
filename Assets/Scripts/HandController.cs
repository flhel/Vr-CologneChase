using UnityEngine.InputSystem;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class HandController : MonoBehaviour {
            
        [SerializeField] InputActionReference controllerActionGrip;
        [SerializeField] InputActionReference controllerActionTrigger;
        private Animator _handAnimator;

        
        public CustomMoveProvider p;

        void Awake() {
            //get all input actions
            controllerActionGrip.action.performed += GripPressPerformed;
            controllerActionGrip.action.canceled += GripPressCanceled;

            controllerActionTrigger.action.performed += TriggerPressPerformed;
            controllerActionTrigger.action.canceled += TriggerPressCanceled;

            //get the animator
            _handAnimator = GetComponent<Animator>();

            //get the locomotion system
            GameObject moveProvider = GameObject.Find("Locomotion System");
            p  = moveProvider.GetComponent<CustomMoveProvider>();
        }

        //Grip input actions
        private void GripPressPerformed(InputAction.CallbackContext obj) {
            SetGripAnimator(obj);
        }

        private void GripPressCanceled(InputAction.CallbackContext obj) {
            SetGripAnimator(obj);
        }

        private void SetGripAnimator(InputAction.CallbackContext obj) {
            _handAnimator.SetFloat("Grip", obj.ReadValue<float>());
        }

        //Trigger input actions
        private void TriggerPressPerformed(InputAction.CallbackContext obj) {
            SetTriggerAnimator(obj);
            p.EnableMove(obj.action.activeControl.device.deviceId);
        }

        private void TriggerPressCanceled(InputAction.CallbackContext obj) {
            SetTriggerAnimator(obj);
            p.DisableMove(obj.action.activeControl.device.deviceId);
        }

        private void SetTriggerAnimator(InputAction.CallbackContext obj) {
            _handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
        }
    }
}

