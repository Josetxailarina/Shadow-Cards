using UnityEngine;

public class AttackButton : MonoBehaviour
{

    public TableSlot tableScript;

    private void OnMouseDown()
    {
        if (tableScript.currentCardInSlot.currentElement == ElementType.Smoke)
        {
            if (tableScript.isEnemySlot)
            {
                tableScript.currentCardInSlot.scriptCard.SortingOrderUp(20);
                tableScript.currentCardInSlot.scriptCard.cardAnimator.SetTrigger("EnemyDoble");
                gameObject.SetActive(false);
            }
            else
            {


                tableScript.currentCardInSlot.scriptCard.SortingOrderUp(20);
                tableScript.currentCardInSlot.scriptCard.cardAnimator.SetTrigger("DobleAttack");
                gameObject.SetActive(false);
            }
        }
        else
        {
            tableScript.currentCardInSlot.scriptCard.SortingOrderUp(20);
            tableScript.currentCardInSlot.scriptCard.cardAnimator.SetTrigger("Attack");
            gameObject.SetActive(false);
        }
    }
}
