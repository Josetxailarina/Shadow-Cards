using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTurn : MonoBehaviour
{
    public EnemyScript enemyScript;
    private AudioSource passAudio;
    private SpriteRenderer sprite;

    private void Start()
    {
        passAudio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }
    public void GoDark()
    {
        sprite.color = new Color(0.25f, 0.25f, 0.25f,1);
    }
    public void GoLight()
    {
        sprite.color = new Color(1f, 1f, 1f, 1);
    }
    private void OnMouseDown()
    {
        if (GameManager.autoMove == false)
        {

        GoDark();
        GameManager.autoMove = true;
        passAudio.Play();
        enemyScript.Turn1();
        }
    }
}
