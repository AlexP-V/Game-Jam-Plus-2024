using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickGroundCheck : MonoBehaviour
{
    [HideInInspector] public bool isGrounded = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Stick is Grounded");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Stick is Not Grounded");
        }         
    }
}
