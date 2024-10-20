using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 70f;
    [SerializeField] int playerIndex = 0;

    // Audio clips for movement and jump
    public AudioClip jumpClip;
    public AudioClip moveClip;

    Animator animator;

    DistanceJoint2D handjoint;

    private Rigidbody2D rb;
    private GroundCheck groundCheck;
    private Vector2 inputVector = Vector2.zero;
    private Transform hand;
    private AudioSource audioSource;

    // To control whether the move sound is playing or not
    private bool isMoving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.parent.GetComponentInChildren<GroundCheck>();
        handjoint = GetComponent<DistanceJoint2D>();
        hand = handjoint.connectedBody.transform;

        // Initialize the audio source component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        animator = GetComponentInChildren<Animator>();
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    public void setInputVector(Vector2 direction)
    {
        inputVector = direction;
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

            Debug.Log("Jump");

            animator.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        Vector2 moveInput = inputVector;
        moveInput.y = 0;

        // Add force for movement
        rb.AddForce(moveInput * speed, ForceMode2D.Force);

        // Play movement sound if player is moving and the sound is not already playing
        if (moveInput.x != 0)
        {
            if (!isMoving && moveClip != null)
            {
                audioSource.loop = true;
                audioSource.clip = moveClip;
                audioSource.Play();
                isMoving = true;
            }
        }
        else
        {
            // Stop movement sound when player stops moving
            if (isMoving)
            {
                audioSource.Stop();
                isMoving = false;
            }
        }

        // Hand joint constraint logic
        Vector2 handTargetDir = hand.position - transform.position;
        if (handTargetDir.magnitude > handjoint.distance - handjoint.distance * 0.1f)
        {
            hand.GetComponent<MoveHand>().constrainMovement = true;
            //hand.position = Vector2.MoveTowards(hand.position, transform.position, handjoint.distance * 0.1f);
            Debug.Log("Constraining movement");
        }
        else
        {
            hand.GetComponent<MoveHand>().constrainMovement = false;
            Debug.Log("Not constraining movement");
        }
    }

    void Update()
    {
        // FOR ANIMATION
        animator.SetFloat("Speed", Mathf.Abs(inputVector.x));
        if (inputVector.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputVector.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

