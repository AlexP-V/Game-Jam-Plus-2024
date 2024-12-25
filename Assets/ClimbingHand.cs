using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ClimbingHand : MonoBehaviour
{
    public float climbSpeed = 5;
    public float rotationForce = 100;
    public float angularDrag = 25f; // Adjustable angular drag in the Inspector
    public float angularVelocityThreshold = 400f; // Threshold for activating angular drag
    float rotationValue = 0;
    [SerializeField] int playerIndex = 0;

    Animator animator;

    float climbValue = 0;

    HingeJoint2D hinge;
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        animator = transform.parent.GetComponentInChildren<Animator>();
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
            // Play climbing sound effect here if needed
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

        // Apply torque at hinge connection pivot
        ApplyTorqueAtPivot(hinge.connectedBody, hinge.connectedAnchor, rotationValue * rotationForce);

        // Update angular drag based on angular velocity
        UpdateAngularDrag(hinge.connectedBody);
    }

    static void ApplyTorqueAtPivot(Rigidbody2D rb, Vector2 pivotPoint, float forceMagnitude)
    {
        Vector2 worldPivotPoint = rb.transform.TransformPoint(pivotPoint);

        Vector2 offset = (Vector2)rb.transform.right * .5f;
        Vector2 pointA = worldPivotPoint + offset;
        Vector2 pointB = worldPivotPoint - offset;

        Vector2 torqueAxis = rb.transform.up;

        rb.AddForceAtPosition(-torqueAxis * forceMagnitude, pointA);
        rb.AddForceAtPosition(torqueAxis * forceMagnitude, pointB);

        if (forceMagnitude != 0)
        {
            // Play rotation sound effect here if needed
        }
    }

    void UpdateAngularDrag(Rigidbody2D rb)
    {
        if (rb == null) return;

        // Check the angular velocity and adjust angular drag
        if (Mathf.Abs(rb.angularVelocity) >= angularVelocityThreshold)
        {
            rb.angularDrag = angularDrag; // Apply angular drag when above the threshold
        }
        else
        {
            rb.angularDrag = 0f; // No angular drag below the threshold
        }
    }
}



