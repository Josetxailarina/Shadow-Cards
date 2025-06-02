using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MuroScript : MonoBehaviour
{
    public TableCards[] tableScripts;
    public TextMeshPro lifeText;
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
        foreach (TableCards scripts in tableScripts)
        {
            scripts.protection = true;
        }
    }
    public void AddLife()
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
            foreach (TableCards scripts in tableScripts) 
            { 
            scripts.protection = false;
            }
            SoundManager.instance.iceBreakSound.Play();
            gameObject.SetActive(false);
        }
    }
}
