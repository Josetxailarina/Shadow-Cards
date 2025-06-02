using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadoresScript : MonoBehaviour
{
    public static int life = 20;
    public static int bossLife = 20;
    public static int mana = 1;
    public static TextMeshPro lifeText;
    public static TextMeshPro bossLifeText;
    public static TextMeshPro manaText;
    public static Animator lifeAnim;
    public static Animator bossAnim;
    public static Animator manaAnim;
    public TextMeshPro lifeRef;
    public TextMeshPro bossLifeRef;
    public TextMeshPro manaRef;
    public GameObject losePanelRef;
    public GameObject winPanelRef;
    public static GameObject winPanel;
    public static GameObject losePanel;

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
        winPanel = winPanelRef;
        losePanel = losePanelRef;
        life = 30;
        bossLife = 30;
        mana = 1;
        manaText.text = mana.ToString();
        lifeText.text = life.ToString();
        bossLifeText.text = bossLife.ToString();
        winPanel.SetActive(false);
        losePanel.SetActive(false);

    }
    public static void UpdateStats()
    {
        manaText.text = mana.ToString();
        lifeText.text = life.ToString();
        bossLifeText.text = bossLife.ToString();
    }

    public static void BajarVida(int Cantidad)
    {
        life -= Cantidad;
        lifeText.text = life.ToString();
        lifeAnim.SetTrigger("Hit");
        if (life <= 0) 
        { 
            losePanel.SetActive(true);
            GameManager.gameState = GameState.Menu;

        }
    }
    public static void BajarVidaBoss(int Cantidad)
    {
        bossLife -= Cantidad;
        bossLifeText.text = bossLife.ToString();
        bossAnim.SetTrigger("Hit");
        if (bossLife <= 0) 
        {
            winPanel.SetActive(true);
            GameManager.gameState = GameState.Menu;
        }

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
