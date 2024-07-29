using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{

    public TableCards tableScript;

    private void OnMouseDown()
    {
        tableScript.statsCard.scriptCard.render.sortingOrder = 20;
        tableScript.statsCard.scriptCard.anim.SetTrigger("Attack");
        gameObject.SetActive(false);
    }
}
