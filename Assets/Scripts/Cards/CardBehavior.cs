using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CardBehavior : MonoBehaviour
{
    // References to other scripts
    [HideInInspector] public TableSlot currentTableSlot;
    [HideInInspector] public CardState cardState;
    private CardData cardData;
    private PlayerHandLayout playerHandLayout;

    // State variables for card interaction
    private bool isDragging = false;
    private bool isTouchingTableSlot = false;

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
        cardData = cardState.cardData;

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
        HighlightCard();
    }


    private void OnMouseExit()
    {
        UnhighlightCard();
    }

    private void OnMouseDown()
    {
        if (GameManager.gameState == GameState.Play)
        {
            if (CountersManager.instance.CanPayManaCost(cardData.cost))
            {
                StartCardDrag();
            }
            else
            {
                CountersManager.instance.ShowManaInsufficientEffect();
            }
        }
    }


    private void OnMouseUp()
    {
        if (!GameManager.isDraggingCard) return;

        if (CanPlaceCardInSlot())
        {
            PlaceCardInSlot();
        }
        else if (CanAddElementToSlot())
        {
            AddElementToSlot();
        }
        else
        {
            ReturnCardToHand();
        }

        ResetSortingOrder();
        isDragging = false;
        GameManager.isDraggingCard = false;
        playerHandLayout.UpdateHand();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetPosition = collision.transform.position;
        currentTableSlot = collision.GetComponent<TableSlot>();
        isTouchingTableSlot = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cardCollider.enabled)
        {
            currentTableSlot = null;
            isTouchingTableSlot = false;
        }
    }


    private bool CanAddElementToSlot()
    {
        return cardData.isElementalCard && isTouchingTableSlot && !currentTableSlot.isSlotEmpty && currentTableSlot.currentCardInSlot.CanAddElement(cardState.cardData.element);
    }

    private bool CanPlaceCardInSlot()
    {
        return !cardData.isElementalCard && isTouchingTableSlot;
    }

    private void PlaceCardInSlot()
    {
        if (!currentTableSlot.isSlotEmpty)
        {
            currentTableSlot.ClearTableSlot();
        }
        currentTableSlot.SetCardInSlot(cardState);
        transform.parent.parent = currentTableSlot.gameObject.transform;
        cardCollider.enabled = false;
        CountersManager.instance.ReducePlayerMana(cardData.cost);
        SoundManager.instance.useCardSound.Play();
        StartCoroutine(AnimateCardMovement(parentGameobject.transform.position, targetPosition, 0.15f));

    }

    private void AddElementToSlot()
    {
        currentTableSlot.currentCardInSlot.AddElement(cardState);
        CountersManager.instance.ReducePlayerMana(cardData.cost);
        Destroy(transform.parent.gameObject);
    }

    private void ReturnCardToHand()
    {
        cardCollider.size = defaultColliderSize;
        cardCollider.offset = defaultColliderOffset;
        transform.parent.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }

   

    public void ResetSortingOrder()
    {
        sortingGroup.sortingOrder = 5;
    }
    public void SetSortingOrder(int Order)
    {
        sortingGroup.sortingOrder = Order;
    }
    public void HighlightCard()
    {
        if (!GameManager.isDraggingCard && GameManager.gameState == GameState.Play)
        {
            cardAnimator.SetBool("ShowCard", true);
            currentSortingOrderInHand = sortingGroup.sortingOrder;
            SetSortingOrder(20);
            SoundManager.instance.selectCardSound.Play();
        }
    }

    public void UnhighlightCard()
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

    private void StartCardDrag()
    {
        isDragging = true;
        GameManager.isDraggingCard = true;
        InitializeCardDragState();
        SoundManager.instance.takeCardSound.Play();
    }

    private void InitializeCardDragState()
    {
        SetSortingOrder(20);
        cardCollider.size = new Vector2(0.2f, 0.2f);
        cardCollider.offset = new Vector2(0, 0);
        cardAnimator.SetBool("ShowCard", false);
        transform.parent.eulerAngles = new Vector3(33.55f, 0, 0);
        transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
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
