using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoScript : MonoBehaviour
{
    public float cardSpacing = 2.0f; // La distancia entre las cartas
    public float transitionDuration = 0.5f; // Duración de la transición
    public int maxCardsWithoutReducingSpacing = 7; // Número máximo de cartas sin reducir el spacing

    private void Start()
    {
        ActualizarMano();
    }
    // Llamar a esta función para actualizar la disposición de las cartas
    public void ActualizarMano()
    {
        StartCoroutine(ActualizarManoCoroutine());
    }

    private IEnumerator ActualizarManoCoroutine()
    {
        yield return new WaitForEndOfFrame();
        Transform[] cards = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            cards[i] = transform.GetChild(i);
        }

        float currentSpacing = cardSpacing;
        if (cards.Length > maxCardsWithoutReducingSpacing)
        {
            currentSpacing /= 1.5f; // Reducir el spacing a la mitad si hay más de 7 cartas
        }

        float totalWidth = (cards.Length - 1) * currentSpacing;
        float startX = -totalWidth / 2.0f;

        Vector3[] targetPositions = new Vector3[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            targetPositions[i] = new Vector3(startX + i * currentSpacing, 0, 0);
        }

        float elapsedTime = 0;
        Vector3[] initialPositions = new Vector3[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            initialPositions[i] = cards[i].localPosition;
        }

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].localPosition = Vector3.Lerp(initialPositions[i], targetPositions[i], t);
            }
            yield return null;
        }

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].localPosition = targetPositions[i];

            // Ajustar el order in layer del SpriteRenderer
            SpriteRenderer sr = cards[i].GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i + 1;
            }
        }
    }
}
