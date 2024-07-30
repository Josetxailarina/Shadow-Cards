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
    public CardScript scriptCard;
    public SpriteRenderer elementObject;
    public Sprite[] spritesElements;

    private void Awake()
    {
        scriptCard = GetComponentInChildren<CardScript>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void TakeFire()
    {
        if (element1 == Element.wind|| element2 == Element.wind)
        {
            health -= 2;
            print("fire damage");
        }
    }
    public void TakeWater()
    {
        if (element1 == Element.fire || element2 == Element.fire)
        {
            health -= 2;
            print("water damage");

        }
    }
    public void TakeWind()
    {
        if (element1 == Element.water || element2 == Element.water)
        {
            health -= 2;
            print("wind damage");

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

                    }
                    break;
                default:
                    break;
            }
        }
        
    }
}
