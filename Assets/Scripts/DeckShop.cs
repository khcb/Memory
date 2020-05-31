using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckShop : MonoBehaviour, IPointerDownHandler
{
    public Image image;
    public GameObject close;
    public Text title;
    public Image activate;

    private Deck deck;

    public Deck Deck
    {
        get => deck;
        set
        {
            deck = value;
            image.sprite = deck.deckSprite;
            title.text = deck.title;

            if (deck.isOpen)//Если колода открыта
            {
                close.SetActive(false);
                if (deck.title == GameManager.instance.deckCurrent.title)
                {
                    activate.gameObject.SetActive(true);
                }
                else
                {
                    activate.gameObject.SetActive(false);
                }
            }
            else//Если колода закрыта
            {
                close.SetActive(true);
                activate.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
        if (deck.isOpen)
        {
            if (deck.title != GameManager.instance.deckCurrent.title)
            {
                GameManager.instance.deckCurrent = deck;
                
                GameManager.instance.shop.OpenShop();
            }
        }
    }
}
