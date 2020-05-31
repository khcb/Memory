using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public DeckShop deckShop;
  
    public void OpenShop()
    {
        
        //очистка перед открытием
        foreach (Transform child in transform) Destroy(child.gameObject);


        foreach (Deck deck in GameManager.instance.decks)
        {
            DeckShop newDeck = Instantiate(deckShop, transform.position, Quaternion.identity,transform);
            newDeck.Deck = deck;
            
        }
    }
}
