using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CardBehavior : MonoBehaviour
{
    // References to other scripts
    [HideInInspector] public TableSlot currentTableSlot;
    [HideInInspector] public CardState cardState;
    private PlayerHandLayout playerHandLayout;


    // State variables for card interaction
    private bool isDragging = false;


    // Internal Unity component references
    [HideInInspector] public Animator cardAnimator;
    private SortingGroup sortingGroup;
    private Camera mainCamera;
    private BoxCollider2D cardCollider;


    // Card movement and positioning
    private GameObject parentGameobject;
    private Vector3 targetPosition;
    private int currentSortingOrderInHand;


    // Default collider values for reset
    private Vector2 defaultColliderOffset;
    private Vector2 defaultColliderSize;


    
    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
        cardAnimator = GetComponent<Animator>();
        cardCollider = GetComponent<BoxCollider2D>();
        cardState = GetComponentInParent<CardState>();

        if (cardCollider != null)
        {
            defaultColliderOffset = cardCollider.offset;
            defaultColliderSize = cardCollider.size;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        parentGameobject = transform.parent.gameObject;
        playerHandLayout = FindObjectOfType<PlayerHandLayout>();
    }

    private void Update()
    {
        HandleDragging();
    }


    private void OnMouseEnter()
    {
        CardUp();
    }
  
   
    private void OnMouseExit()
    {
        CardDown();
    }

    private void OnMouseDown()
    {
        if (GameManager.gameState == GameState.Play)
        {
            if (CountersManager.instance.CanPayManaCost(cardState.cardData.cost))
            {

                SetSortingOrder(20);
                isDragging = true;
                GameManager.isDraggingCard = true;
                cardCollider.size = new Vector2(0.2f, 0.2f);
                cardCollider.offset = new Vector2(0, 0);
                cardAnimator.SetBool("ShowCard", false);
                transform.parent.eulerAngles = new Vector3(33.55f, 0, 0);
                transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                SoundManager.instance.takeCardSound.Play();
            }
            else
            {
                CountersManager.instance.ShowManaInsufficient();
            }
        }


    }

    private void OnMouseUp()
    {
        if (GameManager.isDraggingCard)
        {
            if (!cardState.cardData.isElementalCard && currentTableSlot != null)
            {
                if (!currentTableSlot.isSlotEmpty)
                {
                    currentTableSlot.currentCardInSlot.scriptCard = null;

                    Destroy(currentTableSlot.currentCardInSlot.gameObject);

                    currentTableSlot.currentCardInSlot = null;
                }
                currentTableSlot.isSlotEmpty = false;
                currentTableSlot.EnableAttackButton();
                currentTableSlot.currentCardInSlot = transform.parent.GetComponent<CardState>();
                currentTableSlot.currentCardInSlot.scriptCard = this;
                transform.parent.parent = currentTableSlot.gameObject.transform;
                StartCoroutine(AnimateCardMovement(parentGameobject.transform.position, targetPosition, 0.15f));
                cardCollider.enabled = false;
                CountersManager.instance.ReducePlayerMana(cardState.cardData.cost);
                SoundManager.instance.useCardSound.Play();


            }
            else if (cardState.cardData.isElementalCard && currentTableSlot != null && !currentTableSlot.isSlotEmpty && currentTableSlot.currentCardInSlot.CanAddElement(cardState.cardData.element))
            {
                currentTableSlot.currentCardInSlot.AddElement(cardState);
                CountersManager.instance.ReducePlayerMana(cardState.cardData.cost);
                Destroy(transform.parent.gameObject);
            }
            else
            {
                cardCollider.size = defaultColliderSize;
                cardCollider.offset = defaultColliderOffset;
                transform.parent.eulerAngles = new Vector3(0, 0, 0);
                transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            }
            ResetSortingOrder();
            isDragging = false;
            GameManager.isDraggingCard = false;
            playerHandLayout.UpdateHand();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetPosition = collision.transform.position;
        currentTableSlot = collision.GetComponent<TableSlot>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cardCollider.enabled)
        {
            currentTableSlot = null;
        }
    }

    public void ResetSortingOrder()
    {
        sortingGroup.sortingOrder = 5;
    }
    public void SetSortingOrder(int Order)
    {
        sortingGroup.sortingOrder = Order;
    }
    public void CardUp()
    {
        if (!GameManager.isDraggingCard && GameManager.gameState == GameState.Play)
        {
            cardAnimator.SetBool("ShowCard", true);
            currentSortingOrderInHand = sortingGroup.sortingOrder;
            SetSortingOrder(20);
            SoundManager.instance.selectCardSound.Play();
        }
    }

    public void CardDown()
    {
        if (!GameManager.isDraggingCard && GameManager.gameState == GameState.Play)
        {
            cardAnimator.SetBool("ShowCard", false);
            SetSortingOrder(currentSortingOrderInHand);
        }
    }

    private void HandleDragging()
    {
        if (isDragging)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.WorldToScreenPoint(parentGameobject.transform.position).z));

            parentGameobject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, parentGameobject.transform.position.z);
        }
    }

    public void DeactivateCollider()
    {
        cardCollider.enabled = false;
    }
    IEnumerator AnimateCardMovement(Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            parentGameobject.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        parentGameobject.transform.position = end;
    }
}
