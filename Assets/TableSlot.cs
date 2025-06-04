using TMPro;
using UnityEngine;

public class TableSlot : MonoBehaviour
{
    [HideInInspector] public CardState statsCard;
    [HideInInspector] public bool isSlotEmpty = true;
    [HideInInspector] public bool hasProtection = false;

    public bool isEnemyTable;

    public AttackButton attackButton;
    public TableSlot oppositeTableSlot;
    public IceWall iceWallScript;
    public TableSlot leftTableSlot;
    public TableSlot rightTableSlot;


    private AudioSource hitSound;
    private SpriteRenderer slotSpriteRenderer;

    private void Start()
    {
        slotSpriteRenderer = GetComponent<SpriteRenderer>();
        hitSound = GetComponent<AudioSource>();
    }

    public void Attack(Direction direction)
    {
        TableSlot targetCard = GetTargetCard(direction);
        if (targetCard == null) return;

        hitSound.Play();

        if (targetCard.hasProtection)
        {
            targetCard.iceWallScript.TakeDamage(statsCard.currentAttack);
        }
        else
        {
            HandleDamage(targetCard);
        }
    }

    private TableSlot GetTargetCard(Direction direction)
    {
        switch (direction)
        {
            case Direction.Center:
                return oppositeTableSlot;
            case Direction.Left:
                return leftTableSlot?.oppositeTableSlot;
            case Direction.Right:
                return rightTableSlot?.oppositeTableSlot;
            default:
                return null;
        }
    }

    private void HandleDamage(TableSlot targetCard)
    {
        FXManager.Instance.ShowDamageText(targetCard.transform.position, statsCard.currentAttack);
        if (!targetCard.isSlotEmpty) // Si hay una carta en frente
        {
            targetCard.statsCard.TakeDamage(statsCard.currentAttack);
        }
        else if (!isEnemyTable)
        {
            ContadoresScript.BajarVidaBoss(statsCard.currentAttack);
        }
        else
        {
            ContadoresScript.BajarVida(statsCard.currentAttack);
        }
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.movingCard)
        {
            if (collision.CompareTag("Animal"))
            {
                 GoGreen();
            }
        }
    }

    public void GoRed()
    {
        slotSpriteRenderer.enabled = true;
        slotSpriteRenderer.color = Color.red;
    }

    public void GoGreen()
    {
        slotSpriteRenderer.enabled = true;
        slotSpriteRenderer.color = Color.green;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        slotSpriteRenderer.enabled = false;
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
