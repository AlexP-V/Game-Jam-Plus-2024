using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 70f;
    [SerializeField] int playerIndex = 0;
    Rigidbody2D rb;
    GroundCheck groundCheck;

    DistanceJoint2D handjoint;

    Vector2 inputVector = Vector2.zero;

    Transform hand;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.parent.GetComponentInChildren<GroundCheck>();
        handjoint = GetComponent<DistanceJoint2D>();
        hand = handjoint.connectedBody.transform;
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }   

    public void setInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    public void Jump()
    {
        if (groundCheck.isGrounded) 
        {   
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump");
        }
        
    }

    void FixedUpdate()
    {
        Vector2 moveInput = inputVector;
        moveInput.y = 0;
        rb.AddForce(moveInput * speed, ForceMode2D.Force);

        
        Vector2 handTargetDir = hand.position - transform.position;
        if (handTargetDir.magnitude > handjoint.distance - handjoint.distance * 0.1f)
        {
            hand.GetComponent<MoveHand>().constrainMovement = true;
            Debug.Log("Constraining movement");
        }
        else
        {
            hand.GetComponent<MoveHand>().constrainMovement = false;
            Debug.Log("Not constraining movement");
        }
    }
}

