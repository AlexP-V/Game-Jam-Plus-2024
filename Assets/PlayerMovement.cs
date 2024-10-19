using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public float jumpForce = 70f;
    [SerializeField] PlayerInputActions playerControls;
    InputAction move;
    InputAction jump;
    Rigidbody2D rb;

    void Awake()
    {
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
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
        float moveHorizontal = move.ReadValue<Vector2>().x;

        Vector2 movement = new Vector2(moveHorizontal, 0f);
        rb.AddForce(movement * speed, ForceMode2D.Force);
    }

    void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump");
    }
}