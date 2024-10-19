using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    HingeJoint2D hinge;
    public KeyCode grabKey = KeyCode.E;
    ClimbingHand climbingHand;

    bool IsHolding => hinge.connectedBody != null;
    bool IsGrabInput = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        climbingHand = GetComponent<ClimbingHand>();
        hinge.enabled = false;
    }

    void Update()
    {
        IsGrabInput = Input.GetKeyDown(grabKey);

        if (IsGrabInput && IsHolding)
        {
            IsGrabInput = false;
            SetHold(null);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(IsGrabInput && !IsHolding && other.gameObject.TryGetComponent(out Rigidbody2D rb))
            SetHold(rb);
    }

    void SetHold(Rigidbody2D rb)
    {
        bool isPickingUp = rb != null;
        hinge.connectedBody = rb;
        hinge.enabled = isPickingUp;
        hinge.autoConfigureConnectedAnchor = !isPickingUp;
        climbingHand.enabled = isPickingUp;
    }
}
