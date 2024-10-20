using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ClimbingHand : MonoBehaviour
{
    public bool arrow;
    public float climbSpeed = 5;
    public float rotationForce = 100;
    float rotationValue = 0;
    [SerializeField] int playerIndex = 0;

    Animator animator;
    float climbValue = 0;

    HingeJoint2D hinge;

    // Audio clips for rotation sounds and climbing sounds
    public AudioClip rotateLeftClip;
    public AudioClip rotateRightClip;
    public AudioClip climbUpClip;
    public AudioClip climbDownClip;

    private AudioSource audioSource;

    // Track the previous Y position to detect movement direction
    private float previousClimbPositionY = 0;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        animator = transform.parent.GetComponentInChildren<Animator>();

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

    public void SetClimbValue(float value)
    {
        climbValue = value;
        if (climbValue != 0)
        {
            animator.SetBool("isClimbing", true);

            // Detect movement direction (up or down) and play the respective sound
            float currentClimbPositionY = hinge.connectedAnchor.y;

            if (currentClimbPositionY > previousClimbPositionY && climbUpClip != null && !audioSource.isPlaying)
            {
                // Moving up the stick
                audioSource.PlayOneShot(climbUpClip);
            }
            else if (currentClimbPositionY < previousClimbPositionY && climbDownClip != null && !audioSource.isPlaying)
            {
                // Moving down the stick
                audioSource.PlayOneShot(climbDownClip);
            }

            // Update previous position
            previousClimbPositionY = currentClimbPositionY;
        }
        else
        {
            animator.SetBool("isClimbing", false);
        }
    }

    public void setRotationValue(float value)
    {
        rotationValue = value;
    }

    void FixedUpdate()
    {
        float newAnchorX = Mathf.Clamp(hinge.connectedAnchor.x + (climbValue * climbSpeed * Time.fixedDeltaTime) / transform.lossyScale.y, -.5f, .5f);
        hinge.connectedAnchor = new Vector2(newAnchorX, 0);

        // Add torque at hinge connection pivot
        ApplyTorqueAtPivot(hinge.connectedBody, hinge.connectedAnchor, rotationValue * rotationForce);
    }

    void ApplyTorqueAtPivot(Rigidbody2D rb, Vector2 pivotPoint, float forceMagnitude)
    {
        Vector2 worldPivotPoint = rb.transform.TransformPoint(pivotPoint);

        Vector2 offset = (Vector2)rb.transform.right * .5f;
        Vector2 pointA = worldPivotPoint + offset;
        Vector2 pointB = worldPivotPoint - offset;

        Vector2 torqueAxis = rb.transform.up;

        rb.AddForceAtPosition(-torqueAxis * forceMagnitude, pointA);
        rb.AddForceAtPosition(torqueAxis * forceMagnitude, pointB);

        // Play rotation sound effect based on the direction (left or right)
        if (forceMagnitude > 0 && rotateRightClip != null)  // Rotating to the right
        {
            audioSource.PlayOneShot(rotateRightClip);
        }
        else if (forceMagnitude < 0 && rotateLeftClip != null)  // Rotating to the left
        {
            audioSource.PlayOneShot(rotateLeftClip);
        }
    }
}
