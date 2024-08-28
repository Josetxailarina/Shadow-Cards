using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public AudioSource audioMouse;
    public Image tryAgain;
    public Sprite tryAgainRedSprite;
    public Sprite tryAgainSprite;

    public void TurnRed()
    {
        tryAgain.sprite = tryAgainRedSprite;
        audioMouse.Play();

    }
    public void TurnWhite()
    {
        tryAgain.sprite = tryAgainSprite;
    }

}
