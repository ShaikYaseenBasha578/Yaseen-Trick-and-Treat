using UnityEngine;

public class GhostAct3Manager : MonoBehaviour
{
    public Transform player;                  // Player reference
    public Transform middleOfTPosition;       // Position representing the middle of the T-hallway
    public Transform furtherPointPosition;    // Position representing where glass-breaking sound triggers
    public GameObject initialGhostModel;      // The ghost model initially visible in the scene
    public AudioClip reliefSound;             // Sound that plays when ghost is seen facing away
    public AudioClip glassBreakingSound;      // Glass-breaking sound
    public GhostTrigger ghostTriggerScript;   // Reference to the GhostTrigger script

    private bool playerReachedMiddle = false;
    private bool glassBreakTriggered = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Trigger the relief sound when the player reaches the middle of the T
        if (!playerReachedMiddle && Vector3.Distance(player.position, middleOfTPosition.position) < 1f)
        {
            playerReachedMiddle = true;
            PlayReliefSound();
        }

        // Trigger glass-breaking sound and ghost spawn when the player goes a bit further
        if (playerReachedMiddle && !glassBreakTriggered && Vector3.Distance(player.position, furtherPointPosition.position) < 1f)
        {
            glassBreakTriggered = true;
            TriggerGlassBreakAndSpawnGhost();
        }
    }

    private void PlayReliefSound()
    {
        if (audioSource != null && reliefSound != null)
        {
            audioSource.PlayOneShot(reliefSound);
            Debug.Log("Relief sound played.");
        }
    }

    private void TriggerGlassBreakAndSpawnGhost()
    {
        if (audioSource != null && glassBreakingSound != null)
        {
            audioSource.PlayOneShot(glassBreakingSound);
            Debug.Log("Glass-breaking sound played.");
        }

        // Disable initial ghost model
        initialGhostModel.SetActive(false);

        // Trigger ghost spawn
        ghostTriggerScript.SpawnGhostInScene();
    }
}
