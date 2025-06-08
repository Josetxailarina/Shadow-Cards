using TMPro;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    [SerializeField] private TableSlot[] tableScripts;
    [SerializeField] private TextMeshPro lifeText;
    private Animator textAnim;
    private int life;

    private void Awake()
    {
        textAnim = lifeText.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        life = 5;
        UpdateLife();
        foreach (TableSlot scripts in tableScripts)
        {
            scripts.hasProtection = true;
        }
    }
    public void AddHealthToWall()
    {
        life += 5;
        UpdateLife();
    }
    private void UpdateLife()
    {
        lifeText.text = life.ToString();
    }
    public void TakeDamage(int Amount)
    {
        life -= Amount;
        textAnim.SetTrigger("Hit");
        UpdateLife();

        if (life <= 0)
        {
            foreach (TableSlot scripts in tableScripts) 
            { 
            scripts.hasProtection = false;
            }
            SoundManager.instance.iceBreakSound.Play();
            gameObject.SetActive(false);
        }
    }
}
