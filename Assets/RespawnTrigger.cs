using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    // Make this public so it can be set in the Inspector
    public Transform respawnPoint;

    // This will be called when something enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the "Respawn" tag
        if (other.CompareTag("Respawn"))
        {
            // Move the object with the "Respawn" tag to the respawn point
            other.transform.position = respawnPoint.position;
        }
    }
}


