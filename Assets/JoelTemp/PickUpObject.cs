using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpObject : MonoBehaviour
{
    HingeJoint2D hinge;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        hinge.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore Player layer
        if (other.gameObject.layer == 8)
        {
            return;
        }

        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            FollowMouse followMouse = transform.GetComponent<FollowMouse>();
            followMouse.sens = followMouse.initialSensitivity;
            hinge.connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
            hinge.enabled = true;
            hinge.autoConfigureConnectedAnchor = false;
            transform.GetComponent<ClimbingHand>().enabled = true;
        }
    }
}
