using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Reload : MonoBehaviour
{
    private Button reloadButton;
    private TextMeshProUGUI reloadText;
    private int numberOfStakes = 1;
    public S_Sniper sniper;

    public AudioSource audioManager;
    public AudioClip reloadClunk, reloadPing;

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

            audioManager.pitch = 1.5f;
            audioManager.PlayOneShot(reloadClunk);
            StartCoroutine(DelayedSound());
        }
    }

    private void RefreshText()
    {
        reloadText.text = "Reload\n(" + numberOfStakes + " Stakes Carved)";
    }

    // Called by S_Timer
    public void CarveStake()
    {
        numberOfStakes++;
        RefreshText();
    }

    IEnumerator DelayedSound()
    {
        yield return new WaitForSeconds(0.155f);
        audioManager.pitch = 1.2f;
        audioManager.PlayOneShot(reloadPing);
    }
}
