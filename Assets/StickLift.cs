using UnityEngine;

public class StickLift : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private HandMover player1HandMover; // HandMover for Player 1
    [SerializeField] private GroundCheck player1GroundCheck; // GroundCheck for Player 1
    [SerializeField] private PickUpObject player1PickUp; // PickUpObject for Player 1

    [Header("Player 2")]
    [SerializeField] private HandMover player2HandMover; // HandMover for Player 2
    [SerializeField] private GroundCheck player2GroundCheck; // GroundCheck for Player 2
    [SerializeField] private PickUpObject player2PickUp; // PickUpObject for Player 2

    private Rigidbody2D rb;
    private Vector2 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player1HandMover == null || player1GroundCheck == null || player1PickUp == null)
        {
            Debug.LogError("Player 1 references are not set on StickLift script.");
        }

        if (player2HandMover == null || player2GroundCheck == null || player2PickUp == null)
        {
            Debug.LogError("Player 2 references are not set on StickLift script.");
        }
    }

    void FixedUpdate()
    {
        // Check if both players are holding the stick
        bool bothPlayersHolding = player1PickUp.IsHolding && player2PickUp.IsHolding;

        if (bothPlayersHolding)
        {
            bool player1IsGrounded = player1GroundCheck != null && player1GroundCheck.isGrounded;
            bool player2IsGrounded = player2GroundCheck != null && player2GroundCheck.isGrounded;

            if (player1IsGrounded && !player2IsGrounded)
            {
                // Player 1 is the only one grounded
                moveDir = player1HandMover.GetMoveDir();
                rb.velocity = moveDir * player1HandMover.sens * 4;
            }
            else if (player2IsGrounded && !player1IsGrounded)
            {
                // Player 2 is the only one grounded
                moveDir = player2HandMover.GetMoveDir();
                rb.velocity = moveDir * player2HandMover.sens * 4;
            }
        }
    }
}






