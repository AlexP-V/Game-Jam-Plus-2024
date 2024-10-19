using UnityEngine;

public class P1Movement : MonoBehaviour


{
    // Public variables that you can adjust in the Unity Inspector
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 10f; // Jump force (editable in the Inspector)

    // Private variables
    private Rigidbody2D rb;
    private bool isGrounded = false; // To check if the player is grounded

    // Ground check variables
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

   
    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle movement for Player 1 (using A, D for left/right, and W for jumping)
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f; // Move right
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jumping with W key
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply a force upwards using the jumpForce variable
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded by casting a small circle at the player's feet
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}
