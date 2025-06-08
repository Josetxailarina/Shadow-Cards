using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckEntry // Created to easily manage the deck entries in the inspector
{                      // because a Dictionary cannot be serialized directly

    public GameObject cardPrefab;
    public int cardCount;
}
public class PlayerDeck : MonoBehaviour
{
    [SerializeField] private PlayerHandLayout playerHandLayout;
    [SerializeField] private List<DeckEntry> initialDeck = new List<DeckEntry>();

    private Dictionary<GameObject, int> deck = new Dictionary<GameObject, int>();
    private int currentCardsInDeck = 0;

    private void Start()
    {
        SetInitialDeck();
        StartCoroutine(DrawInitialCards());
    }

    private void SetInitialDeck()
    {
        deck.Clear();
        foreach (var entry in initialDeck)
        {
            if (entry.cardPrefab != null && entry.cardCount > 0)
            {
                deck[entry.cardPrefab] = entry.cardCount;
                currentCardsInDeck += entry.cardCount;
            }
        }
    }

    IEnumerator DrawInitialCards()
    {
        GameManager.gameState = GameState.AutoMove;

        yield return new WaitForSecondsRealtime(0.5f);

        DrawCard(initialDeck[0].cardPrefab); // Make sure the first card is a Rat

        StartCoroutine(DrawSomeCards(4));

    }
    public IEnumerator DrawSomeCards(int cardAmount)
    {
        GameManager.gameState = GameState.AutoMove;

        for (int i = 0; i < cardAmount; i++)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            DrawRandomCard();
        }

        GameManager.gameState = GameState.Play;

    }
    public void DrawRandomCard()
    {
        GameObject card = GetRandomCard();
        if (card != null)
        {
            DrawCard(card);
            SoundManager.instance.takeCardSound.Play();   
        }
    }
    private GameObject GetRandomCard()
    {
        int randomIndex = Random.Range(0, currentCardsInDeck);
        foreach (var entry in deck)
        {
            if (randomIndex < entry.Value)
            {
                // Found the card
                deck[entry.Key]--;
                if (deck[entry.Key] == 0)
                {
                    deck.Remove(entry.Key); // Remove the card type if it is depleted
                }
                return entry.Key;
            }
            randomIndex -= entry.Value;
        }

        return null; // This should never happen
    }

    public void DrawCard(GameObject Card)
    {
        currentCardsInDeck--;
        GameObject instance = Instantiate(Card, transform, true);
        instance.transform.SetParent(playerHandLayout.gameObject.transform);
        instance.transform.position = transform.position;
        playerHandLayout.UpdateHand();
    }


}


