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
        StartCoroutine(SacarCartas());
    }
    IEnumerator Attack()
    {
        if (!table1.available)
        {
            table1.statsCard.scriptCard.anim.SetTrigger("EnemyAttack");
            yield return new WaitForSecondsRealtime(0.8f);
        }
        
        if (!table2.available)
        {
            table2.statsCard.scriptCard.anim.SetTrigger("EnemyAttack");
            yield return new WaitForSecondsRealtime(0.8f);
        }
        if (!table3.available)
        {
            table3.statsCard.scriptCard.anim.SetTrigger("EnemyAttack");
            yield return new WaitForSecondsRealtime(0.8f);
        }
        if (!table4.available)
        {
            table4.statsCard.scriptCard.anim.SetTrigger("EnemyAttack");
            yield return new WaitForSecondsRealtime(0.8f);
        }
        turnScript.GoLight();
        GameManager.autoMove = false;
    }
    IEnumerator SacarCartas()
    {
        yield return new WaitForSecondsRealtime(0.8f);

        StartCoroutine(InstantiateAndMoveCoroutine(rat, table1));
            yield return new WaitForSecondsRealtime(0.5f);
        
        StartCoroutine(InstantiateAndMoveCoroutine(rat, table4));
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(Attack());
    }
    private IEnumerator InstantiateAndMoveCoroutine(GameObject Card, TableCards table)
    {
        // Instancia el prefab en la posición del objeto que tiene el script con rotación identitaria
        GameObject instance = Instantiate(Card, transform.position, Quaternion.identity);

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
    }
}
