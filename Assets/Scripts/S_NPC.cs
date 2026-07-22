using UnityEngine;

public class S_NPC : MonoBehaviour
{
    private SpriteRenderer sprite;
    private BoxCollider2D collisionBox;
    public bool isDracula = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collisionBox = GetComponent<BoxCollider2D>();
    }

    // Temporary code just to show that it works. Do a complete overhaul later
    public void Shot()
    {
        sprite.color = Color.red;
        collisionBox.enabled = false;

        if (isDracula)
        {
            print("You got him");
        }
        else
        {
            print("THAT WAS A CIVVY");
        }
    }
}
