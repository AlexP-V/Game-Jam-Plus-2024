using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public float jumpForce = 70f;
    PlayerInput input;
    [SerializeField] PlayerInputActions playerControls;
    InputAction move;
    InputAction jump;
    Rigidbody2D rb;

    Vector2 moveInput;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        jump = playerControls.Player.Fire;
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
        Vector2 movement = move.ReadValue<Vector2>();
        movement.y = 0;
        rb.AddForce(moveInput * speed, ForceMode2D.Force);
    }

    void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump");
    }
}