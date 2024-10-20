using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public float initialSensitivity = 10f;
    [HideInInspector] public float sens; 
    Rigidbody2D rb;
    Vector3 targetDir;
    [SerializeField] int playerIndex = 0;

    [HideInInspector] public bool constrainMovement = false;

    public float rotationOffset = 0;    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sens = initialSensitivity;
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }   

    public void SetTargetDir(Vector3 newTarget)
    {
       // if (!constrainMovement)
        //{
            targetDir = newTarget;
            targetDir.Normalize();
        //}
    }

    public Vector2 getTargetDir()
    {
        return targetDir;
    }   

    void FixedUpdate() 
    {
        rb.velocity = targetDir * sens;
    }

    void RotateInDirection(Transform transform, Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
    }

    void Update()
    {
        if (targetDir != Vector3.zero)
            RotateInDirection(transform, targetDir);
    }    
}
