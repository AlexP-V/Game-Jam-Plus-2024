using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // This method will be called by the RespawnTrigger when the player touches the trigger
    public void Respawn(Vector3 respawnLocation)
    {
        // Move the player to the respawn location
        transform.position = respawnLocation;
    }
}

