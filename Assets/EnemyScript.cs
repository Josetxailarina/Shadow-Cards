using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyScript : MonoBehaviour
{
    public GameObject rat;
    public GameObject dog;
    public GameObject cat;
    public GameObject horse;
    public GameObject elephant;
    public GameObject effigy;
    public GameObject dragon;
    public GameObject whale;

    public TableCards table1;
    public TableCards table2;
    public TableCards table3;
    public TableCards table4;

    public ScriptTurn turnScript;

    public float duration;
    private void Start()
    {
        
    }
    public void Turn1()
    {
        GameManager.autoMove = true;
        switch (ScriptTurn.turn)
        {
            case 1:
                StartCoroutine(SacarTurno1());
                break;
            case 2:
                StartCoroutine(SacarTurno2());
                break;
            case 3:
                StartCoroutine(SacarTurno3());
                break;
            case 4:
                StartCoroutine(SacarTurno4());
                break;
            case 5:
                StartCoroutine(SacarTurno5());
                break;
            case 6:
                StartCoroutine(SacarTurno6());
                break;
            case 7:
                StartCoroutine(SacarTurno7());
                break;
            case 8:
                StartCoroutine(SacarTurno8());
                break;
            case 9:
                StartCoroutine(SacarTurno9());
                break;
            case 10:
                StartCoroutine(SacarTurno10());
                break;
            default:
                StartCoroutine(SacarTurno10());
                break;
        }

    }
    IEnumerator Attack()
    {
        if (!table1.available)
        {
            EnemyAttack(table1);
            yield return new WaitForSecondsRealtime(0.8f);
        }
        
        if (!table2.available)
        {
            EnemyAttack(table2);
            yield return new WaitForSecondsRealtime(0.8f);
            
        }
        if (!table3.available)
        {
            EnemyAttack(table3);
            yield return new WaitForSecondsRealtime(0.8f);
        }
        if (!table4.available)
        {
            EnemyAttack(table4);
            yield return new WaitForSecondsRealtime(0.8f);
        }
        turnScript.NewTurn();
        GameManager.autoMove = false;
    }
    public void EnemyAttack(TableCards Table)
    {
        if ((Table.statsCard.element1 == Element.fire && Table.statsCard.element2 == Element.water) || (Table.statsCard.element1 == Element.water && Table.statsCard.element2 == Element.fire))
        {
            Table.statsCard.scriptCard.anim.SetTrigger("EnemyDoble");
            print("doble attak");
        }
        else
        {
            Table.statsCard.scriptCard.anim.SetTrigger("EnemyAttack");
            print("attak");

        }
    }
    IEnumerator SacarTurno1()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(InstantiateAndMoveCoroutine(rat, table1));
            yield return new WaitForSecondsRealtime(0.5f);
        
        StartCoroutine(InstantiateAndMoveCoroutine(rat, table4));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno2()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        

        StartCoroutine(InstantiateAndMoveCoroutine(cat, table3));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno3()
    {
        yield return new WaitForSecondsRealtime(0.5f);



        StartCoroutine(InstantiateAndMoveCoroutine(elephant, table2));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno4()
    {
        yield return new WaitForSecondsRealtime(0.5f);



        StartCoroutine(InstantiateAndMoveCoroutine(horse, table4));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(cat, table1));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno5()
    {
        yield return new WaitForSecondsRealtime(0.5f);



        
        StartCoroutine(InstantiateAndMoveCoroutine(whale, table3));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno6()
    {
        yield return new WaitForSecondsRealtime(0.5f);




        StartCoroutine(InstantiateAndMoveCoroutine(dog, table2));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(horse, table1));
        yield return new WaitForSecondsRealtime(0.5f);
        table3.statsCard?.AddElement(Element.wind);
        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno7()
    {
        yield return new WaitForSecondsRealtime(0.5f);




        StartCoroutine(InstantiateAndMoveCoroutine(effigy, table4));
        
        yield return new WaitForSecondsRealtime(0.5f);
        table4.statsCard?.AddElement(Element.fire);
        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno8()
    {
        yield return new WaitForSecondsRealtime(0.5f);




        StartCoroutine(InstantiateAndMoveCoroutine(dragon, table2));
        yield return new WaitForSecondsRealtime(0.5f);
        
        
        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno9()
    {
        yield return new WaitForSecondsRealtime(0.5f);




        StartCoroutine(InstantiateAndMoveCoroutine(whale, table1));
        yield return new WaitForSecondsRealtime(0.5f);
        table2.statsCard?.AddElement(Element.water);
        yield return new WaitForSecondsRealtime(0.5f);



        StartCoroutine(Attack());
    }
    IEnumerator SacarTurno10()
    {
        yield return new WaitForSecondsRealtime(0.5f);




        StartCoroutine(InstantiateAndMoveCoroutine(whale, table3));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(InstantiateAndMoveCoroutine(dragon, table1));
        yield return new WaitForSecondsRealtime(0.5f);



        StartCoroutine(Attack());
    }

    private IEnumerator InstantiateAndMoveCoroutine(GameObject Card, TableCards table)
    {
        // Instancia el prefab en la posición del objeto que tiene el script con rotación identitaria
        GameObject instance = Instantiate(Card, transform.position, Quaternion.identity);
        bool reemplazar = !table.available;
        GameObject cartaReemplazable = null;
        if (reemplazar)
        {
            cartaReemplazable = table.statsCard.gameObject;
        }
        // Configura el padre del prefab instanciado
        instance.transform.SetParent(table.gameObject.transform, true);
        instance.transform.localEulerAngles = Vector3.zero;
        table.statsCard = instance.GetComponent<CardStats>();
        table.statsCard.scriptCard.DeactivateCollider();
        table.statsCard.scriptCard.tableScript = table;
        table.available = false;
        // Inicializa la escala a 0
        instance.transform.localScale = Vector3.zero;
        Transform firstChild = instance.transform.GetChild(0);
        firstChild.transform.localScale = Vector3.one;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = table.gameObject.transform.position;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        float elapsedTime = 0f;

        // Transición de posición y escala
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Lerp para interpolar entre las posiciones y las escalas
            instance.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            instance.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        // Asegurarse de que la posición y escala finales sean exactas
        instance.transform.position = endPosition;
        instance.transform.localScale = endScale;
        if (reemplazar == true)
        {
          Destroy(cartaReemplazable);
        }
    }
}
