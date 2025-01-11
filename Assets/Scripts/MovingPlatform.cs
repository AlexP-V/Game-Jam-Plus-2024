using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float movementDistance = 5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float distance = Mathf.PingPong(Time.time * speed, movementDistance);
        transform.position = startPosition + Vector3.right * distance;
    }
}