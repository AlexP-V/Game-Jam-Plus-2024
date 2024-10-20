using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    HingeJoint2D hinge;
    public KeyCode grabKey = KeyCode.E;
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

    public void SetGrabInput(bool value)
    {
        IsGrabInput = value;
        Debug.Log("Grab input: " + value);

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
