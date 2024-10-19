using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public float jumpForce = 700f;
    public string horizontalAxis;
    [SerializeField] private KeyCode jumpKey;
    Rigidbody2D rb;
    int jumps = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(horizontalAxis);

        Vector2 movement = new Vector2(moveHorizontal, 0f);
        rb.AddForce(movement * speed, ForceMode2D.Force);

        if (Input.GetKeyDown(jumpKey))
        {
            Debug.Log("Jump " + jumps);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumps++;
        }
    }
}