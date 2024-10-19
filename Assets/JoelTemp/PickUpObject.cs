using UnityEngine;
using UnityEngine.UIElements;

public class PickUpObject : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore parent object
        if (other.gameObject == transform.parent.gameObject)
        {
            return;
        }

        if (other.gameObject.GetComponent<DistanceJoint2D>())
        {
            other.gameObject.GetComponent<DistanceJoint2D>().connectedBody = rb;
        }
    }
}
