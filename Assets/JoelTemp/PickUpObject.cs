using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    HingeJoint2D hinge;
    ClimbingHand climbingHand;

    bool IsHolding => hinge.connectedBody != null;
    bool IsGrabInput = false;

    [SerializeField] int playerIndex = 0;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        climbingHand = GetComponent<ClimbingHand>();
        hinge.enabled = false;
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }   

    public void SetGrabInput()
    {
        IsGrabInput = !IsGrabInput;

        if (!IsGrabInput)
            SetHold(null);
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
