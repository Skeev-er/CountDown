using System.Collections;
using UnityEngine;

public class S_NPC : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private PolygonCollider2D collisionBox;
    public bool isDracula = false;
    private Vector2 targetPos;
    public bool reachedTarget = true;
    private float step;
    public float walkSpeed = 10;
    private Animator animator;
    private bool waitTimeDone = true;
    private ParticleSystem particles;
    private bool allowedToMove = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collisionBox = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();

        // Randomise sprite if they are a civilian
        if(!isDracula)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        // Sprite was already set in inspector, so only thing to do is randomise position
        else
        {
            transform.position = new Vector2(Random.Range(-8.39f, 8.39f), Random.Range(4.37f, -4.37f));
        }

            RandomWalkTarget();
    }

    // Every frame
    private void Update()
    {
        if (allowedToMove)
        {
            // If the target hasn't been reached yet, move towards it
            if (!reachedTarget)
            {
                WalkToPoint();
            }
            // If the target has been reached and you aren't currently waiting, start waiting
            else if (waitTimeDone)
            {
                RandomWaitTime();
            }
        }
    }

    // If clicked on when aiming with sniper
    public void Shot()
    {
        // Death effects
        allowedToMove = false;
        animator.SetBool("reachedTarget", true);
        spriteRenderer.color = Color.red;
        collisionBox.enabled = false;
        particles.Play();
        
        // Delete self after double the input time (goes invisible after 1x intput time)
        StartCoroutine(DeleteAfterDelay(0.25f));

        // Output if the NPC was dracula or not
        if (isDracula)
        {
            print("You got him");
        }
        else
        {
            print("THAT WAS A CIVVY");
        }
    }

    // Generate a random point on the map to walk to
    private void RandomWalkTarget()
    {
        targetPos = new Vector2(Random.Range(-8.39f, 8.39f), Random.Range(4.37f, -4.37f));
    }

    // Move towards the target
    private void WalkToPoint()
    {
        step = walkSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);

        // If the target has been reached
        if (transform.position == new Vector3(targetPos.x, targetPos.y, 0))
        {
            reachedTarget = true;
            // Stop animation
            animator.SetBool("reachedTarget", true);
        }
    }

    // Start waiting for 1-3 seconds
    private void RandomWaitTime()
    {
        waitTimeDone = false;
        float waitTime = Random.Range(1, 3);
        StartCoroutine(PauseWalking(waitTime));
    }

    IEnumerator PauseWalking(float waitTime)
    {
        // Reset rotation from the walking animation
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

        yield return new WaitForSeconds(waitTime);
        waitTimeDone = true;
        reachedTarget = false;

        // Restart walk animation and get a new target to walk to
        animator.SetBool("reachedTarget", false);
        RandomWalkTarget();
    }

    // Goes invisible after delayTime, then deletes itself after another delayTime
    IEnumerator DeleteAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        spriteRenderer.color = Color.clear;
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
