using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;         // Name of the scene to load (win scene)
    public GameObject player1;       // Player 1 GameObject
    public GameObject player2;       // Player 2 GameObject
    public Transform finalMilestone; // The final milestone position
    public float triggerDistance = 2f; // Distance threshold for triggering the scene switch

    void Update()
    {
        // Check if either player has reached the final milestone
        if (PlayerAtFinalMilestone())
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
    bool PlayerAtFinalMilestone()
    {
        // Ensure both players and the final milestone are valid
        if (player1 == null || player2 == null || finalMilestone == null) return false;

        // Calculate the distance between players and the final milestone
        float distance1 = Vector2.Distance(player1.transform.position, finalMilestone.position);
        float distance2 = Vector2.Distance(player2.transform.position, finalMilestone.position);

        // Return true if either player is within the trigger distance of the final milestone
        return distance1 < triggerDistance || distance2 < triggerDistance;
    }
}