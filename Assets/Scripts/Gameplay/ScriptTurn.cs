using UnityEngine;

public class ScriptTurn : MonoBehaviour
{
    [HideInInspector] public int currentTurn;

    [SerializeField] private EnemyScript enemyScript;
    [SerializeField] private TableSlot[] playerTableSlots;
    [SerializeField] private PlayerDeck playerDeck;
    [SerializeField] private GameObject panelAttackAvailable;
    [SerializeField] private GameObject panelManaAvailable;
    [SerializeField] private GameObject playerHand;

    private SpriteRenderer sprite;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        currentTurn = 1;
    }

    private void OnMouseDown()
    {
        if (GameManager.gameState == GameState.Play)
        {
            if (AnyAttackAvailable())
            {
                OpenWarningPanel(panelAttackAvailable);
            }
            else if (HasUsableMana())
            {
                OpenWarningPanel(panelManaAvailable);
            }
            else
            {
                EndTurn();
            }
            animator.SetBool("On", false);

        }
    }

    private void OnMouseOver()
    {
        if (GameManager.gameState == GameState.Play)
        {
            animator.SetBool("On", true);
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.gameState == GameState.Play)
        {
            animator.SetBool("On", false);
        }
    }

    public void OpenWarningPanel(GameObject panel)
    {
        GameManager.gameState = GameState.AutoMove;
        panel.SetActive(true);
    }

    public void CloseSecurityPanel(bool useManaPanel)
    {
        GameManager.gameState = GameState.Play;

        if (useManaPanel)
        {
            panelManaAvailable.SetActive(false);

        }
        else
        {
            panelAttackAvailable.SetActive(false);
        }
    }

    public void NewTurn()
    {
        currentTurn++;
        CountersManager.instance.SetCurrentMana(currentTurn);
        sprite.color = new Color(1f, 1f, 1f, 1);
        StartCoroutine(playerDeck.DrawSomeCards(2));
        foreach (TableSlot script in playerTableSlots)
        {
            if (!script.isSlotEmpty)
            {
                script.EnableAttackButton();
            }
        }
    }
    

    private bool AnyAttackAvailable()
    {
        foreach (var script in playerTableSlots)
        {
            if (script.attackButton.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

   
    public bool HasUsableMana()
    {
        CardState[] cards = playerHand.GetComponentsInChildren<CardState>();
        foreach (CardState card in cards)
        {
            if (CountersManager.instance.CanPayManaCost(card.cardData.cost))
            {
                return true;
            }
        }
        return false;
    }
    public void EndTurn()
    {
        sprite.color = Color.gray;
        foreach (TableSlot script in playerTableSlots)
        {
            script.DisableAttackButton();
        }
        GameManager.gameState = GameState.AutoMove;
        SoundManager.instance.passSound.Play();
        enemyScript.ExecuteEnemyTurn();
    }
}
