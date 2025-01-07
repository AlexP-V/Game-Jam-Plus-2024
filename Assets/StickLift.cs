using UnityEngine;

public class StickLift : MonoBehaviour
{
    [SerializeField] private HandMover handMover; // Reference to the HandMover script
    [SerializeField] private bool isGrounded = false; // Variable to track if the player is grounded

    private Rigidbody2D rb;
    private Vector2 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (handMover == null)
        {
            Debug.LogError("HandMover reference is not set on StickLift script.");
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && handMover != null)
        {
            // Get the moveDir from the HandMover script
            moveDir = handMover.GetMoveDir();

            // Apply velocity to move the stick using the hand's moveDir
            rb.velocity = moveDir * handMover.sens * 4;
        }
    }
}




