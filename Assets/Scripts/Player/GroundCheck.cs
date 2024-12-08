using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector] public bool isGrounded = false;
    Animator animator;

    public float forceAmount = 10f; // Adjust this value to your preference


    void Awake()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            animator.SetBool("inAir", false);

            // Apply a downward force to the player's Rigidbody2D
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.down * forceAmount, ForceMode2D.Impulse);
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            animator.SetBool("inAir", true);
            //Debug.Log("Not Grounded");
        }
    }
}
