using UnityEngine;

public class AudioLoopManager : MonoBehaviour
{
    AudioSource[] audioLoops; // Array of 6 AudioSources
    public AudioClip[] audioClips; // Array of 6 AudioClips
    public GameObject player1;       // Player 1 GameObject
    public GameObject player2;       // Player 2 GameObject
    public Transform[] milestones;   // Array of Transform for milestone positions
    private bool fading = false;
    public float volumeFadeOutSpeed = 0.5f;  // Speed at which volume decreases
    public float volumeFadeInSpeed = 0.5f;   // Speed at which volume increases
    public float minVolume = 0.3f;           // Minimum volume after fade
    private float startingVolume;
    public float loopPlayTime = 60f;         // Time before volume starts decreasing

    private int currentMilestoneIndex = 0;
    private float currentLoopStartTime = 0f;

    void Start()
    {
        audioLoops = new AudioSource[audioClips.Length];
        Debug.Log(audioLoops.Length);
        // Start all loops but only set volume for the first loop, others are muted
        for (int i = 0; i < audioLoops.Length; i++)
        {
            audioLoops[i] = gameObject.AddComponent<AudioSource>();
            audioLoops[i].clip = audioClips[i];
            audioLoops[i].Play();
            audioLoops[i].loop = true;
            audioLoops[i].volume = (i == 0) ? 1f : 0f;
        }
        currentLoopStartTime = Time.time;
    }

    void Update()
    {
        // Check if either player has reached the current milestone
        if (PlayerAtMilestone())
        {
            ActivateNextLoop();
        }

        // Check if the current loop has been playing for more than 1 minute, start reducing volume
        AudioSource currentLoop = audioLoops[currentMilestoneIndex];
        if (Time.time - currentLoopStartTime > loopPlayTime)
        {
            fading = true;
            FadeOutCurrentLoop();
        }
        else if (currentLoop.volume < 1f && !fading)
        {
            FadeInCurrentLoop();
        }
    }

    // Update this method to check if either player reached the milestone
    bool PlayerAtMilestone()
    {
        // Ensure both players exist and the current milestone index is valid
        if (player1 == null || player2 == null || currentMilestoneIndex >= milestones.Length) return false;

        // Check if either player has reached the current milestone
        float distance1 = Vector2.Distance(player1.transform.position, milestones[currentMilestoneIndex].position);
        float distance2 = Vector2.Distance(player2.transform.position, milestones[currentMilestoneIndex].position);

        // Trigger the next loop if any player is within 2 units of the milestone
        return distance1 < 2f || distance2 < 2f;
    }

    void ActivateNextLoop()
    {
        if (currentMilestoneIndex < audioLoops.Length - 1)
        {
            // Mute the current loop
            startingVolume = audioLoops[currentMilestoneIndex].volume;
            audioLoops[currentMilestoneIndex].volume = 0f;

            // Move to the next milestone and activate the next loop
            currentMilestoneIndex++;
            audioLoops[currentMilestoneIndex].volume = startingVolume;
            currentLoopStartTime = Time.time; // Reset the timer for the new loop
        }
    }

    void FadeOutCurrentLoop()
    {
        // Gradually reduce the volume of the current loop over time, but no less than the minimum volume
        AudioSource currentLoop = audioLoops[currentMilestoneIndex];
        currentLoop.volume = Mathf.Max(minVolume, currentLoop.volume - volumeFadeOutSpeed * Time.deltaTime);
    }

    void FadeInCurrentLoop()
    {
        // Gradually increase the volume of the current loop over time, but no less than the minimum volume
        AudioSource currentLoop = audioLoops[currentMilestoneIndex];
        currentLoop.volume = Mathf.Max(minVolume, currentLoop.volume + volumeFadeInSpeed * Time.deltaTime);
    }
}








