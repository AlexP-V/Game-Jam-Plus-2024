using UnityEngine;

public class CapZRotation : MonoBehaviour
{
    // Maximum allowed rotation speed along the Z-axis (degrees per second)
    public float maxRotationSpeed = 45f;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb2D;

    
    void Start()
    {
        // Get the Rigidbody2D component attached to the object
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("No Rigidbody2D component found on this GameObject.");
        }
    }
    

    void FixedUpdate()
    {
        if (rb2D == null) return;

        // Get the current angular velocity
        float angularVelocity = rb2D.angularVelocity;

        // Convert maximum rotation speed to radians per second
        float maxRotationSpeedRad = maxRotationSpeed * Mathf.Deg2Rad;

        // Clamp the Z-axis angular velocity
        if (Mathf.Abs(angularVelocity) > maxRotationSpeedRad)
        {
            angularVelocity = Mathf.Sign(angularVelocity) * maxRotationSpeedRad;
            rb2D.angularVelocity = angularVelocity;
        }
    }
}

