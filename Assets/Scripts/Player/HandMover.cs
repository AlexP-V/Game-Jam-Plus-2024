using UnityEngine;

public class HandMover : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    [HideInInspector] public float sens;

    [SerializeField] private float pushForce = 10f; // Magnitude of the push force
    [SerializeField] private float HandMulti = 10f;    

    [SerializeField] private GroundCheck playerGroundCheck; // GroundCheck for Player 
    [SerializeField] private PickUpObject playerPickUp; // PickUpObject for Player 

    [SerializeField] private GroundCheck friendGroundCheck;
    [SerializeField] private PickUpObject friendPickUp;

    [SerializeField] StickGroundCheck stickGroundCheck;


    private Rigidbody2D rb;
    private Vector3 handTarget;
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private Transform body; // Transform of the body object
    [SerializeField] private Rigidbody2D bodyRb; // Rigidbody2D of the body object

    public float rotationOffset = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
        ApplySettings();
    }

    void ApplySettings()
    {
        sens = settings.handSensitivity;
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    public void SetTargetDir(Vector3 newTarget)
    {
        handTarget = newTarget;
    }

    public Vector2 getTargetDir()
    {
        return handTarget;
    }

    public Vector2 GetMoveDir()
    {
        // Compute moveDir as in FixedUpdate
        return -rb.position + (Vector2)(handTarget + body.position);
    }

    void FixedUpdate()
    {
        ApplySettings();

        Vector2 moveDir = GetMoveDir();
        rb.velocity = moveDir * sens;
        //if (playerGroundCheck.isGrounded)
        //{           
        //    rb.velocity = moveDir * sens;
        //}
        //else if (playerPickUp.IsHolding)
        //{
        //    // Calculate movement force
        //    Vector2 movementForce = moveDir * sens;

        //    // Calculate gravity effect on velocity
        //    Vector2 gravityVelocity = rb.velocity + Physics2D.gravity * bodyRb.gravityScale * Time.fixedDeltaTime;

        //    // Combine movement and gravity
        //    rb.velocity = new Vector2(movementForce.x, gravityVelocity.y);
        //}

        bool playerIsGrounded = playerGroundCheck != null && playerGroundCheck.isGrounded;

        bool friendIsGrounded = friendGroundCheck != null && friendGroundCheck.isGrounded;

        bool PlayersHolding = playerPickUp.IsHolding;

        bool bothPlayersHolding = playerPickUp.IsHolding && friendPickUp.IsHolding;

        bool stickIsGrounded = (stickGroundCheck != null && stickGroundCheck.isGrounded);

        if (bothPlayersHolding && friendIsGrounded || stickIsGrounded)
        {
            if (PlayersHolding && !playerIsGrounded)
            {
                // Define a threshold for "full input"
                float inputThreshold = 0.9f; // Assuming 1.0 is the maximum input magnitude

                // Check if the input magnitude is at or near maximum
                if (handTarget.magnitude >= inputThreshold)
                {
                    // Calculate the force direction: opposite of the hand target direction
                    Vector2 forceDirection = -(Vector2)handTarget.normalized;

                    // Apply force to the body's Rigidbody2D
                    if (bodyRb != null)
                    {
                        bodyRb.AddForce(forceDirection * pushForce, ForceMode2D.Force);
                    }

                    // Force for the hand in the same direction as the input
                    Vector2 handForceDirection = (Vector2)handTarget.normalized;
                    rb.AddForce(handForceDirection * pushForce * HandMulti, ForceMode2D.Force);
                }

            }
        }
    }

    void RotateInDirection(Transform transform, Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
    }

    void Update()
    {
        if (handTarget != Vector3.zero)
            RotateInDirection(transform, handTarget);
    }
}

