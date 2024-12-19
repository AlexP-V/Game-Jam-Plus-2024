using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ClimbingHand : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    float climbSpeed = 5;
    float rotationForce = 100;
    float rotationValue = 0;
    [SerializeField] int playerIndex = 0;

    Animator animator;

    float climbValue = 0;

    HingeJoint2D hinge;
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        animator = transform.parent.GetComponentInChildren<Animator>();
        ApplySettings();
    }

    void ApplySettings()
    {
        climbSpeed = settings.climbSpeed;
        rotationForce = settings.handRotationForce;
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
        ApplySettings();

        float newAnchorX = Mathf.Clamp(hinge.connectedAnchor.x + (climbValue * climbSpeed * Time.fixedDeltaTime) / transform.lossyScale.y, -.5f, .5f);
        hinge.connectedAnchor = new Vector2(newAnchorX, 0);

        // add torque at hinge connection pivot
        ApplyTorqueAtPivot(hinge.connectedBody, hinge.connectedAnchor, rotationValue * rotationForce);

        //climbValue = 0;
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
            //UPDATE THE SCRIPT HERE: play a rotation sfx
        }
    }
}
