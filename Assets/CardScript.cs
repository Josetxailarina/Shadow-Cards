using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private Animator anim;
    private int actualPosition;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        actualPosition = transform.GetSiblingIndex();
    }
    public void CardUp()
    {
        anim.SetBool("CardUp",true);
        transform.SetSiblingIndex(transform.parent.childCount - 1);


    }
    public void CardDown()
    {
        anim.SetBool("CardUp", false);
        transform.SetSiblingIndex(actualPosition);
    }
}
