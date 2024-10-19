using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour {

    public int inputID;
    public float speed = 10f;
    public float jumpForce = 70f;
    [SerializeField] PlayerInputActions playerControls;
    InputAction move;
    InputAction jump;
    Rigidbody2D rb;

    Vector2 moveInput;

    void Awake()
    {
        
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    void FixedUpdate()
    {
        Vector2 moveInput = Gamepad.all[inputID].leftStick.value;
        moveInput.y = 0;
        rb.AddForce(moveInput * speed, ForceMode2D.Force);
    }

    void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump");
    }
}