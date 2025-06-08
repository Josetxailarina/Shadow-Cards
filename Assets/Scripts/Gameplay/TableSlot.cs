using UnityEngine;

public class TableSlot : MonoBehaviour
{
    [HideInInspector] public CardState currentCardInSlot;
    [HideInInspector] public bool isSlotEmpty = true;
    [HideInInspector] public bool hasProtection = false;

    public bool isEnemySlot;
    public AttackButton attackButton;
    public TableSlot oppositeTableSlot;
    
    [SerializeField] private IceWall iceWallScript;
    [SerializeField] private TableSlot leftTableSlot;
    [SerializeField] private TableSlot rightTableSlot;

    private SpriteRenderer slotSpriteRenderer;


    private void Start()
    {
        slotSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleSlotState(collision);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        slotSpriteRenderer.enabled = false;
    }



    public void PerformAttack(Direction direction)
    {
        TableSlot targetCard = GetTargetCard(direction);
        if (targetCard == null) return;

        SoundManager.instance.attackSound.Play();

        if (targetCard.hasProtection)
        {
            targetCard.iceWallScript.TakeDamage(currentCardInSlot.currentAttack);
        }
        else
        {
            HandleDamage(targetCard);
        }
    }


    private void HandleSlotState(Collider2D collision)
    {
        if (GameManager.isDraggingCard)
        {
            if (collision.CompareTag("AnimalCard"))
            {
                GoGreen();
            }
            else if (collision.CompareTag("ElementalCard"))
            {
                CardBehavior collisionCard = collision.GetComponent<CardBehavior>();
                if (!isSlotEmpty && currentCardInSlot.CanAddElement(collisionCard.cardState.cardData.element))
                {
                    GoGreen();
                }
                else
                {
                    GoRed();
                }
            }
        }
    }

   
    
    public void AddIceWall()
    {
        if (hasProtection)
        {
            iceWallScript.AddHealthToWall();
        }
        else
        {
            iceWallScript.gameObject.SetActive(true);
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

    private void HandleDamage(TableSlot targetTableSlot)
    {
        FXManager.Instance.ShowDamageText(targetTableSlot.transform.position, currentCardInSlot.currentAttack);
        
        if (!targetTableSlot.isSlotEmpty) // if there is a card in the target slot
        {
            targetTableSlot.currentCardInSlot.TakeDamage(currentCardInSlot.currentAttack);
        }
        else if (!isEnemySlot)
        {
            CountersManager.instance.ReduceBossHealth(currentCardInSlot.currentAttack);
        }
        else
        {
            CountersManager.instance.ReducePlayerHealth(currentCardInSlot.currentAttack);
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

    

    public void EnableAttackButton()
    {
        attackButton.gameObject.SetActive(true);
    }

    public void DisableAttackButton()
    {
        attackButton.gameObject.SetActive(false);
    }

    public void ClearTableSlot()
    {
        currentCardInSlot.cardBehavior = null;
        Destroy(currentCardInSlot.gameObject);
        currentCardInSlot = null;
    }
    public void SetCardInSlot(CardState card)
    {
        isSlotEmpty = false;
        EnableAttackButton();
        currentCardInSlot = card;
    }
}



public enum Direction
{
    Center,
    Left,
    Right
}
