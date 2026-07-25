using UnityEngine;
using System.Collections;

public class S_Pathfinding : MonoBehaviour
{
    public bool allowedToMove = true;
    private Vector2 randomDirection;

    private Vector2 targetPos;
    public float walkSpeed = 0.8f;

    private bool reachedTarget = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        FindNextTarget();
    }

    void Update()
    {
        if (allowedToMove)
        {
            // Move
            WalkToTarget();
            
            // If the target has been reached, start the coroutine once (sets allowedToMove false)
            if (reachedTarget)
            {
                reachedTarget = false;
                StartCoroutine(PauseWalking());

                // If the coroutine has ended, restart the cycle
                FindNextTarget();
            }
        }
        else
        {
            // Stops animation
            animator.SetBool("reachedTarget", true);
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    // Casts a ray in a random direction until it hits an object on the border layer
    // It then finds a random position along that path
    private void FindNextTarget()
    {
        randomDirection = Random.insideUnitCircle.normalized * 10;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, randomDirection, float.PositiveInfinity, LayerMask.GetMask("Border"));

        targetPos = Vector2.Lerp(transform.position, hit.point, Random.Range(0.2f, 0.9f));
    }

    // Moves towards the target every frame
    private void WalkToTarget()
    {
        // Start animation
        animator.SetBool("reachedTarget", false);

        // Walk towards target
        float step = walkSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);

        // If the target has been reached
        if (transform.position == new Vector3(targetPos.x, targetPos.y, 0))
        {
            reachedTarget = true;
            // Stop animation
            animator.SetBool("reachedTarget", true);
        }
    }

    // Stop walking until the wait time is over
    IEnumerator PauseWalking()
    {
        allowedToMove = false;

        yield return new WaitForSeconds(Random.Range(2,4));

        allowedToMove = true;
    }
}
