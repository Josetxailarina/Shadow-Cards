using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Cards prefabs
    [SerializeField] private GameObject rat, dog, cat, horse, elephant, effigy, dragon, whale, fire, wind, water;

    [SerializeField] private TableSlot[] enemyTableSlots;
    [SerializeField] private ScriptTurn turnScript;
    [SerializeField] private float transitionDuration = 0.7f;
   
    public void ExecuteEnemyTurn()
    {
        GameManager.gameState = GameState.AutoMove;

        switch (turnScript.currentTurn)
        {
            case 1:
                StartCoroutine(Turn1());
                break;
            case 2:
                StartCoroutine(Turn2());
                break;
            case 3:
                StartCoroutine(Turn3());
                break;
            case 4:
                StartCoroutine(Turn4());
                break;
            case 5:
                StartCoroutine(Turn5());
                break;
            case 6:
                StartCoroutine(Turn6());
                break;
            case 7:
                StartCoroutine(Turn7());
                break;
            case 8:
                StartCoroutine(Turn8());
                break;
            case 9:
                StartCoroutine(Turn9());
                break;
            case 10:
                StartCoroutine(Turn10());
                break;
            default:
                StartCoroutine(Turn10());
                break;
        }
    }
    IEnumerator ExecuteEnemyAttacks()
    {
        foreach (var table in enemyTableSlots)
        {
            if (!table.isSlotEmpty)
            {
                EnemyAttack(table);
                yield return new WaitForSecondsRealtime(0.8f);
            }
        }
        turnScript.NewTurn();
        GameManager.gameState = GameState.Play;
    }
    public void EnemyAttack(TableSlot Table)
    {
        if (Table.currentCardInSlot.currentElement == ElementType.Smoke)
        {
            Table.currentCardInSlot.cardBehavior.cardAnimator.SetTrigger("EnemyDoble");
        }
        else
        {
            Table.currentCardInSlot.cardBehavior.cardAnimator.SetTrigger("EnemyAttack");
        }
    }
    IEnumerator Turn1()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(InstantiateAndMoveCoroutine(rat, enemyTableSlots[0]));
        yield return new WaitForSecondsRealtime(0.5f);
        
        StartCoroutine(InstantiateAndMoveCoroutine(rat, enemyTableSlots[3]));
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn2()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        

        StartCoroutine(InstantiateAndMoveCoroutine(cat, enemyTableSlots[2]));
        yield return new WaitForSecondsRealtime(0.5f);
        if (enemyTableSlots[0].currentCardInSlot != null)
        {
            StartCoroutine(InstantiateAndMoveCoroutine(fire, enemyTableSlots[0]));
        }
        else
        {
            StartCoroutine(InstantiateAndMoveCoroutine(fire, enemyTableSlots[3]));
        }
        yield return new WaitForSecondsRealtime(1);

        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn3()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(elephant, enemyTableSlots[1]));
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn4()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(horse, enemyTableSlots[3]));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(wind, enemyTableSlots[3]));
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn5()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(whale, enemyTableSlots[2]));
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn6()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(dog, enemyTableSlots[1]));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(dog, enemyTableSlots[0]));
        yield return new WaitForSecondsRealtime(0.5f);
        if (enemyTableSlots[2].currentCardInSlot != null)
        {
            StartCoroutine(InstantiateAndMoveCoroutine(wind, enemyTableSlots[2]));
        }
        else
        {
            StartCoroutine(InstantiateAndMoveCoroutine(water, enemyTableSlots[0]));
            StartCoroutine(InstantiateAndMoveCoroutine(water, enemyTableSlots[1]));
        }
        yield return new WaitForSecondsRealtime(1);

        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn7()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(effigy, enemyTableSlots[3]));
        yield return new WaitForSecondsRealtime(0.5f);
        if (enemyTableSlots[3].currentCardInSlot != null)
        {
            StartCoroutine(InstantiateAndMoveCoroutine(fire, enemyTableSlots[3]));
        }
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn8()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(dragon, enemyTableSlots[1]));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(elephant, enemyTableSlots[0]));
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn9()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(whale, enemyTableSlots[0]));
        yield return new WaitForSecondsRealtime(0.5f);
        if (enemyTableSlots[1].currentCardInSlot != null)
        { 
        StartCoroutine(InstantiateAndMoveCoroutine(water, enemyTableSlots[1]));
        }
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }
    IEnumerator Turn10()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(whale, enemyTableSlots[2]));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(dragon, enemyTableSlots[0]));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(wind, enemyTableSlots[0]));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(fire, enemyTableSlots[2]));
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ExecuteEnemyAttacks());
    }

   

    private IEnumerator InstantiateAndMoveCoroutine(GameObject Card, TableSlot table)
    {
        GameObject cardInstance = Instantiate(Card, transform.position, Quaternion.identity);
        CardState cardState = cardInstance.GetComponent<CardState>();

        cardInstance.transform.SetParent(table.gameObject.transform, true);
        cardInstance.transform.localEulerAngles = Vector3.zero;

        bool isElementalCard = cardState.cardData.isElementalCard;
        bool shouldReplaceCard = !table.isSlotEmpty && !isElementalCard;
        GameObject cardToReplace = null;

        if (shouldReplaceCard)
        {
            cardToReplace = table.currentCardInSlot.gameObject;
        }

        if (!isElementalCard)
        {
            table.currentCardInSlot = cardState;
            table.currentCardInSlot.cardBehavior.DeactivateCollider();
            table.currentCardInSlot.cardBehavior.currentTableSlot = table;
            table.isSlotEmpty = false;
        }
        else { cardInstance.GetComponent<CardState>().cardBehavior.DeactivateCollider(); }

        // Inicialize the position and scale of the instance
        cardInstance.transform.localScale = Vector3.zero;
        Transform firstChild = cardInstance.transform.GetChild(0);
        firstChild.transform.localScale = Vector3.one;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = table.gameObject.transform.position;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        float elapsedTime = 0f;

        // Transition of the card
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            cardInstance.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            cardInstance.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        cardInstance.transform.position = endPosition;
        cardInstance.transform.localScale = endScale;

        if (isElementalCard)
        {
            table.currentCardInSlot?.AddElement(cardState);
            Destroy(cardInstance);
        }
        else if (shouldReplaceCard)
        {
            Destroy(cardToReplace);
            SoundManager.instance.useCardSound.Play();
        }
        else
        {
            SoundManager.instance.useCardSound.Play();
        }
    }

}
