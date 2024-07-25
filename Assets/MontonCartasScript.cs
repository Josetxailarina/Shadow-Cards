using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontonCartasScript : MonoBehaviour
{
    public ManoScript manoScript;
    public GameObject prefabCard;

    private void OnMouseDown()
    {
        SacarCarta();
    }
    public void SacarCarta()
    {
        GameObject instance = Instantiate(prefabCard, transform,true);
        instance.transform.SetParent(manoScript.gameObject.transform);
        instance.transform.position = transform.position;
        manoScript.ActualizarMano();
    }
}
