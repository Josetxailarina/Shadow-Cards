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
        UpdateStats();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateStats();
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

    public void UpdateStats()
    {
        scriptCard.lifeText.text = health.ToString();
        scriptCard.attackText.text = attack.ToString();

    }

    public void TakeFire()
    {
        if (element1 == Element.wind || element2 == Element.wind)
        {
            health -= 2;
            UpdateStats();
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
            UpdateStats();
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
            UpdateStats();
            if (health <= 0)
            {
                Muerto();
            }
        }
    }

    public void AddElement(Element ElementAdded)
    {
        Vector3 position = transform.position;

        if (element1 == Element.none)
        {
            element1 = ElementAdded;
            elementObject.gameObject.SetActive(true);

            if (ElementAdded == Element.fire)
            {
                elementObject.sprite = spritesElements[0];
                SoundManager.PlayFireEffect(position);
                attack += 2;
                UpdateStats();
            }
            else if (ElementAdded == Element.wind)
            {
                elementObject.sprite = spritesElements[1];
                SoundManager.PlayWindEffect(position);
                attack += 1;
                health += 1;
                UpdateStats();

            }
            else if (ElementAdded == Element.water)
            {
                elementObject.sprite = spritesElements[2];
                SoundManager.PlayWaterEffect(position);
                health += 2;
                UpdateStats();

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
                        SoundManager.PlayTornadoEffect(position);
                        attack += 1;
                        health += 1;
                        UpdateStats();
                        
                    }
                    else if (ElementAdded == Element.water)
                    {
                        elementObject.sprite = spritesElements[4];
                        SoundManager.PlaySmokeEffect(position);
                        health += 2;
                        UpdateStats();
                    }
                    break;
                case Element.water:
                    if (ElementAdded == Element.wind)
                    {
                        elementObject.sprite = spritesElements[5];
                        scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);
                        SoundManager.PlayIceEffect(position);
                        attack += 1;
                        health += 1;
                        UpdateStats();
                    }
                    else if (ElementAdded == Element.fire)
                    {
                        elementObject.sprite = spritesElements[4];
                        SoundManager.PlaySmokeEffect(position);
                        attack += 2;
                        UpdateStats();
                    }
                    break;
                case Element.wind:
                    if (ElementAdded == Element.fire)
                    {
                        elementObject.sprite = spritesElements[3];
                        SoundManager.PlayTornadoEffect(position);
                        attack += 2;
                        UpdateStats();
                    }
                    else if (ElementAdded == Element.water)
                    {
                        elementObject.sprite = spritesElements[5];
                        scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);
                        SoundManager.PlayIceEffect(position);
                        health += 2;
                        UpdateStats();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
