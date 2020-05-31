using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardGame : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Image imageFace = null;
    [SerializeField]
    private Image imageBack = null;

    private CardInfo cardInfo;

    public CardInfo CardInfo
    {
        get => cardInfo;
        set
        {
            cardInfo = value;

            if (cardInfo.faceSprite != null)
                imageFace.sprite = cardInfo.faceSprite;

            if (imageBack != null)
                imageBack.gameObject.SetActive(true);

            if (GameManager.instance.deckCurrent.backSprite != null)
                imageBack.sprite = GameManager.instance.deckCurrent.backSprite;
        }
    }

    private bool isGood;
    public bool IsGood
    {
        get => isGood;
        set
        {
            isGood = value;
            if (!value) Show();
        }
    }

    public void Show()
    {
        imageBack.gameObject.SetActive(false);
    }
    public void Hide()
    {
        imageBack.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isGood && imageBack.gameObject.activeInHierarchy) GameManager.instance.fieldGame.ClickCard(this);
    }
}
