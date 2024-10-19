using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public float jumpForce = 70f;
    public string horizontalAxis;
    [SerializeField] KeyCode jumpKey;
    [SerializeField] PlayerInputActions playerControls;
    Rigidbody2D rb;
    InputAction move;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerControls = new PlayerInputActions();
    }
    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(horizontalAxis);

        Vector2 movement = new Vector2(moveHorizontal, 0f);
        rb.AddForce(movement * speed, ForceMode2D.Force);
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}