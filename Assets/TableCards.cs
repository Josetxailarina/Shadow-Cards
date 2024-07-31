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
    public bool protection;
    public MuroScript scriptMuro;
    public bool enemyBoard;
    private AudioSource hitSound;
    public TableCards tableLeft;
    public TableCards tableRight;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        hitSound = GetComponent<AudioSource>();
    }
    public void Attack()
    {
        hitSound.Play();
        if (oppositeCard.protection)
        {
            oppositeCard.scriptMuro.TakeDamage(statsCard.attack);
        }
        else
        {
            if (!oppositeCard.available) //si hay una carta en frente
            {
                oppositeCard.statsCard.TakeDamage(statsCard.attack);
                print("enemy card take " + statsCard.attack + "damage");
                if (oppositeCard.statsCard !=null)
                {

                
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
            }
            else if(!enemyBoard)
            {
                ContadoresScript.BajarVidaBoss(statsCard.attack);
            }
            else if (enemyBoard)
            {
                ContadoresScript.BajarVida(statsCard.attack);
            }
        }
    }
    public void AttackLeft()
    {
        if(tableLeft != null)
        {
            hitSound.Play();
            if (tableLeft.oppositeCard.protection)
            {
                tableLeft.oppositeCard.scriptMuro.TakeDamage(statsCard.attack);
            }
            else
            {
                if (!tableLeft.oppositeCard.available) //si hay una carta en frente
                {
                    tableLeft.oppositeCard.statsCard.TakeDamage(statsCard.attack);
                    print("enemy card take " + statsCard.attack + "damage");
                    if (tableLeft.oppositeCard.statsCard != null)
                    {


                        switch (statsCard.element1)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                tableLeft.oppositeCard.statsCard.TakeFire();

                                break;
                            case Element.water:
                                tableLeft.oppositeCard.statsCard.TakeWater();

                                break;
                            case Element.wind:
                                tableLeft.oppositeCard.statsCard.TakeWind();

                                break;
                            default:
                                break;
                        }
                        switch (statsCard.element2)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                tableLeft.oppositeCard.statsCard.TakeFire();

                                break;
                            case Element.water:
                                tableLeft.oppositeCard.statsCard.TakeWater();

                                break;
                            case Element.wind:
                                tableLeft.oppositeCard.statsCard.TakeWind();

                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (!enemyBoard)
                {
                    ContadoresScript.BajarVidaBoss(statsCard.attack);
                }
                else if (enemyBoard)
                {
                    ContadoresScript.BajarVida(statsCard.attack);
                }
            }
        }
    }
    public void AttackRight()
    {
        if (tableRight != null)
        {
            hitSound.Play();
            if (tableRight.oppositeCard.protection)
            {
                tableRight.oppositeCard.scriptMuro.TakeDamage(statsCard.attack);
            }
            else
            {
                if (!tableRight.oppositeCard.available) //si hay una carta en frente
                {
                    tableRight.oppositeCard.statsCard.TakeDamage(statsCard.attack);
                    print("enemy card take " + statsCard.attack + "damage");
                    if (tableRight.oppositeCard.statsCard != null)
                    {
                        switch (statsCard.element1)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                tableRight.oppositeCard.statsCard.TakeFire();
                                break;
                            case Element.water:
                                tableRight.oppositeCard.statsCard.TakeWater();
                                break;
                            case Element.wind:
                                tableRight.oppositeCard.statsCard.TakeWind();
                                break;
                            default:
                                break;
                        }
                        switch (statsCard.element2)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                tableRight.oppositeCard.statsCard.TakeFire();
                                break;
                            case Element.water:
                                tableRight.oppositeCard.statsCard.TakeWater();
                                break;
                            case Element.wind:
                                tableRight.oppositeCard.statsCard.TakeWind();
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (!enemyBoard)
                {
                    ContadoresScript.BajarVidaBoss(statsCard.attack);
                }
                else if (enemyBoard)
                {
                    ContadoresScript.BajarVida(statsCard.attack);
                }
            }
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
