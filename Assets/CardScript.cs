using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public Animator anim;
    private int actualLayer;
    public SpriteRenderer render;
    private bool dragging = false;
    private BoxCollider2D cardCollider;
    private Vector2 colliderOffset;
    private Vector2 colliderSize;
    private Camera mainCamera;
    private GameObject parentGameobject;
    private ManoScript scriptMano;
    private Vector3 targetPosition;
    private TableCards tableScript;
    public Element element = Element.none;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        cardCollider =  GetComponent<BoxCollider2D>();
        if (cardCollider != null)
        {
            colliderOffset = cardCollider.offset;
            colliderSize = cardCollider.size;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        parentGameobject = transform.parent.gameObject;
        scriptMano = FindObjectOfType<ManoScript>();
    }

    private void Update()
    {
        if (dragging)
        {
            // Obtiene la posición del ratón en la pantalla.
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Convierte la posición del ratón a coordenadas del mundo.
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.WorldToScreenPoint(parentGameobject.transform.position).z));

            // Establece la posición del GameObject en la posición del ratón en el mundo.
            parentGameobject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, parentGameobject.transform.position.z);

            Debug.Log("Mouse World Position: " + mouseWorldPosition);
            Debug.Log("Object Position: " + transform.position);
        }
    }

    private void OnMouseEnter()
    {
        CardUp();
    }
    public void Attack()
    {
        tableScript.Attack();
    }
    public void ResetSortingOrder()
    {
        render.sortingOrder = 0;
    }
    private void OnMouseExit()
    {
        CardDown();
    }

    private void OnMouseDown()
    {
        if (!GameManager.autoMove)
        {
            render.sortingOrder = 20;
            dragging = true;
            GameManager.movingCard = true;
            cardCollider.size = new Vector2(0.2f, 0.2f);
            cardCollider.offset = new Vector2(0, 0);
            anim.SetBool("ShowCard", false);
            transform.parent.eulerAngles = new Vector3(33.55f, 0, 0);
            transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
        
        
    }

    private void OnMouseUp()
    {

        if (element == Element.none && tableScript != null && tableScript.available)
        {
            tableScript.available = false;
            tableScript.ActivateButton();
            tableScript.statsCard = transform.parent.GetComponent<CardStats>();
            tableScript.statsCard.scriptCard = this;
            transform.parent.parent = tableScript.gameObject.transform;
            StartCoroutine(MoveFromTo(parentGameobject.transform.position, targetPosition, 0.15f));
            cardCollider.enabled = false;
        }
        else if (element != Element.none && tableScript != null && !tableScript.available && tableScript.statsCard.element2 == Element.none && tableScript.statsCard.element1 != element)
        {
            tableScript.statsCard.AddElement(element);
            Destroy(transform.parent.gameObject);
        }
       else 
        {

            cardCollider.size = colliderSize;
            cardCollider.offset = colliderOffset;
            transform.parent.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
        }
       
        render.sortingOrder = 1;

        dragging = false;
        GameManager.movingCard = false;
        scriptMano.ActualizarMano();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetPosition = collision.transform.position;
        tableScript = collision.GetComponent<TableCards>();
        if (element != Element.none && tableScript != null && !tableScript.available && tableScript.statsCard.element2 == Element.none && tableScript.statsCard.element1 != element)
        {
            tableScript.GoGreen();
        }
        else
        {
            tableScript.GoRed();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cardCollider.enabled)
        {
            tableScript = null;

        }
    }

    public void CardUp()
    {
        if (!GameManager.movingCard)
        {
            anim.SetBool("ShowCard", true);
            actualLayer = render.sortingOrder;
            render.sortingOrder = 20;
        }
        
    }

    public void CardDown()
    {
        if (!GameManager.movingCard)
        {
            anim.SetBool("ShowCard", false);
            render.sortingOrder = actualLayer;
        }
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
