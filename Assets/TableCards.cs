using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshPro dmgText;
    public Animator dmgAnimator;

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
                int totalDamage = 0;
                oppositeCard.statsCard.TakeDamage(statsCard.attack);
                totalDamage = statsCard.attack;
                print("enemy card take " + statsCard.attack + "damage");
                if (oppositeCard.statsCard !=null)
                {


                    switch (statsCard.element1)
                    {
                        case Element.none:
                            break;
                        case Element.fire:
                            if (oppositeCard.statsCard.element1 == Element.water || oppositeCard.statsCard.element2 == Element.water)
                            {
                                oppositeCard.statsCard.TakeFire();
                                totalDamage += 2;
                            }
                            break;
                        case Element.water:
                            if (oppositeCard.statsCard.element1 == Element.wind || oppositeCard.statsCard.element2 == Element.wind)
                            {
                                oppositeCard.statsCard.TakeWater();
                                totalDamage += 2;
                            }
                            break;
                        case Element.wind:
                            if (oppositeCard.statsCard.element1 == Element.fire || oppositeCard.statsCard.element2 == Element.fire)
                            {
                                oppositeCard.statsCard.TakeWind();
                                totalDamage += 2;
                            }
                            break;
                        default:
                            break;
                    }
                    switch (statsCard.element2)
                {
                    case Element.none:
                        break;
                    case Element.fire:
                            if (oppositeCard.statsCard.element1 == Element.wind || oppositeCard.statsCard.element2 == Element.wind)
                            {
                                oppositeCard.statsCard.TakeFire();
                                totalDamage += 2;
                            }
                            break;
                    case Element.water:
                            if (oppositeCard.statsCard.element1 == Element.fire || oppositeCard.statsCard.element2 == Element.fire)
                            {
                                oppositeCard.statsCard.TakeWater();
                                totalDamage += 2;
                            }
                            break;
                    case Element.wind:
                            if (oppositeCard.statsCard.element1 == Element.water || oppositeCard.statsCard.element2 == Element.water)
                            {
                                oppositeCard.statsCard.TakeWind();
                                totalDamage += 2;
                            }
                            break;
                    default:
                        break;
                }
                }
                dmgText.text = "-"+totalDamage.ToString();
                dmgText.transform.parent.transform.position = oppositeCard.transform.position;
                dmgAnimator.SetTrigger("Hit");
            }
            else if(!enemyBoard)
            {
                ContadoresScript.BajarVidaBoss(statsCard.attack);
                dmgText.text = "-" + statsCard.attack.ToString();
                dmgText.transform.parent.transform.position = oppositeCard.transform.position;
                dmgAnimator.SetTrigger("Hit");
            }
            else if (enemyBoard)
            {
                ContadoresScript.BajarVida(statsCard.attack);
                dmgText.text = "-" + statsCard.attack.ToString();
                dmgText.transform.parent.transform.position = oppositeCard.transform.position;
                dmgAnimator.SetTrigger("Hit");
            }
        }
    }
    public void AttackLeft()
    {
        if (tableLeft != null)
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
                        int totalDamage = 0;

                        switch (statsCard.element1)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                if (tableLeft.oppositeCard.statsCard.element1 == Element.water || tableLeft.oppositeCard.statsCard.element2 == Element.water)
                                {
                                    tableLeft.oppositeCard.statsCard.TakeFire();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.water:
                                if (tableLeft.oppositeCard.statsCard.element1 == Element.wind || tableLeft.oppositeCard.statsCard.element2 == Element.wind)
                                {
                                    tableLeft.oppositeCard.statsCard.TakeWater();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.wind:
                                if (tableLeft.oppositeCard.statsCard.element1 == Element.fire || tableLeft.oppositeCard.statsCard.element2 == Element.fire)
                                {
                                    tableLeft.oppositeCard.statsCard.TakeWind();
                                    totalDamage += 2;
                                }
                                break;
                            default:
                                break;
                        }
                        switch (statsCard.element2)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                if (tableLeft.oppositeCard.statsCard.element1 == Element.water || tableLeft.oppositeCard.statsCard.element2 == Element.water)
                                {
                                    tableLeft.oppositeCard.statsCard.TakeFire();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.water:
                                if (tableLeft.oppositeCard.statsCard.element1 == Element.wind || tableLeft.oppositeCard.statsCard.element2 == Element.wind)
                                {
                                    tableLeft.oppositeCard.statsCard.TakeWater();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.wind:
                                if (tableLeft.oppositeCard.statsCard.element1 == Element.fire || tableLeft.oppositeCard.statsCard.element2 == Element.fire)
                                {
                                    tableLeft.oppositeCard.statsCard.TakeWind();
                                    totalDamage += 2;
                                }
                                break;
                            default:
                                break;
                        }
                        dmgText.text = "-" + totalDamage.ToString();
                        dmgText.transform.parent.transform.position = tableLeft.oppositeCard.transform.position;
                        dmgAnimator.SetTrigger("Hit");
                    }
                    
                }
                else if (!enemyBoard)
                {
                    ContadoresScript.BajarVidaBoss(statsCard.attack);
                    dmgText.text = "-" + statsCard.attack.ToString();
                    dmgText.transform.parent.transform.position = tableLeft.oppositeCard.transform.position;
                    dmgAnimator.SetTrigger("Hit");
                }
                else if (enemyBoard)
                {
                    ContadoresScript.BajarVida(statsCard.attack);
                    dmgText.text = "-" + statsCard.attack.ToString();
                    dmgText.transform.parent.transform.position = tableLeft.oppositeCard.transform.position;
                    dmgAnimator.SetTrigger("Hit");
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
                        int totalDamage = 0;
                        switch (statsCard.element1)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                if (tableRight.oppositeCard.statsCard?.element1 == Element.water || tableRight.oppositeCard.statsCard?.element2 == Element.water)
                                {
                                    tableRight.oppositeCard.statsCard.TakeFire();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.water:
                                if (tableRight.oppositeCard.statsCard?.element1 == Element.wind || tableRight.oppositeCard.statsCard?.element2 == Element.wind)
                                {
                                    tableRight.oppositeCard.statsCard.TakeWater();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.wind:
                                if (tableRight.oppositeCard.statsCard?.element1 == Element.fire || tableRight.oppositeCard.statsCard?.element2 == Element.fire)
                                {
                                    tableRight.oppositeCard.statsCard.TakeWind();
                                    totalDamage += 2;
                                }
                                break;
                            default:
                                break;
                        }
                        switch (statsCard.element2)
                        {
                            case Element.none:
                                break;
                            case Element.fire:
                                if (tableRight.oppositeCard.statsCard?.element1 == Element.water || tableRight.oppositeCard.statsCard?.element2 == Element.water)
                                {
                                    tableRight.oppositeCard.statsCard.TakeFire();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.water:
                                if (tableRight.oppositeCard.statsCard?.element1 == Element.wind || tableRight.oppositeCard.statsCard?.element2 == Element.wind)
                                {
                                    tableRight.oppositeCard.statsCard.TakeWater();
                                    totalDamage += 2;
                                }
                                break;
                            case Element.wind:
                                if (tableRight.oppositeCard.statsCard?.element1 == Element.fire || tableRight.oppositeCard.statsCard?.element2 == Element.fire)
                                {
                                    tableRight.oppositeCard.statsCard.TakeWind();
                                    totalDamage += 2;
                                }
                                break;
                            default:
                                break;
                        }
                        dmgText.text = "-" + totalDamage.ToString();
                        dmgText.transform.parent.transform.position = tableRight.oppositeCard.transform.position;
                        dmgAnimator.SetTrigger("Hit");
                    }
                }
                else if (!enemyBoard)
                {
                    ContadoresScript.BajarVidaBoss(statsCard.attack);
                    dmgText.text = "-" + statsCard.attack.ToString();
                    dmgText.transform.parent.transform.position = tableRight.oppositeCard.transform.position;
                    dmgAnimator.SetTrigger("Hit");
                }
                else if (enemyBoard)
                {
                    ContadoresScript.BajarVida(statsCard.attack);
                    dmgText.text = "-" + statsCard.attack.ToString();
                    dmgText.transform.parent.transform.position = tableRight.oppositeCard.transform.position;
                    dmgAnimator.SetTrigger("Hit");
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
