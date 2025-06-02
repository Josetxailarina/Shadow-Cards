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

    public void Attack(Direction direction)
    {
        TableCards targetCard = GetTargetCard(direction);
        if (targetCard == null) return;

        hitSound.Play();

        if (targetCard.protection)
        {
            targetCard.scriptMuro.TakeDamage(statsCard.attack);
        }
        else
        {
            HandleDamage(targetCard);
        }
    }

    private TableCards GetTargetCard(Direction direction)
    {
        switch (direction)
        {
            case Direction.Center:
                return oppositeCard;
            case Direction.Left:
                return tableLeft?.oppositeCard;
            case Direction.Right:
                return tableRight?.oppositeCard;
            default:
                return null;
        }
    }

    private void HandleDamage(TableCards targetCard)
    {
        if (!targetCard.available) // Si hay una carta en frente
        {
            int totalDamage = statsCard.attack;
            //totalDamage += CalculateElementalDamage(targetCard);

            targetCard.statsCard.TakeDamage(totalDamage);


            ShowDamage(targetCard.transform.position, totalDamage);
        }
        else if (!enemyBoard)
        {
            ContadoresScript.BajarVidaBoss(statsCard.attack);
            ShowDamage(targetCard.transform.position, statsCard.attack);
        }
        else
        {
            ContadoresScript.BajarVida(statsCard.attack);
            ShowDamage(targetCard.transform.position, statsCard.attack);
        }
    }

    private int CalculateElementalDamage(TableCards targetCard)
    {
        int elementalBonus = 0;

        elementalBonus += GetElementalBonus(statsCard.element1, targetCard);
        elementalBonus += GetElementalBonus(statsCard.element2, targetCard);

        return elementalBonus;
    }

    private int GetElementalBonus(Element element, TableCards targetCard)
    {
        switch (element)
        {
            case Element.fire:
                if (IsAffectedByElement(targetCard, Element.wind))  return 2;
                break;
            case Element.water:
                if (IsAffectedByElement(targetCard, Element.fire)) return 2;
                break;
            case Element.wind:
                if (IsAffectedByElement(targetCard, Element.water)) return 2;
                break;
        }
        return 0;
    }

    private bool IsAffectedByElement(TableCards targetCard, Element element)
    {
        return targetCard.statsCard?.element1 == element || targetCard.statsCard?.element2 == element;
    }

    private void ShowDamage(Vector3 position, int totalDamage)
    {
        dmgText.text = "-" + totalDamage.ToString();
        dmgText.transform.parent.transform.position = position;
        dmgAnimator.SetTrigger("Hit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.movingCard)
        {
            if (collision.CompareTag("Animal"))
            {
                 sprite.enabled = true;
                 sprite.color = Color.green;
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

public enum Direction
{
    Center,
    Left,
    Right
}
