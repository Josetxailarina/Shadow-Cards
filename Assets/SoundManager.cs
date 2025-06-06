using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource iceBreakSound;
    public AudioSource useCardSound;
    public AudioSource selectCardSound;
    public AudioSource takeCardSound;
    public AudioSource attackSound;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

}
