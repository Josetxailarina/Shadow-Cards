using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelStart : MonoBehaviour
{
    public Image startImg;
    public Image exitImg;
    public Sprite startOn;
    public Sprite exitOn;
    public Sprite startOff;
    public Sprite exitOff;
    public TextMeshProUGUI xText;

    public void RedX() { xText.color = Color.red; }
    public void WhiteX() { xText.color = Color.white; }

    public void StartOn() {startImg.sprite = startOn; }
    public void StartOff() { startImg.sprite = startOff; }
    public void ExitOn() { exitImg.sprite = exitOn; }
    public void ExitOff() { exitImg.sprite = exitOff; }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

}
