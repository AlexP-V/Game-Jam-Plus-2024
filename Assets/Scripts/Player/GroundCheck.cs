using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector] public bool isGrounded = false;
    Animator animator;

    void Awake()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Player"))
        {
            isGrounded = true;
            animator.SetBool("inAir", false);
            Debug.Log("Grounded");

        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Player"))
        {
            isGrounded = false;
            animator.SetBool("inAir", true);
            Debug.Log("Not Grounded");
        }          
        
    }
}
