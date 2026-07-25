using UnityEngine;
using UnityEngine.UI;

public class S_Exit : MonoBehaviour
{
    private Button exitButton;

    void Start()
    {
        exitButton = GetComponent<Button>();

        exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
