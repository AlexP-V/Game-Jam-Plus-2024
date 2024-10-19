using UnityEngine;
using static P1Movement;

public class RespawnTrigger : MonoBehaviour
{
    // Make this public so it can be set in the Inspector
    public Transform respawnPoint;

    // This will be called when something enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Call the respawn method on the player object
            PlayerRespawn player = other.GetComponent<PlayerRespawn>();
            if (player != null)
            {
                player.Respawn(respawnPoint.position); // Pass in the respawn location
            }
        }
    }
}


