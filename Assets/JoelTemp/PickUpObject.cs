using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    HingeJoint2D hinge;
    ClimbingHand climbingHand;
    Rigidbody2D rigidBody;
    GroundCheck groundCheck;

    // Audio clips for pick up and drop
    public AudioClip pickUpClip;
    public AudioClip dropClip;
    private AudioSource audioSource;

    bool IsHolding => hinge.connectedBody != null;
    bool IsGrabInput = false;

    [SerializeField] int playerIndex = 0;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        climbingHand = GetComponent<ClimbingHand>();
        hinge.enabled = false;
        rigidBody = GetComponent<Rigidbody2D>();
        groundCheck = transform.parent.GetComponentInChildren<GroundCheck>();

        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }   

    public void SetGrabInput()
    {
        IsGrabInput = !IsGrabInput;

        if (!IsGrabInput)
            SetHold(null);  // Drop the object if the grab input is released
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
        if (isPickingUp && pickUpClip != null)
        {
            audioSource.PlayOneShot(pickUpClip);
        }
        else if (!isPickingUp && dropClip != null)
        {
            audioSource.PlayOneShot(dropClip);
        }

        hinge.connectedBody = rb;
        hinge.enabled = isPickingUp;
        hinge.autoConfigureConnectedAnchor = !isPickingUp;
        climbingHand.enabled = isPickingUp;

        // Adjust mass based on whether the player is holding something or not
        rigidBody.mass = isPickingUp ? 1f : 0f;
    }
}