using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private Animator anim;
    private int actualLayer;
    private SpriteRenderer render;
    private bool dragging = false;
    private BoxCollider2D cardCollider;
    private Vector2 colliderOffset;
    private Vector2 colliderSize;
    private Camera mainCamera;
    private GameObject parentGameobject;
    private bool usingCard;
    private ManoScript scriptMano;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        cardCollider = GetComponent<BoxCollider2D>();
        colliderOffset = cardCollider.offset;
        colliderSize = cardCollider.size;
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

    private void OnMouseExit()
    {
        CardDown();
    }

    private void OnMouseDown()
    {
        dragging = true;
        cardCollider.size = new Vector2(0.2f, 0.2f);
        cardCollider.offset = new Vector2(0, 0);
        anim.SetBool("ShowCard", false);
        usingCard = true;
        transform.eulerAngles = new Vector3(33.55f, 0, 0);
        transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }

    private void OnMouseUp()
    {
        dragging = false;
        cardCollider.size = colliderSize;
        cardCollider.offset = colliderOffset;
        scriptMano.ActualizarMano();
        usingCard = false;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }

    public void CardUp()
    {
        if (!usingCard)
        {
            anim.SetBool("ShowCard", true);
            actualLayer = render.sortingOrder;
            render.sortingOrder = 20;
        }
        
    }

    public void CardDown()
    {
        if (!usingCard)
        {
            anim.SetBool("ShowCard", false);
            render.sortingOrder = actualLayer;
        }
    }
}
