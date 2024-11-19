using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float initialSensitivity = 10f;
    [HideInInspector] public float sens; 
    Rigidbody2D rb;
    Vector3 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sens = initialSensitivity;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate() 
    {
        Vector3 mouseDir = mousePos - transform.position;
        mouseDir.Normalize();
        rb.velocity = mouseDir * sens;
    }
}
