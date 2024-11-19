using UnityEngine;

public class HandMover : MonoBehaviour
{
    public float initialSensitivity = 10f;
    [HideInInspector] public float sens; 
    Rigidbody2D rb;
    Vector3 handTarget;
    [SerializeField] int playerIndex = 0;
    [SerializeField] Transform body;

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
            handTarget = newTarget;
            //handTarget.Normalize();
        //}
    }

    public Vector2 getTargetDir()
    {
        return handTarget;
    }   

    void FixedUpdate() 
    {
        Vector2 moveDir = -rb.position + (Vector2)(handTarget + body.position);
        rb.velocity = moveDir * sens;
    }

    void RotateInDirection(Transform transform, Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
    }

    void Update()
    {
        if (handTarget != Vector3.zero)
            RotateInDirection(transform, handTarget);
    }
}
