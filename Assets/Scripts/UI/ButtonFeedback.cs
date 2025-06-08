using UnityEngine;
using UnityEngine.UI;

public class ButtonFeedback : MonoBehaviour
{
    private Image imageRender;
    [SerializeField] private Sprite onButtonSprite;
    [SerializeField] private Sprite offButtonSprite;

    private void Start()
    {
        imageRender = GetComponent<Image>();
    }

    public void SetButtonHighlight() // Called on event trigger 
    {
        imageRender.sprite = onButtonSprite;
        SoundManager.instance.selectCardSound.Play();
    }
    public void SetButtonDefault() // Called on event trigger 
    {
        imageRender.sprite = offButtonSprite;
    }
}
