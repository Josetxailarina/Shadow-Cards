using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CardBehavior : MonoBehaviour
{
    [HideInInspector] public TableSlot currentTableSlot;
    [HideInInspector] public CardState cardState;
    [HideInInspector] public Animator cardAnimator;

    private bool isDragging = false;
    private int currentSortingOrderInHand;

    private SortingGroup sortingGroup;
    private Camera mainCamera;
    private GameObject parentGameobject;
    private PlayerHandLayout playerHandLayout;
    private Vector3 targetPosition;
    private BoxCollider2D cardCollider;
    private Vector2 defaultColliderOffset;
    private Vector2 defaultColliderSize;



    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
        cardAnimator = GetComponent<Animator>();
        cardCollider = GetComponent<BoxCollider2D>();
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
        cardState = GetComponentInParent<CardState>();
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
            if (ContadoresScript.mana >= cardState.cardData.cost)
            {

                SortingOrderUp(20);
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
                ContadoresScript.ManaAnim();
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
                StartCoroutine(MoveFromTo(parentGameobject.transform.position, targetPosition, 0.15f));
                cardCollider.enabled = false;
                ContadoresScript.BajarMana(cardState.cardData.cost);
                SoundManager.instance.useCardSound.Play();


            }
            else if (cardState.cardData.isElementalCard && currentTableSlot != null && !currentTableSlot.isSlotEmpty && currentTableSlot.currentCardInSlot.CanAddElement(cardState.cardData.element))
            {
                currentTableSlot.currentCardInSlot.AddElement(cardState);
                ContadoresScript.BajarMana(cardState.cardData.cost);
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
    public void SortingOrderUp(int Order)
    {
        sortingGroup.sortingOrder = Order;
    }
    public void CardUp()
    {
        if (!GameManager.isDraggingCard && GameManager.gameState == GameState.Play)
        {
            cardAnimator.SetBool("ShowCard", true);
            currentSortingOrderInHand = sortingGroup.sortingOrder;
            SortingOrderUp(20);
            SoundManager.instance.selectCardSound.Play();
        }
    }

    public void CardDown()
    {
        if (!GameManager.isDraggingCard && GameManager.gameState == GameState.Play)
        {
            cardAnimator.SetBool("ShowCard", false);
            SortingOrderUp(currentSortingOrderInHand);
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
    IEnumerator MoveFromTo(Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            // Actualiza la posición del objeto
            parentGameobject.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Espera hasta el siguiente frame
            yield return null;
        }

        // Asegura que el objeto termine exactamente en la posición final
        parentGameobject.transform.position = end;
    }
}
