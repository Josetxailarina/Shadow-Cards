using System.Collections;
using UnityEngine;

public class CardAttack : MonoBehaviour
{
    private CardBehavior cardScript;

    private void Start()
    {
        cardScript = GetComponent<CardBehavior>();
    }
    public void Attack() // Called from animator
    {
        if (cardScript.cardState.currentElement == ElementType.Tornado)
        {
            StartCoroutine(TripleAttack());
            FXManager.Instance.PlayEffect(ElementType.TornadoAttack, cardScript.currentTableSlot.oppositeTableSlot.transform.position);
        }
        else
        {
            cardScript.currentTableSlot.PerformAttack(Direction.Center);
        }
    }
    IEnumerator TripleAttack()
    {
        cardScript.currentTableSlot.PerformAttack(Direction.Left);

        yield return new WaitForSecondsRealtime(0.39f);

        cardScript.currentTableSlot.PerformAttack(Direction.Center);

        yield return new WaitForSecondsRealtime(0.39f);

        cardScript.currentTableSlot.PerformAttack(Direction.Right);
    }
}
