using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCards : MonoBehaviour
{
    public bool available = true;
    private SpriteRenderer sprite;
    public AttackButton attackButton;
    public CardStats statsCard;
    public TableCards oppositeCard;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void Attack()
    {
        if (!oppositeCard.available) //si hay una carta en frente
        {
            oppositeCard.statsCard.TakeDamage(statsCard.attack);
            print("enemy card take " + statsCard.attack + "damage");

            switch (statsCard.element1)
            {
                case Element.none:
                    break;
                case Element.fire:
                    oppositeCard.statsCard.TakeFire();

                    break;
                case Element.water:
                    oppositeCard.statsCard.TakeWater();

                    break;
                case Element.wind:
                    oppositeCard.statsCard.TakeWind();

                    break;
                default:
                    break;
            }
            switch (statsCard.element2)
            {
                case Element.none:
                    break;
                case Element.fire:
                    oppositeCard.statsCard.TakeFire();

                    break;
                case Element.water:
                    oppositeCard.statsCard.TakeWater();

                    break;
                case Element.wind:
                    oppositeCard.statsCard.TakeWind();

                    break;
                default:
                    break;
            }
        }
        else
        {
            //atacamos al boss
            print("boss take " + statsCard.attack + "damage");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.movingCard)
        {

            if (collision.CompareTag("Animal"))
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

    }
    public void GoRed()
    {
        sprite.enabled = true;
        sprite.color = Color.red;
    }
    public void GoGreen()
    {
        sprite.enabled = true;
        sprite.color = Color.green;
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
