using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCards : MonoBehaviour
{
    public bool available = true;
    private SpriteRenderer sprite;
    public AttackButton attackButton;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.movingCard)
        {


            if (available)
            {
                sprite.enabled = true;
                sprite.color = Color.green;
            }
            else
            {
                sprite.enabled = true;
                sprite.color = Color.red;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.enabled = false;

    }
    public void ActivateButton()
    {
        attackButton.gameObject.SetActive(true);
    }
    public void DeactivateButton()
    {
        attackButton.gameObject.SetActive(false);
    }

}
