using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capSpeed : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
