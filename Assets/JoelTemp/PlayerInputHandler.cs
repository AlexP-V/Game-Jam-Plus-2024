using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    HandMover handMover;
    ClimbingHand climbingHand;
    PickUpObject pickUpObject;
   
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerMovers = FindObjectsOfType<PlayerMovement>();
        var index = playerInput.playerIndex;
        playerMovement = playerMovers.FirstOrDefault(mover => mover.getPlayerIndex() == index);

        var handMovers = FindObjectsOfType<HandMover>();
        handMover = handMovers.FirstOrDefault(mover => mover.getPlayerIndex() == index);

        var climbingHands = FindObjectsOfType<ClimbingHand>();
        climbingHand = climbingHands.FirstOrDefault(mover => mover.getPlayerIndex() == index);

        var objectPickers = FindObjectsOfType<PickUpObject>();
        pickUpObject = objectPickers.FirstOrDefault(pickUpObject => pickUpObject.getPlayerIndex() == index);
    }

    public void OnJumpInput(CallbackContext context)
    {
        playerMovement.Jump();
    }

    public void OnPlayerMoveInput(CallbackContext context)
    {
        playerMovement.setInputVector(context.ReadValue<Vector2>());
    }

    public void OnHandMoveInput(CallbackContext context)
    {
        handMover.SetTargetDir(context.ReadValue<Vector2>());
    }    

    public void OnGrabInput(CallbackContext context)
    {
        Debug.Log("Grab input!");
        pickUpObject.SetGrabInput();
    }

    public void OnClimbInput(CallbackContext context)
    {
        climbingHand.SetClimbValue(context.ReadValue<float>());
    }

    public void OnRotateObjectInput(CallbackContext context)
    {
        climbingHand.setRotationValue(context.ReadValue<float>());
    }
}
