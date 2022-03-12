using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class CustomMoveProvider : ContinuousMoveProviderBase
    {
        [SerializeField]
        [Tooltip("The Input System Action that will be used to read Move data from the left hand controller. A Vector3 Control eg. Acceleration will be converted to a Vector2 Control.")]
        public InputActionProperty m_LeftHandMoveAction;
        /// <summary>
        /// The Input System Action that Unity uses to read Move data from the left hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector3Control"/> Control.
        /// </summary>
        public InputActionProperty leftHandMoveAction
        {
            get => m_LeftHandMoveAction;
            set => SetInputActionProperty(ref m_LeftHandMoveAction, value);
        }

        [SerializeField]
        [Tooltip("The Input System Action that will be used to read Move data from the right hand controller. A Vector3 Control eg. Acceleration will be converted to a Vector2 Control.")]
        public InputActionProperty m_RightHandMoveAction;
        /// <summary>
        /// The Input System Action that Unity uses to read Move data from the right hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector3Control"/> Control.
        /// </summary>
        public InputActionProperty rightHandMoveAction
        {
            get => m_RightHandMoveAction;
            set => SetInputActionProperty(ref m_RightHandMoveAction, value);
        }

        private int controllerLeftId = -1;
        private int controllerRightId = -1;

        // Is managed through HandController.cs
        private bool moveLeftEnabled = false;
        private bool moveRightEnabled = false;

        //The activation of the controller movement value readings is split between left and right
        public void EnableMove(int deviceId)
        {      
            //Initialising the controller(l/r) ids
            if(controllerLeftId < 0) { // && controllerRightId < 0
                 controllerLeftId = m_LeftHandMoveAction.action.activeControl.device.deviceId;
                 controllerRightId = m_RightHandMoveAction.action.activeControl.device.deviceId;
            }
            //Setting status enabled for left
            if(controllerLeftId == deviceId){
                moveLeftEnabled = true;
            }
            //Setting status enabled for right
            if(controllerRightId == deviceId){
                moveRightEnabled = true;
            }  
        }

        public void DisableMove(int deviceId)
        {
             if(controllerLeftId == deviceId){
                moveLeftEnabled = false;
            }   
            if(controllerRightId == deviceId){
                moveRightEnabled = false;
            }
        }

        // z value of vector3 will be dropped in the conversion to vector2 
        // Input should be the controller acceleration this results in smoother movement than the velocity
        protected override Vector2 ReadInput()
        {
            Vector2 moveForce = Vector2.zero;

            // Add movement forces of both hands
            if(moveLeftEnabled) {
                var leftHandValue = m_LeftHandMoveAction.action?.ReadValue<Vector3>() ?? Vector3.zero;
                
                if(leftHandValue.y < 0) {
                    leftHandValue.y = leftHandValue.y * -1; // negative acceleration should still result in forward movement
                } 
                
                moveForce.y = moveForce.y + leftHandValue.y;
            } 
            if(moveRightEnabled) {
                var rightHandValue = m_RightHandMoveAction.action?.ReadValue<Vector3>() ?? Vector3.zero;
                
                if(rightHandValue.y < 0) {
                    rightHandValue.y = rightHandValue.y * -1; // negative acceleration should still result in forward movement
                }  
                
                moveForce.y = moveForce.y + rightHandValue.y;
            }     
            
            // Attention order!!!
            // manages movement speed relative to real arm movement 
            Debug.Log(moveForce);

            if(moveForce.y < 1) {
                return Vector2.zero;
            }
            if(moveForce.y < 8) {
                return moveForce / 14;
            }
            if(moveForce.y < 10) {
                return moveForce / 9;
            }
            if(moveForce.y < 12) {
                return moveForce / 5;
            }
            if(moveForce.y < 15) {
                return moveForce / 2;
            }
            return moveForce;
        }

        void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
        {
            if (Application.isPlaying)
                property.DisableDirectAction();

            property = value;

            if (Application.isPlaying && isActiveAndEnabled)
                property.EnableDirectAction();
        }
    }
}
