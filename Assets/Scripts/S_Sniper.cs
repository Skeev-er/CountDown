using TMPro;
using UnityEngine;

public class S_Sniper : MonoBehaviour
{
    public int bullets = 3;
    private bool aiming = false;
    public SpriteRenderer reticleSprite;
    public SpriteRenderer backgroundSprite;

    public S_Camera cameraScript;

    public TextMeshProUGUI bulletText;

    private bool changeVisibility = false;
    private bool makeAimObjectsVisible = false;
    private float t = 0;

    private void Start()
    {
        RefreshBulletUI();
    }

    void Update()
    {
        // If the right mouse button is pressed and the camera isn't currently transitioning into/out of aiming already
        if (Input.GetMouseButtonDown(1) && !changeVisibility)
        {
            Aim();
        }

        // If the aiming transition is taking place
        if (changeVisibility)
        {
            ColourTransitions();
        }

        // If the player is aiming, pressed left mouse button, and has bullets loaded
        if (aiming && Input.GetMouseButtonDown(0) && bullets >= 1)
        {
            Fire();
        }

        // This will likely be a temporary dev tool
        Reload();
    }

    private void Aim()
    {
        if (!aiming)
        {
            aiming = true;
            makeAimObjectsVisible = true;
        }
        else
        {
            aiming = false;
            makeAimObjectsVisible = false;
        }

        // Trigger the opacity transition
        changeVisibility = true;

        // Tell the camera to either follow the mouse or go back to the middle depending on if you are aiming or not
        cameraScript.Aiming(aiming);
    }

    private void Fire()
    {
        bullets--;
        RefreshBulletUI();

        // Cast a ray from the middle of the reticle
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Shootable"));

        // If the ray hit anything from the shootable layer
        if (hit)
        {
            hit.transform.gameObject.SendMessage("Shot");
        }
    }

    // Temporary dev tool
    private void Reload()
    {
        if (Input.GetKeyDown("r"))
        {
            bullets++;
            RefreshBulletUI();
        }
    }

    private void RefreshBulletUI()
    {
        bulletText.text = "Bullets: " + bullets;
    }

    // AAHHH AVERT YOUR EYES
    // Lerps the alpha of the sprites between 1 and 0
    private void ColourTransitions()
    {
        t += Time.deltaTime;

        if (makeAimObjectsVisible)
        {
            reticleSprite.color = new Color(reticleSprite.color.r, reticleSprite.color.g, reticleSprite.color.b, Mathf.Lerp(reticleSprite.color.a, 1f, t));
            backgroundSprite.color = new Color(backgroundSprite.color.r, backgroundSprite.color.g, backgroundSprite.color.b, Mathf.Lerp(backgroundSprite.color.a, 0.995f, t));
            bulletText.color = new Color(bulletText.color.r, bulletText.color.g, bulletText.color.b, Mathf.Lerp(bulletText.color.a, 1f, t));
        }
        else
        {
            reticleSprite.color = new Color(reticleSprite.color.r, reticleSprite.color.g, reticleSprite.color.b, Mathf.Lerp(reticleSprite.color.a, 0f, t));
            backgroundSprite.color = new Color(backgroundSprite.color.r, backgroundSprite.color.g, backgroundSprite.color.b, Mathf.Lerp(backgroundSprite.color.a, 0f, t));
            bulletText.color = new Color(bulletText.color.r, bulletText.color.g, bulletText.color.b, Mathf.Lerp(bulletText.color.a, 0f, t));
        }

        // Why 0.5? I don't know, but it works as intended so eh 
        if (t > 0.5f)
        {
            t = 0;
            changeVisibility = false;
        }
    }
}
