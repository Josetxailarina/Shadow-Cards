using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadoresScript : MonoBehaviour
{
    public static int life = 20;
    public static int bossLife = 20;
    public static int mana = 5;
    public static TextMeshPro lifeText;
    public static TextMeshPro bossLifeText;
    public static TextMeshPro manaText;
    public static Animator lifeAnim;
    public static Animator bossAnim;
    public static Animator manaAnim;
    public TextMeshPro lifeRef;
    public TextMeshPro bossLifeRef;
    public TextMeshPro manaRef;
    public static AudioSource deniedSound;
    // Start is called before the first frame update
    void Start()
    {
        deniedSound = GetComponent<AudioSource>();
        lifeText = lifeRef;
        bossLifeText = bossLifeRef;
        manaText = manaRef;
        lifeAnim = lifeRef.GetComponent<Animator>();
        bossAnim = bossLifeRef.GetComponent<Animator>();
        manaAnim = manaRef.GetComponent<Animator>();
        manaText.text = mana.ToString();
        lifeText.text = life.ToString();
        bossLifeText.text = bossLife.ToString();

    }

    public static void BajarVida(int Cantidad)
    {
        life -= Cantidad;
        lifeText.text = life.ToString();
        lifeAnim.SetTrigger("Hit");
    }
    public static void BajarVidaBoss(int Cantidad)
    {
        bossLife -= Cantidad;
        bossLifeText.text = bossLife.ToString();
        bossAnim.SetTrigger("Hit");
    }
    public static void BajarMana(int Cantidad)
    {
        mana -= Cantidad;
        manaText.text = mana.ToString();

    }
    public static void ManaAnim()
    {
       manaAnim.SetTrigger("Hit");
deniedSound.Play();
    }
}
