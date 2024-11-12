using System.Collections;
using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    public GameObject ghostPrefab;  // Reference to the ghost prefab
    public Transform spawnLocation;  // Location where the ghost will spawn
    public GameObject player;  // Player reference
    public float spawnDistance = 5f;  // Distance at which the ghost will spawn

    private GameObject ghostInstance;  // Instance of the spawned ghost
    private Animator ghostAnimator;
    private bool canMove = false;  // Flag to control ghost movement

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, spawnLocation.position);

        // Spawn the ghost when the player is within spawn distance
        if (ghostInstance == null && distanceToPlayer <= spawnDistance)
        {
            SpawnGhost();
        }

        // Make the ghost move towards the player if it's allowed to move
        if (canMove && ghostInstance != null)
        {
            // Code for moving the ghost towards the player
            ghostInstance.transform.position = Vector3.MoveTowards(
                ghostInstance.transform.position,
                player.transform.position,
                2f * Time.deltaTime  // Adjust speed as needed
            );
        }
    }

    void SpawnGhost()
    {
        // Instantiate the ghost at the spawn location
        ghostInstance = Instantiate(ghostPrefab, spawnLocation.position, spawnLocation.rotation);
        ghostAnimator = ghostInstance.GetComponent<Animator>();

        // Start the scream animation
        if (ghostAnimator != null)
        {
            ghostAnimator.SetTrigger("ScreamTrigger");  // Transition from Idle to Scream
            StartCoroutine(StartChaseAfterScream());    // Schedule chase start
        }
    }

    // Coroutine to transition from scream to chase
    private IEnumerator StartChaseAfterScream()
    {
        canMove = false;  // Disable movement during scream
        yield return new WaitForSeconds(2f);  // Adjust this delay as needed

        if (ghostAnimator != null)
        {
            ghostAnimator.SetBool("isChasing", true);  // Transition from Scream to Chasing
            canMove = true;  // Enable movement after scream
        }
    }

    // Method to despawn the ghost
    public void DespawnGhost()
    {
        if (ghostInstance != null)
        {
            Destroy(ghostInstance);
            Debug.Log("Ghost has despawned.");
        }
    }

    // Manually spawn ghost from the scene (for testing purposes)
    [ContextMenu("Spawn Ghost In Scene")]
    public void SpawnGhostInScene()
    {
        if (ghostInstance == null)
        {
            SpawnGhost();
        }
    }


}
