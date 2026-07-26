using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_Menu : MonoBehaviour
{
    private Button menuButton;

    public AudioSource audioManager;
    public AudioClip select;

    private bool delayDone = false;

    void Start()
    {
        menuButton = GetComponent<Button>();

        menuButton.onClick.AddListener(OpenMenu);
    }

    private void OpenMenu()
    {
        if (delayDone)
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            audioManager.pitch = 2;
            audioManager.PlayOneShot(select);
        }

        StartCoroutine(DelayedOpenMenu());
    }

    IEnumerator DelayedOpenMenu()
    {
        yield return new WaitForSeconds(0.155f);
        delayDone = true;
        OpenMenu();
    }
}
