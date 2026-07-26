using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class S_Timer : MonoBehaviour
{
    public Light2D lightSource;
    private Color nightColour = new Color32(70, 45, 70, 255);
    private Color dayColour = new Color32(255, 205, 150, 255);
    public AnimationCurve curve;
    private float t = 0;
    private bool timeUp = false;

    public TextMeshProUGUI timerText;

    public S_Reload reload;
    private bool carvedStake = true;

    public S_EndScreen endScreen;

    private void Start()
    {
        lightSource.color = nightColour;
        StartCoroutine(ResetCarvedStake());
    }

    void Update()
    {
        if (!timeUp)
        {
            // T will equal 1 after 1 second
            t += Time.deltaTime;

            timerText.text = "Sunrise In: " + (180 - (int)t); 

            if ((int)t % 20 == 0 && !carvedStake)
            {
                carvedStake = true;
                reload.CarveStake();
                StartCoroutine(ResetCarvedStake());
            }

            // Use the animation curve (set in inspector) to lerp between night and day
            // Divided by 100 becuase that is the curve's max value
            lightSource.color = Color.Lerp(nightColour, dayColour, curve.Evaluate(t) / 100);
            
            // If the full 3 minutes (180 secs) pass 
            if (t >= 180)
            {
                Time.timeScale = 0;

                endScreen.OpenEndScreen();
            }
        }
    }

    IEnumerator ResetCarvedStake()
    {
        yield return new WaitForSeconds(1);
        carvedStake = false;
    }

    public int GetTimeTaken()
    {
        return (int)t;
    }
}
