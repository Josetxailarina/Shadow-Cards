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

    private void Start()
    {
        passAudio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        turn = 1;
    }
    public void EndTurn()
    {
        sprite.color = new Color(0.25f, 0.25f, 0.25f,1);
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

        EndTurn();
        GameManager.autoMove = true;
        passAudio.Play();
        enemyScript.Turn1();
        }
    }
}
