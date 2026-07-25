using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Reload : MonoBehaviour
{
    private Button reloadButton;
    private TextMeshProUGUI reloadText;
    private int numberOfStakes = 1;
    public S_Sniper sniper;

    void Start()
    {
        reloadButton = GetComponent<Button>();
        reloadText = GetComponentInChildren<TextMeshProUGUI>();

        reloadButton.onClick.AddListener(Reload);
    }

    private void Reload()
    {
        if (numberOfStakes >= 1)
        {
            numberOfStakes--;
            sniper.Reload();

            RefreshText();
        }
    }

    private void RefreshText()
    {
        reloadText.text = "Reload\n(" + numberOfStakes + " Stakes Carved)";
    }
}
