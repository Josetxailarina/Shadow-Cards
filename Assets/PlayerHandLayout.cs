using System.Collections;
using UnityEngine;

public class PlayerHandLayout : MonoBehaviour
{
    [Header("Card Layout Settings")]
    [SerializeField] private float cardSpacing = 2.0f; // Default spacing between cards
    [SerializeField] private float maxHandWidth = 12.0f; // Maximum width allowed for the hand
    [SerializeField] private float transitionDuration = 0.5f; // Duration of the card movement animation

    private void Start()
    {
        UpdateHand();
    }

    public void UpdateHand()
    {
        StartCoroutine(UpdateHandCoroutine());
    }

    
    private IEnumerator UpdateHandCoroutine() // Simulates the horizontal layout group behavior
    {
        yield return new WaitForEndOfFrame();

        int cardCount = transform.childCount;
        if (cardCount == 0)
            yield break;

        // Collect all card transforms
        Transform[] cards = new Transform[cardCount];
        for (int i = 0; i < cardCount; i++)
            cards[i] = transform.GetChild(i);

        // Dynamically calculate spacing so all cards fit within maxHandWidth
        float currentSpacing = cardSpacing;
        if (cardCount > 1)
            currentSpacing = Mathf.Min(cardSpacing, maxHandWidth / (cardCount - 1));

        // Calculate the starting X position so the hand is centered
        float totalWidth = (cardCount - 1) * currentSpacing;
        float startX = -totalWidth / 2.0f;

        // Calculate target positions for each card
        Vector3[] targetPositions = new Vector3[cardCount];
        for (int i = 0; i < cardCount; i++)
            targetPositions[i] = new Vector3(startX + i * currentSpacing, 0, 0);

        // Store initial positions for smooth animation
        Vector3[] initialPositions = new Vector3[cardCount];
        for (int i = 0; i < cardCount; i++)
            initialPositions[i] = cards[i].localPosition;

        // Animate cards to their new positions
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            for (int i = 0; i < cardCount; i++)
                cards[i].localPosition = Vector3.Lerp(initialPositions[i], targetPositions[i], t);
            yield return null;
        }

        // Ensure all cards are exactly at their target positions and update sorting order
        for (int i = 0; i < cardCount; i++)
        {
            cards[i].localPosition = targetPositions[i];

            // Update sorting order for correct rendering
            CardBehavior cardScript = cards[i].GetComponentInChildren<CardBehavior>();
            if (cardScript != null)
                cardScript.SortingOrderUp(i + 5);
        }
    }
}
