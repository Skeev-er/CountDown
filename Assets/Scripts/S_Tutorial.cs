using UnityEngine;
using UnityEngine.UI;

public class S_Tutorial : MonoBehaviour
{
    private Button tutorialButton;
    public GameObject tutorialPanel;

    public GameObject reloadButton;

    public AudioSource audioManager;
    public AudioClip select;

    void Start()
    {
        tutorialButton = GetComponent<Button>();

        tutorialButton.onClick.AddListener(OpenTutorial);
    }

    private void OpenTutorial()
    {
        audioManager.pitch = 1.5f;
        audioManager.PlayOneShot(select);

        // Open panel if closed
        if (!tutorialPanel.activeSelf)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0;

            if (reloadButton != null)
            {
                reloadButton.SetActive(false);
            }
        }
        // Close panel if open
        else
        {
            tutorialPanel.SetActive(false);
            Time.timeScale = 1;

            if (reloadButton != null)
            {
                reloadButton.SetActive(true);
            }
        }
    }
}
