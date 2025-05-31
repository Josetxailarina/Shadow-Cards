using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckEntry
{
    public GameObject cardPrefab;
    public int cardCount;
}
public class MontonCartasScript : MonoBehaviour
{
    [SerializeField] private ManoScript manoScript;
    [SerializeField] private List<DeckEntry> initialDeck = new List<DeckEntry>();

    private Dictionary<GameObject, int> deck = new Dictionary<GameObject, int>();
    private AudioSource cardSound;

    private void Start()
    {
        cardSound = GetComponent<AudioSource>();
        SetInitialDeck();
        StartCoroutine(SacarIniciales());

    }

    private void SetInitialDeck()
    {
        deck.Clear();
        foreach (var entry in initialDeck)
        {
            if (entry.cardPrefab != null && entry.cardCount > 0)
            {
                deck[entry.cardPrefab] = entry.cardCount;
            }
        }
    }

    IEnumerator SacarIniciales()
    {
        GameManager.autoMove = true;
        yield return new WaitForSecondsRealtime(0.5f);
        SacarCarta(initialDeck[0].cardPrefab);
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
        GameManager.autoMove = false;

    }
    //private void OnMouseDown()
    //{
    //    SacarCartaRandom();
    //    print(GameManager.autoMove);
    //}
    public void SacarCartaRandom()
    {
        GameObject card = GetRandomCard();
        if (card != null)
        {
            SacarCarta(card);
            cardSound.Play();   
        }
    }
    public void Sacar2Random()
    {
        StartCoroutine(Sacar2Cartas());
    }
    IEnumerator Sacar2Cartas()
    {
        SacarCartaRandom();
        yield return new WaitForSecondsRealtime(0.2f);
        SacarCartaRandom();
    }

    private GameObject GetRandomCard()
    {
        int totalCards = 0;
        foreach (var entry in deck)
        {
            totalCards += entry.Value;
        }

        if (totalCards == 0)
        {
            return null; // No more cards in the deck
        }

        int randomIndex = Random.Range(0, totalCards);
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

    public void SacarCarta(GameObject Card)
    {
        cardSound.Play();

        GameObject instance = Instantiate(Card, transform, true);
        instance.transform.SetParent(manoScript.gameObject.transform);
        instance.transform.position = transform.position;
        manoScript.ActualizarMano();
    }
}
