using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 70f;
    [SerializeField] int playerIndex = 0;
    Rigidbody2D rb;

    Vector2 inputVector = Vector2.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        Vector2 moveInput = inputVector;
        moveInput.y = 0;
        rb.AddForce(moveInput * speed, ForceMode2D.Force);
    }
}
