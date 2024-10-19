using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ClimbingHand : MonoBehaviour
{
    public bool arrow;
    public float climbSpeed = 5;

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

        GetComponent<HingeJoint2D>().connectedAnchor += climb * climbSpeed * Time.deltaTime * Vector2.right;
    }
}
