using UnityEngine;

public class CardState : MonoBehaviour
{
    [SerializeField] public CardData cardData; // <--- ScriptableObject with card data
    
    [HideInInspector] public ElementType currentElement;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentAttack;
    [HideInInspector] public CardScript scriptCard;
    [SerializeField] private Color colorBuffText;

    public SpriteRenderer elementSpriteRenderer;

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

    private void CardDie()
    {
        currentHealth = 0;
        if (!scriptCard.tableScript.isEnemyTable)
        {
            scriptCard.tableScript.DeactivateButton();
        }
        scriptCard.tableScript.isSlotEmpty = true;
        scriptCard.tableScript.statsCard = null;
        Destroy(gameObject);
    }

    private void UpdateStats()
    {
        scriptCard.lifeText.text = currentHealth.ToString();
        scriptCard.attackText.text = currentAttack.ToString();
    }

    public void AddElement(CardState elementalCard)
    {
        ElementType elementToAdd = elementalCard.currentElement;

        currentAttack += elementalCard.cardData.attack;
        currentHealth += elementalCard.cardData.health;
        UpdateStats();
        UpdateBuffTextColor(elementToAdd);

        if (currentElement == ElementType.None)
        {
            currentElement = elementToAdd;
            elementSpriteRenderer.gameObject.SetActive(true);
            FXManager.Instance.PlayEffect(currentElement, this);
        }
        else
        {
            switch (currentElement)
            {
                case ElementType.Fire:
                    if (elementToAdd == ElementType.Wind)
                    {
                        currentElement = ElementType.Tornado;
                        FXManager.Instance.PlayEffect(ElementType.Tornado, this);
                    }
                    else if (elementToAdd == ElementType.Water)
                    {
                        currentElement = ElementType.Smoke;
                        FXManager.Instance.PlayEffect(ElementType.Smoke, this);
                    }
                    break;
                case ElementType.Water:
                    if (elementToAdd == ElementType.Wind)
                    {
                        currentElement = ElementType.Ice;
                        if (scriptCard.tableScript.iceWallScript.gameObject.activeSelf)
                        {
                            scriptCard.tableScript.iceWallScript.AddLife();
                        }
                        else
                        {
                            scriptCard.tableScript.iceWallScript.gameObject.SetActive(true);
                        }
                        FXManager.Instance.PlayEffect(ElementType.Ice, this);
                    }
                    else if (elementToAdd == ElementType.Fire)
                    {
                        currentElement = ElementType.Smoke;
                        FXManager.Instance.PlayEffect(ElementType.Smoke, this);
                    }
                    break;

                case ElementType.Wind:
                    if (elementToAdd == ElementType.Fire)
                    {
                        currentElement = ElementType.Tornado;
                        FXManager.Instance.PlayEffect(ElementType.Tornado, this);
                    }
                    else if (elementToAdd == ElementType.Water)
                    {
                        currentElement = ElementType.Ice;
                        if (scriptCard.tableScript.iceWallScript.gameObject.activeSelf)
                        {
                            scriptCard.tableScript.iceWallScript.AddLife();
                        }
                        else
                        {
                            scriptCard.tableScript.iceWallScript.gameObject.SetActive(true);
                        }
                        FXManager.Instance.PlayEffect(ElementType.Ice, this);
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
