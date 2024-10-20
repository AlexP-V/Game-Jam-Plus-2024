using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public float initialSensitivity = 10f;
    [HideInInspector] public float sens; 
    Rigidbody2D rb;
    Vector3 targetDir;
    [SerializeField] int playerIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sens = initialSensitivity;
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }   

    public void SetTargetDir(Vector3 target)
    {
        targetDir = target;
    }

    void FixedUpdate() 
    {
        targetDir.Normalize();
        rb.velocity = targetDir * sens;
    }
}
