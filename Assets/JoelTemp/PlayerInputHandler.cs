using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput playerInput;
    NewPlayerMovement playerMovement;
    MoveHand handMover;
    ClimbingHand climbingHand;
   
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerMovers = FindObjectsOfType<NewPlayerMovement>();
        var index = playerInput.playerIndex;
        playerMovement = playerMovers.FirstOrDefault(mover => mover.getPlayerIndex() == index);

        var handMovers = FindObjectsOfType<MoveHand>();
        handMover = handMovers.FirstOrDefault(mover => mover.getPlayerIndex() == index);

        var climbingHands = FindObjectsOfType<ClimbingHand>();
        climbingHand = climbingHands.FirstOrDefault(mover => mover.getPlayerIndex() == index);
    }

    public void OnJumpInput(CallbackContext context)
    {
        playerMovement.Jump();
    }

    public void OnPlayerMoveInput(CallbackContext context)
    {
        Debug.Log("Setting Input Vector to: " + context.ReadValue<Vector2>());
        playerMovement.setInputVector(context.ReadValue<Vector2>());
    }

    public void OnHandMoveInput(CallbackContext context)
    {
        Debug.Log("Setting Target Dir to: " + context.ReadValue<Vector2>());
        handMover.SetTargetDir(context.ReadValue<Vector2>());
    }    

    public void OnClimbInput(CallbackContext context)
    {
        climbingHand.SetClimbValue(context.ReadValue<float>());
    }
}
