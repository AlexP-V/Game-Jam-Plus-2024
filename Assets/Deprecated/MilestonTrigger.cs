using UnityEngine;

public class MilestoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("A player entered the milestone trigger!");
        }
    }
}
