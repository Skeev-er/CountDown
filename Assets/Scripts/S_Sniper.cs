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

    public GameObject tutorialButton;
    private Image tutorialButtonImage;
    private TextMeshProUGUI tutorialButtonText;

    public GameObject reloadButton;
    private Image reloadButtonImage;
    private TextMeshProUGUI reloadButtonText;

    public Image timerImage;
    private TextMeshProUGUI timerText;

    private AudioSource audioSource;

    private void Start()
    {
        tutorialButtonImage = tutorialButton.GetComponent<Image>();
        tutorialButtonText = tutorialButton.GetComponentInChildren<TextMeshProUGUI>();
        reloadButtonImage = reloadButton.GetComponent<Image>();
        reloadButtonText = reloadButton.GetComponentInChildren<TextMeshProUGUI>();
        timerText = timerImage.GetComponentInChildren<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
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
            gotSprites = false;
            ColourTransitions();
        }

        // If the player is aiming, pressed left mouse button, and has bullets loaded
        if (aiming && Input.GetMouseButtonDown(0) && bullets >= 1)
        {
            Fire();
        }
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
            tutorialButton.SetActive(true);
            reloadButton.SetActive(true);
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
        audioSource.Play();

        // Cast a ray from the middle of the reticle
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Shootable"));

        // If the ray hit anything from the shootable layer
        if (hit)
        {
            hit.transform.gameObject.SendMessage("Shot");
        }

        cameraScript.Fired();
    }

    // Called by S_Reload when the reload button is pressed and there are stakes carved
    public void Reload()
    {
        bullets++;
        bulletUI.AddBullet();
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
        
        // AAAAHHHH AVERT YOUR EYES
        if (makeAimObjectsVisible)
        {
            reticleSprite.color = new Color(1, 1, 1, Mathf.Lerp(reticleSprite.color.a, 1f, t));
            backgroundSprite.color = new Color(0.463f, 0.463f, 0.463f, Mathf.Lerp(backgroundSprite.color.a, 0.995f, t));

            tutorialButtonImage.color = new Color(0.906f, 0.337f, 0.165f, Mathf.Lerp(tutorialButtonImage.color.a, 0f, t));
            tutorialButtonText.color = new Color(0.2f, 0.2f, 0.2f, Mathf.Lerp(tutorialButtonText.color.a, 0f, t));
            reloadButtonImage.color = new Color(0.906f, 0.337f, 0.165f, Mathf.Lerp(tutorialButtonImage.color.a, 0f, t));
            reloadButtonText.color = new Color(0.2f, 0.2f, 0.2f, Mathf.Lerp(tutorialButtonText.color.a, 0f, t));
            timerImage.color = new Color(1, 1, 1, Mathf.Lerp(timerImage.color.a, 0f, t));
            timerText.color = new Color(0.2f, 0.2f, 0.2f, Mathf.Lerp(timerText.color.a, 0f, t));

            for (int i = 0; i < bullets; i++)
            {
                bulletSprites[i].color = new Color(1,1,1, Mathf.Lerp(bulletSprites[i].color.a, 1, t));
            }
        }
        else
        {
            reticleSprite.color = new Color(1, 1, 1, Mathf.Lerp(reticleSprite.color.a, 0f, t));
            backgroundSprite.color = new Color(0.463f, 0.463f, 0.463f, Mathf.Lerp(backgroundSprite.color.a, 0f, t));

            tutorialButtonImage.color = new Color(0.906f, 0.337f, 0.165f, Mathf.Lerp(tutorialButtonImage.color.a, 1f, t));
            tutorialButtonText.color = new Color(0.2f, 0.2f, 0.2f, Mathf.Lerp(tutorialButtonText.color.a, 1f, t));
            reloadButtonImage.color = new Color(0.906f, 0.337f, 0.165f, Mathf.Lerp(tutorialButtonImage.color.a, 1f, t));
            reloadButtonText.color = new Color(0.2f, 0.2f, 0.2f, Mathf.Lerp(tutorialButtonText.color.a, 1f, t));
            timerImage.color = new Color(1, 1, 1, Mathf.Lerp(timerImage.color.a, 1f, t));
            timerText.color = new Color(0.2f, 0.2f, 0.2f, Mathf.Lerp(timerText.color.a, 1f, t));

            for (int i = 0; i < bullets; i++)
            {
                bulletSprites[i].color = new Color(1, 1, 1, Mathf.Lerp(bulletSprites[i].color.a, 0, t));
            }
        }

        // Why 0.5? I don't know, but it works as intended so eh 
        if (t >= 0.5f)
        {
            t = 0;
            changeVisibility = false;

            if(makeAimObjectsVisible)
            {
                tutorialButton.SetActive(false);
                reloadButton.SetActive(false);
            } 
        }
    }
}
