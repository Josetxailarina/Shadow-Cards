using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MontonCartasScript : MonoBehaviour
{
    public ManoScript manoScript;
    public GameObject rat;
    public GameObject cat;
    public GameObject dog;
    public GameObject horse;
    public GameObject elephant;
    public GameObject dragon;
    public GameObject whale;
    public GameObject effigy;
    public GameObject fire;
    public GameObject wind;
    public GameObject water;

    private Dictionary<GameObject, int> deck = new Dictionary<GameObject, int>();

    private void Start()
    {
        // Initialize the deck
        deck[rat] = 3;
        deck[cat] = 3;
        deck[dog] = 3;
        deck[horse] = 3;
        deck[elephant] = 3;
        deck[dragon] = 1;
        deck[whale] = 1;
        deck[effigy] = 1;
        deck[fire] = 3;
        deck[wind] = 3;
        deck[water] = 3;

        StartCoroutine(SacarIniciales());
    }

    IEnumerator SacarIniciales()
    {
        GameManager.autoMove = true;
        yield return new WaitForSecondsRealtime(1);
        SacarCarta(rat);
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
        yield return new WaitForSecondsRealtime(0.3f);
        SacarCartaRandom();
    }
    private void OnMouseDown()
    {
        SacarCartaRandom();
    }
    public void SacarCartaRandom()
    {
        GameObject card = GetRandomCard();
        if (card != null)
        {
            SacarCarta(card);
        }
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
        GameObject instance = Instantiate(Card, transform, true);
        instance.transform.SetParent(manoScript.gameObject.transform);
        instance.transform.position = transform.position;
        manoScript.ActualizarMano();
    }
}
