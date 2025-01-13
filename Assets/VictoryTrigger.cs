using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [Header("Canvas to Activate")]
    [SerializeField] private Canvas victoryCanvas;

    private bool hasPlayer1 = false;
    private bool hasPlayer2 = false;

    private void Start()
    {
        // Ensure the Canvas is disabled at the start
        if (victoryCanvas != null)
        {
            victoryCanvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player 1"))
        {
            hasPlayer1 = true;
        }
        if (other.CompareTag("Player 2"))
        {
            hasPlayer2 = true;
        }
      
        if (hasPlayer1 && hasPlayer2 && victoryCanvas != null)
        {
            victoryCanvas.gameObject.SetActive(true);
            Debug.Log("Victory");
        }
    }   
}

