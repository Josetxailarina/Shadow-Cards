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
            targetCard.scriptMuro.TakeDamage(statsCard.currentAttack);
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
            int totalDamage = statsCard.currentAttack;
            //totalDamage += CalculateElementalDamage(targetCard);

            targetCard.statsCard.TakeDamage(totalDamage);


            ShowDamage(targetCard.transform.position, totalDamage);
        }
        else if (!enemyBoard)
        {
            ContadoresScript.BajarVidaBoss(statsCard.currentAttack);
            ShowDamage(targetCard.transform.position, statsCard.currentAttack);
        }
        else
        {
            ContadoresScript.BajarVida(statsCard.currentAttack);
            ShowDamage(targetCard.transform.position, statsCard.currentAttack);
        }
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
