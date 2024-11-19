using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 70f;
    [SerializeField] int playerIndex = 0;

    // Audio clips for movement and jump
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip moveClip;

    Animator animator;

    DistanceJoint2D handjoint;

    Rigidbody2D rb;
    new Collider2D collider;
    GroundCheck groundCheck;
    Vector2 moveInput;
    Transform hand;
    HandMover handMover;
    AudioSource audioSource;
    Vector3 initialScale;
    public PhysicsMaterial2D slipperyMaterial, highFriction;

    bool IsMoveInput => Mathf.Abs(moveInput.x) > .1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        groundCheck = transform.parent.GetComponentInChildren<GroundCheck>();
        handjoint = GetComponent<DistanceJoint2D>();
        hand = handjoint.connectedBody.transform;
        handMover = hand.GetComponent<HandMover>();

        // Initialize the audio source component
        if (TryGetComponent<AudioSource>(out _) == false)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        animator = GetComponentInChildren<Animator>();

        initialScale = transform.localScale;
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    public void setInputVector(Vector2 direction)
    {
        moveInput = direction;
    }

    public void Jump()
    {
        if (groundCheck.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Play the jump sound if the audio clip is assigned
            if (jumpClip != null)
            {
                audioSource.PlayOneShot(jumpClip);
            }

            animator.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        // Add force for movement
        rb.AddForce(moveInput.x * speed * Vector2.right, ForceMode2D.Force);

        // Play movement sound if player is moving and the sound is not already playing
        // if (moveInput.x != 0)
        // {
        //     if (!isMoving && moveClip != null)
        //     {
        //         audioSource.loop = true;
        //         audioSource.clip = moveClip;
        //         audioSource.Play();
        //         isMoving = true;
        //     }
        // }
        // else
        // {
        //     // Stop movement sound when player stops moving
        //     if (isMoving)
        //     {
        //         audioSource.Stop();
        //         isMoving = false;
        //     }
        // }

        // Hand joint constraint logic
        Vector2 handTargetDir = hand.position - transform.position;
        bool constrainHands = handTargetDir.magnitude > handjoint.distance * .9f;
        handMover.constrainMovement = constrainHands;

        bool beSlippery = !groundCheck.isGrounded || IsMoveInput;
        collider.sharedMaterial = beSlippery ? slipperyMaterial : highFriction;
    }

    void Update()
    {
        // FOR ANIMATION
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        if (moveInput.x > 0)
        {
            Vector3 theScale = transform.localScale;
            transform.localScale = new Vector3(initialScale.x, theScale.y, theScale.z);
        }
        else if (moveInput.x < 0)
        {
            Vector3 theScale = transform.localScale;
            transform.localScale = new Vector3(-initialScale.x, theScale.y, theScale.z);
        }
    }
}

