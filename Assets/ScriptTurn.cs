using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTurn : MonoBehaviour
{
    public EnemyScript enemyScript;
    private AudioSource passAudio;
    private SpriteRenderer sprite;
    public TableCards[] tableScript;
    public MontonCartasScript mazoScript;
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
    public void EndTurn()
    {
        sprite.color = new Color(0.25f, 0.25f, 0.25f,1);
        foreach (TableCards script in tableScript)
        {
            script.DeactivateButton();
        }
    }
    public void OpenSecurityPanel(bool mana)
    {
        GameManager.autoMove = true;
        if (mana)
        {
            panelSeguridadMana.SetActive(true);

        }
        else
        {
            panelSeguridad.SetActive(true);
        }
    }
    public void CloseSecurityPanel(bool mana)
    {
        GameManager.autoMove = false;

        if (mana)
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
        mazoScript.Sacar2Random();
        foreach (TableCards script in tableScript)
        {
            if (!script.available)
            {
                script.ActivateButton();
            }
        }
    }
    private void OnMouseDown()
    {
        if (GameManager.autoMove == false && !GameManager.menu)
        {
            bool anyAttackButtonActive = false;

            foreach (var script in tableScript)
            {
                if (script.attackButton.gameObject.activeSelf)
                {
                    anyAttackButtonActive = true;
                    break;
                }
            }

            if (anyAttackButtonActive)
            {
                OpenSecurityPanel(false);
            }
            else if (CheckManaUsefull())
            {
                OpenSecurityPanel(true);

            }
            else
            {
                TerminarTurno();
            }
            animator.SetBool("On", false);

        }
    }
    private void OnMouseOver()
    {
        if (GameManager.autoMove == false && !GameManager.menu)
        {
            animator.SetBool("On", true);

        }
    }
    private void OnMouseExit()
    {
        if (GameManager.autoMove == false && !GameManager.menu)
        {
            animator.SetBool("On", false);

        }
    }
    public bool CheckManaUsefull()
    {
        CardStats[] cards = mano.GetComponentsInChildren<CardStats>();
        foreach (CardStats card in cards)
        {
            if (card.cost <= ContadoresScript.mana)
            {
                return true;
            }
        }
        return false;
    }
    public void TerminarTurno()
    {
        EndTurn();
        GameManager.autoMove = true;
        passAudio.Play();
        enemyScript.Turn1();
    }
}
