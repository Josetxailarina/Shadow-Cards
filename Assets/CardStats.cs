using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    none,
    fire,
    water,
    wind,

}
public class CardStats : MonoBehaviour
{
    public Element element1;
    public Element element2;
    public int health;
    public int attack;
    public int cost;
    public CardScript scriptCard;
    public SpriteRenderer elementObject;
    public Sprite[] spritesElements;

    private void Awake()
    {
        scriptCard = GetComponentInChildren<CardScript>();
    }
    private void Start()
    {
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealth();
        if (health <= 0)
        {
            Muerto();
        }
    }
    public void Muerto()
    {
        health = 0;
        if (!scriptCard.tableScript.enemyBoard)
        {
            scriptCard.tableScript.DeactivateButton();
        }
        scriptCard.tableScript.available = true;
        scriptCard.tableScript.statsCard = null;
        Destroy(gameObject);
    }
    public void UpdateHealth()
    {
        scriptCard.lifeText.text = health.ToString();

    }
    public void TakeFire()
    {
        if (element1 == Element.wind|| element2 == Element.wind)
        {
            health -= 2;
            UpdateHealth();
            if (health <= 0)
            {
                Muerto();
            }
        }
    }
    public void TakeWater()
    {
        if (element1 == Element.fire || element2 == Element.fire)
        {
            health -= 2;
            UpdateHealth();
            if (health <= 0)
            {
                Muerto();
            }

        }
    }
    public void TakeWind()
    {
        if (element1 == Element.water || element2 == Element.water)
        {
            health -= 2;
            UpdateHealth();
            if (health <= 0)
            {
                Muerto();
            }

        }
    }
    public void AddElement(Element ElementAdded)
    {
        if (element1 == Element.none)
        {
            element1 = ElementAdded;
            elementObject.gameObject.SetActive(true);
            if (ElementAdded == Element.fire)
            {
                elementObject.sprite = spritesElements[0];
            }
            else if (ElementAdded == Element.wind)
            {
                elementObject.sprite = spritesElements[1];
            }
            else if (ElementAdded == Element.water)
            {
                elementObject.sprite = spritesElements[2];
            }
        }
        else if (element2 == Element.none)
        {
            element2 = ElementAdded;

            switch (element1)
            {
                case Element.none:
                    break;
                case Element.fire:
                    if (ElementAdded == Element.wind)
                    {
                        elementObject.sprite = spritesElements[3];
                    }
                    else if (ElementAdded == Element.water)
                    {
                        elementObject.sprite = spritesElements[4];

                    }
                    break;
                case Element.water:
                    if (ElementAdded == Element.wind)
                    {
                        elementObject.sprite = spritesElements[5];
                        scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);
                    }
                    else if (ElementAdded == Element.fire)
                    {
                        elementObject.sprite = spritesElements[4];

                    }
                    break;
                case Element.wind:
                    if (ElementAdded == Element.fire)
                    {
                        elementObject.sprite = spritesElements[3];
                    }
                    else if (ElementAdded == Element.water)
                    {
                        elementObject.sprite = spritesElements[5];
                        scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);


                    }
                    break;
                default:
                    break;
            }
        }
        
    }
}
