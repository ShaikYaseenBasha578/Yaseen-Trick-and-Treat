using UnityEngine;

public class HideInCloset : MonoBehaviour
{
    public Transform closetHidePosition;  // Position inside the closet for hiding
    public GameObject player;             // Reference to the player
    public KeyCode exitKey = KeyCode.E;   // Key to exit the closet
    public GhostTrigger ghostTrigger;     // Reference to the GhostTrigger

    private bool isHiding = false;
    private Vector3 originalPosition;     // Store player's original position
    private FirstPersonController fpsController; // Reference to FirstPersonController script

    void Start()
    {
        // Cache the player's original position for when they exit
        originalPosition = player.transform.position;

        // Get the FirstPersonController component from the player
        fpsController = player.GetComponent<FirstPersonController>();
    }

    void OnMouseDown()
    {
        if (!isHiding)
        {
            Hide();
        }
        else
        {
            ExitCloset();
        }
    }

    void Update()
    {
        if (isHiding && Input.GetKeyDown(exitKey))
        {
            ExitCloset();
        }
    }

    void Hide()
    {
        isHiding = true;
        player.transform.position = closetHidePosition.position; // Move player to hide position
        player.transform.rotation = closetHidePosition.rotation; // Match rotation

        // Disable player movement by adjusting the FirstPersonController script
        fpsController.enabled = false;  // Disable the whole controller temporarily
        Debug.Log("You are now hiding.");

        // Despawn the ghost
        if (ghostTrigger != null)
        {
            ghostTrigger.DespawnGhost();
        }
    }

    void ExitCloset()
    {
        isHiding = false;
        player.transform.position = originalPosition; // Return player to original position
        fpsController.enabled = true;  // Re-enable movement by enabling the controller
        Debug.Log("You have exited the closet.");
    }
}
