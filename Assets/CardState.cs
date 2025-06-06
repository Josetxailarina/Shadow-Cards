using TMPro;
using UnityEngine;

public class CardState : MonoBehaviour
{
    public CardData cardData; // <--- ScriptableObject with card data
    public SpriteRenderer elementSpriteRenderer;

    [HideInInspector] public ElementType currentElement;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentAttack;
    [HideInInspector] public CardBehavior scriptCard;
    [SerializeField] private Color colorBuffText;
    [SerializeField] private TextMeshPro attackText;
    [SerializeField] private TextMeshPro healthText;


    private void Awake()
    {
        scriptCard = GetComponentInChildren<CardBehavior>();
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
            HandleCardDeath();
        }
    }
    public void AddElement(CardState elementalCard)
    {
        ElementType elementToAdd = elementalCard.cardData.element;

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
            currentElement = CombineElements(currentElement, elementToAdd);
            FXManager.Instance.PlayEffect(currentElement, this);
            if (currentElement == ElementType.Ice)
            {
                scriptCard.currentTableSlot.AddIceWall();
            }

        }
    }
    private ElementType CombineElements(ElementType baseElement, ElementType elementToAdd)
    {
        if (baseElement == ElementType.Fire && elementToAdd == ElementType.Wind ||
            baseElement == ElementType.Wind && elementToAdd == ElementType.Fire)
            return ElementType.Tornado;

        if (baseElement == ElementType.Fire && elementToAdd == ElementType.Water ||
            baseElement == ElementType.Water && elementToAdd == ElementType.Fire)
            return ElementType.Smoke;

        if (baseElement == ElementType.Water && elementToAdd == ElementType.Wind ||
            baseElement == ElementType.Wind && elementToAdd == ElementType.Water)
            return ElementType.Ice;

        return baseElement;
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
                attackText.color = colorBuffText;
                break;

            case ElementType.Water:
                healthText.color = colorBuffText;
                break;

            case ElementType.Wind:
                healthText.color = colorBuffText;
                attackText.color = colorBuffText;
                break;

            default:
                break;
        }
    }

    private void HandleCardDeath()
    {
        currentHealth = 0;
        if (!scriptCard.currentTableSlot.isEnemySlot)
        {
            scriptCard.currentTableSlot.DisableAttackButton();
        }
        scriptCard.currentTableSlot.isSlotEmpty = true;
        scriptCard.currentTableSlot.currentCardInSlot = null;
        Destroy(gameObject);
    }

    private void UpdateStats()
    {
        healthText.text = currentHealth.ToString();
        attackText.text = currentAttack.ToString();
    }
}
