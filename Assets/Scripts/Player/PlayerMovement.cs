using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    public float speed = 10f;
    public float jumpForce = 70f;
    [SerializeField] int playerIndex = 0;

    // Audio clips for movement and jump
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip moveClip;

    Animator animator;    

    Rigidbody2D rb;
    Collider2D playerCollider;
    PhysicsMaterial2D playerMaterial;
    [SerializeField] PhysicsMaterial2D slipperyMaterial;
    GroundCheck groundCheck;
    Vector2 moveInput;
   
    AudioSource audioSource;
    Vector3 initialScale;

    bool IsMoveInput => Mathf.Abs(moveInput.x) > .1f;

    void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerMaterial = playerCollider.sharedMaterial;
        groundCheck = transform.parent.GetComponentInChildren<GroundCheck>(); 

        ApplySettings();       

        // Initialize the audio source component
        if (TryGetComponent<AudioSource>(out _) == false)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        animator = GetComponentInChildren<Animator>();
       

        initialScale = transform.localScale;
    }

    void ApplySettings()
    {
        speed = settings.movementSpeed;
        jumpForce = settings.jumpForce;
        rb.mass = settings.mass;
        rb.gravityScale = settings.gravityScale;
        rb.drag = settings.airResistance;
        if (playerCollider.sharedMaterial == playerMaterial)
            playerCollider.sharedMaterial.friction = settings.friction;
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
            jumpForce = settings.jumpForce;

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
        ApplySettings(); 

        // Add force for movement
        rb.AddForce(moveInput.x * speed * Vector2.right, ForceMode2D.Force);

        bool beSlippery = !groundCheck.isGrounded || IsMoveInput;
        playerCollider.sharedMaterial = beSlippery ? slipperyMaterial : playerMaterial;
    }

    void Update()
    {
        //For animation
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
       /* if (moveInput.x > 0)
        {
            Vector3 theScale = transform.localScale;
            transform.localScale = new Vector3(initialScale.x, theScale.y, theScale.z);
        }
        else if (moveInput.x < 0)
        {
            Vector3 theScale = transform.localScale;
            transform.localScale = new Vector3(-initialScale.x, theScale.y, theScale.z);
        }
        */     
    }
}

