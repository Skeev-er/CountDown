using TMPro;
using UnityEngine;

public class S_EndScreen : MonoBehaviour
{
    public Canvas UI;
    private Canvas endScreenCanvas;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI bodyText;
    public TextMeshProUGUI statsText;

    public S_Timer timer;

    private bool dracDown = false;
    private int timeTaken;
    private int civiliansKilled = 0;

    public S_Sniper sniper;

    private void Start()
    {
        endScreenCanvas = GetComponent<Canvas>();
    }

    public void OpenEndScreen()
    {
        endScreenCanvas.enabled = true;
        UI.enabled = false;
        sniper.enabled = false;

        if (dracDown)
        {
            headerText.text = "Count down!\nI repeat, Count down!";
            bodyText.text = "Good job!\nWe no longer need to live in fear of vampirism thanks to you.\nNow lets just hope there were no unnecessary casualties.";
            statsText.text = "Stats:\nTime Taken - " + timeTaken + "s\nCivilians Killed - " + civiliansKilled + "\nCount Down? - Yes";
        }
        else // Time Up
        {
            headerText.text = "Time's Up!";
            bodyText.text = "The Count has fled.\\nHopefully we will get another chance at taking him down, once and for all.\\nUntil that day, we shall live in fear.";
            statsText.text = "Stats:\nTime Taken - 180s\nCivilians Killed - " + civiliansKilled + "\nCount Down? - No";
        }
    }

    public void NPCKilled(bool wasDrac)
    {
        if(wasDrac)
        {
            dracDown = true;
            timeTaken = timer.GetTimeTaken();
        }
        else
        {
            civiliansKilled++;
        }
    }
}
