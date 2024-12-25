using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ClimbingHand : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    public float climbSpeed = 5;
    public float rotationForce = 100;
    public float maxAngularVelocity = 200; // Angular velocity cap (degrees per second)
    
    float rotationValue = 0;
    [SerializeField] int playerIndex = 0;

    Animator animator;

    float climbValue = 0;

    HingeJoint2D hinge;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        animator = transform.parent.GetComponentInChildren<Animator>();

        //ApplySettings();
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
            //UPDATE THE SCRIPT HERE: play a climbing sfx
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
        // Handle climbing
        float newAnchorX = Mathf.Clamp(hinge.connectedAnchor.x + (climbValue * climbSpeed * Time.fixedDeltaTime) / transform.lossyScale.y, -.5f, .5f);
        hinge.connectedAnchor = new Vector2(newAnchorX, 0);

        // Add torque at hinge connection pivot with capped angular velocity and buffer zone
        ApplyTorqueAtPivot(hinge.connectedBody, hinge.connectedAnchor, rotationValue * rotationForce, maxAngularVelocity, 50f); // Adjust buffer zone if needed
    }



    void CapAngularVelocity(Rigidbody2D rb)
    {
        if (Mathf.Abs(rb.angularVelocity) > maxAngularVelocity)
        {
            rb.angularVelocity = Mathf.Sign(rb.angularVelocity) * maxAngularVelocity;
        }
    }

    static void ApplyTorqueAtPivot(Rigidbody2D rb, Vector2 pivotPoint, float forceMagnitude, float maxAngularVelocity, float bufferZone = 50f)
    {
        // Convert pivot point to world space
        Vector2 worldPivotPoint = rb.transform.TransformPoint(pivotPoint);

        // Determine offset points along the stick's width
        Vector2 offset = (Vector2)rb.transform.right * 0.5f;
        Vector2 pointA = worldPivotPoint + offset;
        Vector2 pointB = worldPivotPoint - offset;

        // Calculate torque axis
        Vector2 torqueAxis = rb.transform.up;

        // Get the current angular velocity of the Rigidbody
        float currentAngularVelocity = rb.angularVelocity;

        // Smooth force adjustment with buffer zone
        float adjustedForceMagnitude = forceMagnitude;

        if (Mathf.Abs(currentAngularVelocity) >= maxAngularVelocity - bufferZone)
        {
            // Scale force smoothly when nearing the max velocity, considering the buffer zone
            float velocityDifference = (maxAngularVelocity - Mathf.Abs(currentAngularVelocity));
            adjustedForceMagnitude *= Mathf.Clamp01(velocityDifference / bufferZone);
        }

        // Calculate and apply forces at points A and B
        Vector2 forceA = -torqueAxis * adjustedForceMagnitude;
        Vector2 forceB = torqueAxis * adjustedForceMagnitude;

        rb.AddForceAtPosition(forceA, pointA);
        rb.AddForceAtPosition(forceB, pointB);

        if (forceMagnitude != 0)
        {
            Debug.Log("Rotation SFX triggered.");
        }
    }


}

