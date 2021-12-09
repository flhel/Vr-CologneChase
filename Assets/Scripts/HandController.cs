using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour {
    [SerializeField] InputActionReference controllerActionGrip;
    [SerializeField] InputActionReference controllerActionTrigger;
    private Animator _handAnimator;

    // Start is called before the first frame update
    void Awake() {
        //get all actions
        controllerActionGrip.action.performed += GripPress;
        controllerActionGrip.action.canceled += GripPress;

        controllerActionTrigger.action.performed += TriggerPress;
        controllerActionTrigger.action.canceled += TriggerPress;

        //get the animator
        _handAnimator = GetComponent<Animator>();
    }

    private void GripPress(InputAction.CallbackContext obj) {
        _handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }

    private void TriggerPress(InputAction.CallbackContext obj) {
        _handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }
}
