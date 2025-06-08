using System.Collections;
using UnityEngine;

public class CardAttack : MonoBehaviour
{
    private CardBehavior cardBehavior;

    private void Start()
    {
        cardBehavior = GetComponent<CardBehavior>();
    }
    public void Attack() // Called from animator when attack animation ends
    {
        if (cardBehavior.cardState.currentElement == ElementType.Tornado)
        {
            StartCoroutine(TripleAttack());
            FXManager.Instance.PlayEffect(ElementType.TornadoAttack, cardBehavior.currentTableSlot.oppositeTableSlot.transform.position);
        }
        else
        {
            cardBehavior.currentTableSlot.PerformAttack(Direction.Center);
        }
    }
    IEnumerator TripleAttack()
    {
        cardBehavior.currentTableSlot.PerformAttack(Direction.Left);

        yield return new WaitForSecondsRealtime(0.39f);

        cardBehavior.currentTableSlot.PerformAttack(Direction.Center);

        yield return new WaitForSecondsRealtime(0.39f);

        cardBehavior.currentTableSlot.PerformAttack(Direction.Right);
    }
}
