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
    public float maxHeldObjectRotationSpeed = 45f;

    
    [Header("Rigidbody")]
    public float mass = 1;
    public float gravityScale = 1;
    public float friction = 0.1f;
    public float airResistance = 0.1f;
    
}
