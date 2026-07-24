using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Sniper : MonoBehaviour
{
    public int bullets = 3;
    private bool aiming = false;
    public SpriteRenderer reticleSprite;
    public SpriteRenderer backgroundSprite;

    public S_Camera cameraScript;

    private bool changeVisibility = false;
    private bool makeAimObjectsVisible = false;
    private float t = 0;

    public S_BulletCount bulletUI;
    private Image[] bulletSprites;
    private bool gotSprites = false;

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
            gotSprites = false;
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

        // Trigger the camera zoom in S_Camera
        cameraScript.changeZoom = true;
        
        // Tell the camera to either follow the mouse or go back to the middle depending on if you are aiming or not
        cameraScript.Aiming(aiming);
    }

    private void Fire()
    {
        bullets--;
        bulletUI.RemoveBullet();

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
            bulletUI.AddBullet();
        }
    }

    // Lerps the alpha of the sprites between 1 and 0
    private void ColourTransitions()
    {
        t += Time.deltaTime;
        if (!gotSprites)
        {
            bulletSprites = new Image[bullets];

            for (int i = 0; i < bullets; i++)
            {
                bulletSprites[i] = bulletUI.transform.GetChild(i).gameObject.GetComponent<Image>();
            }
            gotSprites = true;
        }
        
        
        if (makeAimObjectsVisible)
        {
            reticleSprite.color = new Color(1, 1, 1, Mathf.Lerp(reticleSprite.color.a, 1f, t));
            backgroundSprite.color = new Color(0.463f, 0.463f, 0.463f, Mathf.Lerp(backgroundSprite.color.a, 0.995f, t));

            for (int i = 0; i < bullets; i++)
            {
                bulletSprites[i].color = new Color(1,1,1, Mathf.Lerp(bulletSprites[i].color.a, 1, t));
            }
        }
        else
        {
            reticleSprite.color = new Color(1, 1, 1, Mathf.Lerp(reticleSprite.color.a, 0f, t));
            backgroundSprite.color = new Color(0.463f, 0.463f, 0.463f, Mathf.Lerp(backgroundSprite.color.a, 0f, t));

            for (int i = 0; i < bullets; i++)
            {
                bulletSprites[i].color = new Color(1, 1, 1, Mathf.Lerp(bulletSprites[i].color.a, 0, t));
            }
        }

        // Why 0.5? I don't know, but it works as intended so eh 
        if (t > 0.5f)
        {
            t = 0;
            changeVisibility = false;
        }
    }
}
