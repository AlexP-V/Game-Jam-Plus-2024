using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    HingeJoint2D hinge;
    ClimbingHand climbingHand;
    Rigidbody2D rigidBody;

    // Audio clips for pick up and drop
    public AudioClip pickUpClip;
    public AudioClip dropClip;
    private AudioSource audioSource;

    bool IsHolding => hinge.connectedBody != null;
    bool IsGrabInput = false;

    [SerializeField] int playerIndex = 0;

    GroundCheck groundCheck;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        climbingHand = GetComponent<ClimbingHand>();
        hinge.enabled = false;
        rigidBody = GetComponent<Rigidbody2D>();
        // Initialize the AudioSource component
        if (TryGetComponent<AudioSource>(out _) == false)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        groundCheck = transform.parent.GetComponentInChildren<GroundCheck>();

    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }   

    public void SetGrabInput()
    {
        IsGrabInput = !IsGrabInput;

        if (!IsGrabInput)
        {
            if (IsHolding)
            {
                audioSource.PlayOneShot(dropClip);
            }
            SetHold(null);  // Drop the object if the grab input is released
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (IsGrabInput && !IsHolding && other.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            SetHold(rb);  // Pick up the object
        }
    }

    void SetHold(Rigidbody2D rb)
    {
        bool isPickingUp = rb != null;

        // Play pickup sound when holding starts, drop sound when holding stops
        if (isPickingUp)
        {
            audioSource.PlayOneShot(pickUpClip);
        }

        hinge.connectedBody = rb;
        hinge.enabled = isPickingUp;
        hinge.autoConfigureConnectedAnchor = !isPickingUp;
        climbingHand.enabled = isPickingUp;

        // Adjust mass based on whether the player is holding something or not
        rigidBody.mass = isPickingUp ? 1f : 0f;
    }


    void Update()
    {
        //if (IsHolding && !groundCheck.isGrounded)
        //{
        //    transform.GetComponent<MoveHand>().enabled = false;
        //}
        //else
        //{
        //    transform.GetComponent<MoveHand>().enabled = true;
        //}
    }
}