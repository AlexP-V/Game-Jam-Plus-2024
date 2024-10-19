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
   
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerMovers = FindObjectsOfType<NewPlayerMovement>();
        var index = playerInput.playerIndex;
        playerMovement = playerMovers.FirstOrDefault(mover => mover.getPlayerIndex() == index);
    }

    public void OnJumpInput(CallbackContext context)
    {
        playerMovement.Jump();
    }

    public void OnMoveInput(CallbackContext context)
    {
        Debug.Log("Setting Input Vector to: " + context.ReadValue<Vector2>());
        playerMovement.setInputVector(context.ReadValue<Vector2>());
    }

    
}
