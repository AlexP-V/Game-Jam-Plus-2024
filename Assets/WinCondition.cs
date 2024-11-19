using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public string sceneName;         // Name of the scene to load (win scene)
    public Transform player1;       // Player 1 GameObject
    public Transform player2;       // Player 2 GameObject
    public float triggerDistance = 2f; // Distance threshold for triggering the scene switch

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }

    void Update()
    {
        // Check if either player has reached the final milestone
        if (IsPlayerAtFinalMilestone())
        {
            SwitchScene();
        }
    }

    // This function will be called to switch the scene
    public void SwitchScene()
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
    }

    // Check if either player is at the final milestone
    bool IsPlayerAtFinalMilestone()
    {
        // Calculate the distance between players and the final milestone
        float distance1 = Vector2.Distance(player1.position, transform.position);
        float distance2 = Vector2.Distance(player2.position, transform.position);

        // Return true if either player is within the trigger distance of the final milestone
        return distance1 < triggerDistance || distance2 < triggerDistance;
    }
}