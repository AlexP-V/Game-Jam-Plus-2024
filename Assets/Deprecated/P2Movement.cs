using UnityEngine;

public class P2Movement : MonoBehaviour
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
        // Handle movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
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

