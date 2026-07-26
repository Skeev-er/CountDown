using System.Collections;
using UnityEngine;

public class S_NPC : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private PolygonCollider2D collisionBox;
    public bool isDracula = false;
    private ParticleSystem particles;
    public S_Pathfinding pathfindingScript;

    private GameObject endScreen;
    private S_EndScreen endScreenScript;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collisionBox = GetComponent<PolygonCollider2D>();
        particles = GetComponent<ParticleSystem>();

        endScreen = GameObject.Find("End Screen Canvas");
        endScreenScript = endScreen.GetComponent<S_EndScreen>();

        // Randomise sprite if they are a civilian
        if (!isDracula)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        // Sprite was already set in inspector, so only thing to do is randomise position
        else
        {
            SpawnAtRandomPosition();
        }
    }

    private void SpawnAtRandomPosition()
    {
        Vector2 randomPos = new Vector2(Random.Range(-12.44f, 12.44f), Random.Range(6.65f, -6.65f));
        RaycastHit2D hit = Physics2D.Raycast(randomPos, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Border"));

        if (hit)
        {
            SpawnAtRandomPosition();
        }
        else
        {
            transform.position = randomPos;
        }
    }

    // If clicked on when aiming with sniper
    public void Shot()
    {
        // Death effects
        spriteRenderer.color = Color.red;
        collisionBox.enabled = false;
        particles.Play();
        pathfindingScript.allowedToMove = false;

        endScreenScript.NPCKilled(isDracula);

        // Delete self after double the input time (goes invisible after 1x intput time)
        StartCoroutine(DeleteAfterDelay(0.25f));

        // Output if the NPC was dracula or not
        if (isDracula)
        {
            StartCoroutine(DelayedOpenEndScreen(0.5f));
        }
    }

    // Goes invisible after delayTime, then deletes itself after another delayTime
    IEnumerator DeleteAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        spriteRenderer.color = Color.clear;
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }

    IEnumerator DelayedOpenEndScreen(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        endScreenScript.OpenEndScreen();
    }
}
