using UnityEngine;

public class TriggerCanvasDisplay : MonoBehaviour
{
    public GameObject winCanvas;  // Reference to the Canvas that will appear
    private Collider2D player1 = null;
    private Collider2D player2 = null;

    private void Start()
    {
        // Make sure the Canvas is disabled at the start
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is a player and register them if there's space
        if (other.CompareTag("Player"))
        {
            if (player1 == null)
            {
                player1 = other;
                Debug.Log("Player 1 entered the trigger.");
            }
            else if (player2 == null && other != player1)
            {
                player2 = other;
                Debug.Log("Player 2 entered the trigger.");
            }
        }

        // If both players are in the trigger zone, show the canvas
        if (player1 != null && player2 != null)
        {
            Debug.Log("Both players are within the trigger!");
            if (winCanvas != null)
            {
                winCanvas.SetActive(true); // Show the Canvas
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if one of the players has left the trigger zone
        if (other == player1)
        {
            player1 = null;
            Debug.Log("Player 1 left the trigger.");
        }
        else if (other == player2)
        {
            player2 = null;
            Debug.Log("Player 2 left the trigger.");
        }

        // Hide the canvas if either player leaves the zone
        if (winCanvas != null && (player1 == null || player2 == null))
        {
            Debug.Log("One or both players left the trigger. Hiding the canvas.");
            winCanvas.SetActive(false); // Hide the Canvas
        }
    }
}




