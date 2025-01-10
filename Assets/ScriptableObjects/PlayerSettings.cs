using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSettings", fileName = "PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public float movementSpeed = 10f;
    public float maxXSpeed = 10f;
    public float jumpForce = 70f;
    
    [Header("Hand")]
    public float handSensitivity = 10f;
    public float climbSpeed = 10f;
    public float handRotationForce = 10f;
    public float stickAirResistance = 0.1f;
    public float stickRotationResistance = 0.1f;
    public float stickRotationThreshold = 0.1f;

    [Header("Player Rigidbody")]
    public float mass = 1;
    public float gravityScale = 1;
    public float friction = 100f;
    public float airResistance = 0.1f;

    [Header("Stick Rigidbody")]
    public float stickMass = 1;
    public float stickGravityScale = 1;
    public float stickFriction = 0.1f;
}
