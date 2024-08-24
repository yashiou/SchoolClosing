using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();

    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return null;
        }
        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
    }
}