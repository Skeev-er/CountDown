using UnityEngine;
using UnityEngine.UI;

public class S_Tutorial : MonoBehaviour
{
    private Button tutorialButton;
    public GameObject tutorialPanel;

    void Start()
    {
        tutorialButton = GetComponent<Button>();

        tutorialButton.onClick.AddListener(OpenTutorial);
    }

    private void OpenTutorial()
    {
        tutorialPanel.SetActive(!tutorialPanel.activeSelf);
    }
}
