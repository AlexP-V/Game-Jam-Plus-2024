using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ClimbingHand : MonoBehaviour
{
    public bool arrow;
    public float climbSpeed = 5;
    public float rotationForce = 100;

    HingeJoint2D hinge;
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    void FixedUpdate()
    {
        float climb = 0;
        if (arrow)
        {
            if(Input.GetKey(KeyCode.UpArrow))
                climb += 1;
            if(Input.GetKey(KeyCode.DownArrow))
                climb -= 1;
        }
        else
        {
            if(Input.GetKey(KeyCode.W))
                climb += 1;
            if(Input.GetKey(KeyCode.S))
                climb -= 1;
        }

        hinge.connectedAnchor += climb * climbSpeed * Time.deltaTime * Vector2.right;

        float rotate = 0;
        if (Input.GetKey(KeyCode.Q))
            rotate -= 1;
        if (Input.GetKey(KeyCode.E))
            rotate += 1;

        // add torque at hinge connection pivot
        ApplyTorqueAtPivot(hinge.connectedBody, hinge.connectedAnchor, rotate * rotationForce);
        
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

        Debug.DrawRay(pointB, torqueAxis);
        Debug.DrawRay(pointA, -torqueAxis);

        Debug.DrawLine(pointA, pointB, Color.red);
    }
}
