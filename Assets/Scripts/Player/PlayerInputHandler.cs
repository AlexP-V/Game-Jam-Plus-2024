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
        PlayerMovement[] playerMovers = FindObjectsOfType<PlayerMovement>();
        int index = playerInput.playerIndex;
        playerMovement = playerMovers.FirstOrDefault(mover => mover.getPlayerIndex() == index);

        Transform player = playerMovement.transform.parent;

        handMover = player.GetComponentInChildren<HandMover>();

        climbingHand = player.GetComponentInChildren<ClimbingHand>();

        pickUpObject = player.GetComponentInChildren<PickUpObject>();
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
