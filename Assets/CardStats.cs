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
    public Color colorBuffText;

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
        switch (ElementAdded)
        {
            case Element.none:
                break;
            case Element.fire:
                scriptCard.attackText.color = colorBuffText;
                break;
            case Element.water:
                scriptCard.lifeText.color = colorBuffText;

                break;
            case Element.wind:
                scriptCard.lifeText.color = colorBuffText;
                scriptCard.attackText.color = colorBuffText;

                break;
            default:
                break;
        }
        if (element1 == Element.none)
        {
            element1 = ElementAdded;
            elementObject.gameObject.SetActive(true);

            if (ElementAdded == Element.fire)
            {
                elementObject.sprite = spritesElements[0];
                FXManager.Instance.PlayEffect(ParticleType.Fire,position);
                attack += 2;
                UpdateStats();
            }
            else if (ElementAdded == Element.wind)
            {
                elementObject.sprite = spritesElements[1];
                FXManager.Instance.PlayEffect(ParticleType.Wind, position);
                attack += 1;
                health += 1;
                UpdateStats();

            }
            else if (ElementAdded == Element.water)
            {
                elementObject.sprite = spritesElements[2];
                FXManager.Instance.PlayEffect(ParticleType.Water, position);
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
                        FXManager.Instance.PlayEffect(ParticleType.Tornado, position);
                        attack += 1;
                        health += 1;
                        UpdateStats();
                        
                    }
                    else if (ElementAdded == Element.water)
                    {
                        elementObject.sprite = spritesElements[4];
                        FXManager.Instance.PlayEffect(ParticleType.Smoke, position);
                        health += 2;
                        UpdateStats();
                    }
                    break;
                case Element.water:
                    if (ElementAdded == Element.wind)
                    {
                        elementObject.sprite = spritesElements[5];
                        if (scriptCard.tableScript.scriptMuro.gameObject.activeSelf)
                        {
                            scriptCard.tableScript.scriptMuro.AddLife();
                        }
                        else
                        {
                            scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);
                        }                        
                        FXManager.Instance.PlayEffect(ParticleType.Ice, position);
                        attack += 1;
                        health += 1;
                        UpdateStats();
                    }
                    else if (ElementAdded == Element.fire)
                    {
                        elementObject.sprite = spritesElements[4];
                        FXManager.Instance.PlayEffect(ParticleType.Smoke, position);
                        attack += 2;
                        UpdateStats();
                    }
                    break;
                case Element.wind:
                    if (ElementAdded == Element.fire)
                    {
                        elementObject.sprite = spritesElements[3];
                        FXManager.Instance.PlayEffect(ParticleType.Tornado, position);
                        attack += 2;
                        UpdateStats();
                    }
                    else if (ElementAdded == Element.water)
                    {
                        elementObject.sprite = spritesElements[5];
                        if (scriptCard.tableScript.scriptMuro.gameObject.activeSelf)
                        {
                            scriptCard.tableScript.scriptMuro.AddLife();
                        }
                        else
                        {
                            scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);
                        }
                        FXManager.Instance.PlayEffect(ParticleType.Ice, position);
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
