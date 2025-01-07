using UnityEngine;

public class HandMover : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    [HideInInspector] public float sens;
    private Rigidbody2D rb;
    private Vector3 handTarget;
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private Transform body;

    public float rotationOffset = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplySettings();
    }

    void ApplySettings()
    {
        sens = settings.handSensitivity;
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    public void SetTargetDir(Vector3 newTarget)
    {
        handTarget = newTarget;
    }

    public Vector2 getTargetDir()
    {
        return handTarget;
    }

    public Vector2 GetMoveDir()
    {
        // Compute moveDir as in FixedUpdate
        return -rb.position + (Vector2)(handTarget + body.position);
    }

    void FixedUpdate()
    {
        ApplySettings();
        Vector2 moveDir = GetMoveDir();
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
