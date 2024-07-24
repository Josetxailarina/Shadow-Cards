using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardsScript : MonoBehaviour
{
    private int childCount;
    private GridLayoutGroup cardsGroup;

    private void Awake()
    {
        cardsGroup = GetComponent<GridLayoutGroup>();
    }
    private void Start()
    {
        childCount = transform.childCount;
        AjustarSeparacion();
    }
    public void AjustarSeparacion()
    {
        if (childCount > 7)
        {
            cardsGroup.spacing = new Vector2(-125,0);
        }
        else
        {
            cardsGroup.spacing = new Vector2(-60, 0);

        }
    }
}
