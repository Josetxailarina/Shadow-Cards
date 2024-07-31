using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{

    public TableCards tableScript;

    private void OnMouseDown()
    {
        if ((tableScript.statsCard.element1 == Element.fire && tableScript.statsCard.element2 == Element.water) || (tableScript.statsCard.element1 == Element.water && tableScript.statsCard.element2 == Element.fire))
        {
            if (tableScript.enemyBoard)
            {
                tableScript.statsCard.scriptCard.SortingOrderUp(20);
                tableScript.statsCard.scriptCard.anim.SetTrigger("EnemyDoble");
                gameObject.SetActive(false);
            }
            else
            {


                tableScript.statsCard.scriptCard.SortingOrderUp(20);
                tableScript.statsCard.scriptCard.anim.SetTrigger("DobleAttack");
                gameObject.SetActive(false);
            }
        }
        else
        {
            tableScript.statsCard.scriptCard.SortingOrderUp(20);
            tableScript.statsCard.scriptCard.anim.SetTrigger("Attack");
            gameObject.SetActive(false);
        }
    }
}
