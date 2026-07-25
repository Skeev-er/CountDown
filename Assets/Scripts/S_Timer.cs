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

    private void Start()
    {
        lightSource.color = nightColour;
    }

    void Update()
    {
        if (!timeUp)
        {
            // T will equal 1 after 1 second
            t += Time.deltaTime;

            // Use the animation curve (set in inspector) to lerp between night and day
            // Divided by 100 becuase that is the curve's max value
            lightSource.color = Color.Lerp(nightColour, dayColour, curve.Evaluate(t) / 100);
            
            // If the full 3 minutes (180 secs) pass 
            if (t >= 180)
            {
                t = 0;
            }
        }
    }
}
