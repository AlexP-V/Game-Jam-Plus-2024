using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public float initialSensitivity = 10f;
    [HideInInspector] public float sens; 
    Rigidbody2D rb;
    Vector3 targetDir;
    [SerializeField] int playerIndex = 0;

    [HideInInspector] public bool constrainMovement = false;

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
}
