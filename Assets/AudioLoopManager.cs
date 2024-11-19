using UnityEngine;

public class AudioLoopManager : MonoBehaviour
{
    [SerializeField] private Collider2D player1Trigger; // Trigger for Player 1
    [SerializeField] private Collider2D player2Trigger; // Trigger for Player 2
    [SerializeField] private AudioSource musicSource;   // The AudioSource component for the music track

    private bool player1Triggered = false; // Tracks if Player 1 has triggered
    private bool player2Triggered = false; // Tracks if Player 2 has triggered

    void Update()
    {
        // Check if both players have triggered, and the music isn't already playing
        if (player1Triggered && player2Triggered && !musicSource.isPlaying)
        {
            musicSource.Play(); // Start playing the music
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if Player 1 triggered its collider
        if (other == player1Trigger)
        {
            player1Triggered = true;
        }

        // Check if Player 2 triggered its collider
        if (other == player2Trigger)
        {
            player2Triggered = true;
        }
    }
}









