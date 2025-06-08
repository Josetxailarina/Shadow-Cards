using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelStart : MonoBehaviour
{
    [SerializeField] private Image startButtonImg;
    [SerializeField] private Image tutorialButtonImg;
    [SerializeField] private Sprite startHighlightSprite;
    [SerializeField] private Sprite tutorialHighlightSprite;
    [SerializeField] private Sprite startDefaultSprite;
    [SerializeField] private Sprite tutorialDefaultSprite;
    [SerializeField] private TextMeshProUGUI xButtonTMP;

    // Functions called from event triggers in the UI buttons
    public void RedX() { xButtonTMP.color = Color.red; }
    public void WhiteX() { xButtonTMP.color = Color.white; }
    public void StartOn() {startButtonImg.sprite = startHighlightSprite; }
    public void StartOff() { startButtonImg.sprite = startDefaultSprite; }
    public void ExitOn() { tutorialButtonImg.sprite = tutorialHighlightSprite; }
    public void ExitOff() { tutorialButtonImg.sprite = tutorialDefaultSprite; }
    public void ExitGame() { Application.Quit(); }
    public void LoadGame() { SceneManager.LoadScene(1);  }

}
