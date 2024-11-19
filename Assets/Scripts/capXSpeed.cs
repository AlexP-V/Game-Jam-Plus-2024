using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capXSpeed : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
    }
}
