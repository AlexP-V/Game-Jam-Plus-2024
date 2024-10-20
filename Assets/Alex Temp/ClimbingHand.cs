using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ClimbingHand : MonoBehaviour
{
    public bool arrow;
    public float climbSpeed = 5;
    public float rotationForce = 100;
    [SerializeField] int playerIndex = 0;

    float climbValue = 0;

    HingeJoint2D hinge;
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }   

    public void SetClimbValue(float value)
    {
        climbValue = value;
    }

    void FixedUpdate()
    {
        float newAnchorX = Mathf.Clamp(hinge.connectedAnchor.x + (climbValue * climbSpeed * Time.fixedDeltaTime) / transform.lossyScale.y, -.5f, .5f);
        hinge.connectedAnchor = new Vector2(newAnchorX, 0);

        float rotate = 0;
        if (Input.GetKey(KeyCode.Q))
            rotate -= 1;
        if (Input.GetKey(KeyCode.E))
            rotate += 1;

        // add torque at hinge connection pivot
        ApplyTorqueAtPivot(hinge.connectedBody, hinge.connectedAnchor, rotate * rotationForce);

        climbValue = 0;
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
    }
}
