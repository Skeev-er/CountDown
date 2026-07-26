using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_Start : MonoBehaviour
{
    private Button startButton;
    private bool delayDone = false;

    public AudioSource audioManager;
    public AudioClip select;

    void Start()
    {
        startButton = GetComponent<Button>();

        startButton.onClick.AddListener(OpenLevel);
    }

    private void OpenLevel()
    {
        if (delayDone)
        {
            SceneManager.LoadScene("Level");
        }
        else
        {
            audioManager.pitch = 2;
            audioManager.PlayOneShot(select);
        }

        StartCoroutine(DelayedOpenLevel());
    }

    IEnumerator DelayedOpenLevel()
    {
        yield return new WaitForSeconds(0.155f);
        delayDone = true;
        OpenLevel();
    }
}
