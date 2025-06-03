using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTurn : MonoBehaviour
{
    public EnemyScript enemyScript;
    private AudioSource passAudio;
    private SpriteRenderer sprite;
    public TableCards[] playerTableCards;
    public PlayerDeck mazoScript;
    public static int turn;
    public GameObject panelSeguridad;
    public GameObject panelSeguridadMana;
    private Animator animator;
    public GameObject mano;

    private void Start()
    {
        animator = GetComponent<Animator>();
        passAudio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        turn = 1;
    }

    private void OnMouseDown()
    {
        if (GameManager.gameState == GameState.Play)
        {
            if (AnyAttackAvailable())
            {
                OpenWarningPanel(false);
            }
            else if (CheckManaUsefull())
            {
                OpenWarningPanel(true);
            }
            else
            {
                EndTurn();
            }
            animator.SetBool("On", false);

        }
    }
    private void OnMouseOver()
    {
        if (GameManager.gameState == GameState.Play)
        {
            animator.SetBool("On", true);
        }
    }
    private void OnMouseExit()
    {
        if (GameManager.gameState == GameState.Play)
        {
            animator.SetBool("On", false);
        }
    }
    public void OpenWarningPanel(bool useManaPanel)
    {
        GameManager.gameState = GameState.AutoMove;
        if (useManaPanel)
        {
            panelSeguridadMana.SetActive(true);
        }
        else
        {
            panelSeguridad.SetActive(true);
        }
    }
    public void CloseSecurityPanel(bool useManaPanel)
    {
        GameManager.gameState = GameState.Play;

        if (useManaPanel)
        {
            panelSeguridadMana.SetActive(false);

        }
        else
        {
            panelSeguridad.SetActive(false);
        }
    }
    public void NewTurn()
    {
        turn++;
        if (turn > 10)
        {
            ContadoresScript.mana = 10;

        }
        else
        {
            ContadoresScript.mana = turn;

        }
        ContadoresScript.UpdateStats();
        sprite.color = new Color(1f, 1f, 1f, 1);
        StartCoroutine(mazoScript.DrawSomeCards(2));
        foreach (TableCards script in playerTableCards)
        {
            if (!script.available)
            {
                script.ActivateButton();
            }
        }
    }
    

    private bool AnyAttackAvailable()
    {
        foreach (var script in playerTableCards)
        {
            if (script.attackButton.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

   
    public bool CheckManaUsefull()
    {
        CardStats[] cards = mano.GetComponentsInChildren<CardStats>();
        foreach (CardStats card in cards)
        {
            if (card.cardData.cost <= ContadoresScript.mana)
            {
                return true;
            }
        }
        return false;
    }
    public void EndTurn()
    {
        sprite.color = new Color(0.25f, 0.25f, 0.25f, 1);
        foreach (TableCards script in playerTableCards)
        {
            script.DeactivateButton();
        }
        GameManager.gameState = GameState.AutoMove;
        passAudio.Play();
        enemyScript.Turn1();
    }
}
