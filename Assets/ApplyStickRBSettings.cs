using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStickRBSettings : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    Rigidbody2D rb;
    Collider2D collider;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void ApplySettings()
    {
        rb.mass = settings.stickMass;
        rb.gravityScale = settings.stickGravityScale;
        collider.sharedMaterial.friction = settings.stickFriction;
    }

    void FixedUpdate()
    {
        ApplySettings();
    }
}
