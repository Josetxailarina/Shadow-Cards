using UnityEngine;

public class CardStats : MonoBehaviour
{
    [SerializeField] public CardData cardData; // <--- ScriptableObject with card data
    
    [HideInInspector] public ElementType currentElement;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentAttack;
    public CardScript scriptCard;
    public SpriteRenderer elementSpriteRenderer;
    public Sprite[] spritesElements;
    public Color colorBuffText;

    private void Awake()
    {
        scriptCard = GetComponentInChildren<CardScript>();
        currentAttack = cardData.attack;
        currentHealth = cardData.health;
        currentElement = cardData.element;
    }

    private void Start()
    {
        UpdateStats();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateStats();
        if (currentHealth <= 0)
        {
            CardDie();
        }
    }

    public void CardDie()
    {
        currentHealth = 0;
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
        scriptCard.lifeText.text = currentHealth.ToString();
        scriptCard.attackText.text = currentAttack.ToString();
    }

    public void AddElement(CardStats elementalCard)
    {
        Vector3 position = transform.position;
        ElementType elementToAdd = elementalCard.currentElement;
        currentAttack += elementalCard.cardData.attack;
        currentHealth += elementalCard.cardData.health;
        UpdateStats();
        UpdateBuffTextColor(elementToAdd);

        if (currentElement == ElementType.None)
        {
            currentElement = elementToAdd;
            elementSpriteRenderer.gameObject.SetActive(true);

            if (elementToAdd == ElementType.Fire)
            {
                elementSpriteRenderer.sprite = spritesElements[0];
                FXManager.Instance.PlayEffect(ElementType.Fire, position);
            }
            else if (elementToAdd == ElementType.Wind)
            {
                elementSpriteRenderer.sprite = spritesElements[1];
                FXManager.Instance.PlayEffect(ElementType.Wind, position);
            }
            else if (elementToAdd == ElementType.Water)
            {
                elementSpriteRenderer.sprite = spritesElements[2];
                FXManager.Instance.PlayEffect(ElementType.Water, position);
            }
        }
        else
        {
            switch (currentElement)
            {
                case ElementType.Fire:
                    if (elementToAdd == ElementType.Wind)
                    {
                        currentElement = ElementType.Tornado;
                        elementSpriteRenderer.sprite = spritesElements[3];
                        FXManager.Instance.PlayEffect(ElementType.Tornado, position);
                    }
                    else if (elementToAdd == ElementType.Water)
                    {
                        currentElement = ElementType.Smoke;
                        elementSpriteRenderer.sprite = spritesElements[4];
                        FXManager.Instance.PlayEffect(ElementType.Smoke, position);
                    }
                    break;
                case ElementType.Water:
                    if (elementToAdd == ElementType.Wind)
                    {
                        currentElement = ElementType.Ice;
                        elementSpriteRenderer.sprite = spritesElements[5];
                        if (scriptCard.tableScript.scriptMuro.gameObject.activeSelf)
                        {
                            scriptCard.tableScript.scriptMuro.AddLife();
                        }
                        else
                        {
                            scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);
                        }
                        FXManager.Instance.PlayEffect(ElementType.Ice, position);
                    }
                    else if (elementToAdd == ElementType.Fire)
                    {
                        currentElement = ElementType.Smoke;
                        elementSpriteRenderer.sprite = spritesElements[4];
                        FXManager.Instance.PlayEffect(ElementType.Smoke, position);
                    }
                    break;

                case ElementType.Wind:
                    if (elementToAdd == ElementType.Fire)
                    {
                        currentElement = ElementType.Tornado;
                        elementSpriteRenderer.sprite = spritesElements[3];
                        FXManager.Instance.PlayEffect(ElementType.Tornado, position);
                    }
                    else if (elementToAdd == ElementType.Water)
                    {
                        currentElement = ElementType.Ice;
                        elementSpriteRenderer.sprite = spritesElements[5];
                        if (scriptCard.tableScript.scriptMuro.gameObject.activeSelf)
                        {
                            scriptCard.tableScript.scriptMuro.AddLife();
                        }
                        else
                        {
                            scriptCard.tableScript.scriptMuro.gameObject.SetActive(true);
                        }
                        FXManager.Instance.PlayEffect(ElementType.Ice, position);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public bool CanAddElement(ElementType element)
    {
        if (currentElement != element && (currentElement == ElementType.Fire || currentElement == ElementType.Wind || currentElement == ElementType.Water || currentElement == ElementType.None))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void UpdateBuffTextColor(ElementType ElementAdded)
    {
        switch (ElementAdded)
        {
            case ElementType.None:
                break;

            case ElementType.Fire:
                scriptCard.attackText.color = colorBuffText;
                break;

            case ElementType.Water:
                scriptCard.lifeText.color = colorBuffText;
                break;

            case ElementType.Wind:
                scriptCard.lifeText.color = colorBuffText;
                scriptCard.attackText.color = colorBuffText;
                break;

            default:
                break;
        }
    }
}
