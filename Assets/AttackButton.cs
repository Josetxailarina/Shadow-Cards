using UnityEngine;

public class AttackButton : MonoBehaviour
{

    public TableSlot tableScript;

    private void OnMouseDown()
    {
        if (tableScript.statsCard.currentElement == ElementType.Smoke)
        {
            if (tableScript.isEnemyTable)
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
