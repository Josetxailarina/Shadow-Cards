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

    private void Start()
    {
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
    public void OpenSecurityPanel()
    {
        GameManager.autoMove = true;
        panelSeguridad.SetActive(true);
    }
    public void CloseSecurityPanel()
    {
        GameManager.autoMove = false;

        panelSeguridad.SetActive(false);
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
        if (GameManager.autoMove == false)
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
                OpenSecurityPanel();
            }
            else
            {
                TerminarTurno();
            }
        }
    }
    public void TerminarTurno()
    {
        EndTurn();
        GameManager.autoMove = true;
        passAudio.Play();
        enemyScript.Turn1();
    }
}
