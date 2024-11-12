using UnityEngine;

public class GhostMovementTest : MonoBehaviour
{
    public float moveSpeed = 2f;  // Movement speed of the ghost
    public Animator ghostAnimator;  // Reference to the Animator component

    private void Start()
    {
        if (ghostAnimator == null)
        {
            ghostAnimator = GetComponent<Animator>();  // Automatically find the Animator on the ghost
        }
    }

    private void Update()
    {
        // Move the ghost forward
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Test animations by setting different Animator parameters
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetIdleAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWalkingAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetScreamAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetChasingAnimation();
        }
    }

    void SetIdleAnimation()
    {
        ghostAnimator.SetBool("isWalking", false);
        ghostAnimator.SetBool("isChasing", false);
    }

    void SetWalkingAnimation()
    {
        ghostAnimator.SetBool("isWalking", true);
        ghostAnimator.SetBool("isChasing", false);
    }

    void SetScreamAnimation()
    {
        ghostAnimator.SetTrigger("Scream");
    }

    void SetChasingAnimation()
    {
        ghostAnimator.SetBool("isWalking", false);
        ghostAnimator.SetBool("isChasing", true);
    }
}
