using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_Start : MonoBehaviour
{
    private Button startButton;

    void Start()
    {
        startButton = GetComponent<Button>();

        startButton.onClick.AddListener(OpenLevel);
    }

    private void OpenLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
