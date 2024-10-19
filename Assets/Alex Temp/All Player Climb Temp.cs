using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public KeyCode jumpKeyCode;
    public bool arrow;
    public float jumpForce;
    public float climbSpeed = 5; 

    void Update()
    {
        if(Input.GetKeyDown(jumpKeyCode))
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        float climb = 0;
        if (arrow)
        {
            if(Input.GetKey(KeyCode.W))
                climb += 1;
            if(Input.GetKey(KeyCode.S))
                climb -= 1;
        }
        else
        {
            if(Input.GetKey(KeyCode.UpArrow))
                climb += 1;
            if(Input.GetKey(KeyCode.DownArrow))
                climb -= 1;
        }

        GetComponent<HingeJoint2D>().connectedAnchor += climb * climbSpeed * Time.deltaTime * Vector2.right;
    }
}
