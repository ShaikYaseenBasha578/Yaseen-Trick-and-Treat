using System.Collections;
using UnityEngine;

public class GhostAct3 : MonoBehaviour
{
    public Transform player;               // Reference to the player
    public Transform middleOfTPosition;    // Position representing the middle of the T-shaped hallway
    private Animator ghostAnimator;        // Animator component of the ghost

    private bool playerCrossedMiddle = false;  // Tracks if the player crossed the middle

    private void Start()
    {
        ghostAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player has crossed the middle of the T
        if (!playerCrossedMiddle && Vector3.Distance(player.position, middleOfTPosition.position) < 1f)
        {
            playerCrossedMiddle = true;
            TriggerGhostBehavior();
        }
    }

    void TriggerGhostBehavior()
    {
        // Make the ghost turn toward the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = lookRotation;

        // Trigger scream animation
        if (ghostAnimator != null)
        {
            ghostAnimator.SetTrigger("ScreamTrigger");
            StartCoroutine(StartChaseAfterScream());
        }
    }

    private IEnumerator StartChaseAfterScream()
    {
        yield return new WaitForSeconds(2f);  // Adjust this time as necessary
        if (ghostAnimator != null)
        {
            ghostAnimator.SetBool("isChasing", true);  // Start chasing the player
        }
    }
}

