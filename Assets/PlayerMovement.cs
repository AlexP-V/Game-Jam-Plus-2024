using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public float jumpForce = 70f;
    public string horizontalAxis;
    [SerializeField] KeyCode jumpKey;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(horizontalAxis);

        Vector2 movement = new Vector2(moveHorizontal, 0f);
        rb.AddForce(movement * speed, ForceMode2D.Force);
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}