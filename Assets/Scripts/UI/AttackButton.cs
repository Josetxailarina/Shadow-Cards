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
                tableScript.currentCardInSlot.cardBehavior.SetSortingOrder(20);
                tableScript.currentCardInSlot.cardBehavior.cardAnimator.SetTrigger("EnemyDoble");
                gameObject.SetActive(false);
            }
            else
            {
                tableScript.currentCardInSlot.cardBehavior.SetSortingOrder(20);
                tableScript.currentCardInSlot.cardBehavior.cardAnimator.SetTrigger("DobleAttack");
                gameObject.SetActive(false);
            }
        }
        else
        {
            tableScript.currentCardInSlot.cardBehavior.SetSortingOrder(20);
            tableScript.currentCardInSlot.cardBehavior.cardAnimator.SetTrigger("Attack");
            gameObject.SetActive(false);
        }
    }
}
