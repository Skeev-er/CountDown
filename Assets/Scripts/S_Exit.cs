using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class S_Exit : MonoBehaviour
{
    private Button exitButton;
    private bool delayDone = false;

    public AudioSource audioManager;
    public AudioClip select;

    void Start()
    {
        exitButton = GetComponent<Button>();

        exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        if (delayDone)
        {
            Application.Quit();
        }
        else
        {
            audioManager.pitch = 1;
            audioManager.PlayOneShot(select);
        }

        StartCoroutine(DelayedExit());
    }

    IEnumerator DelayedExit()
    {
        yield return new WaitForSeconds(0.155f);
        delayDone = true;
        Exit();
    }
}
