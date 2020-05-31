using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject cardPref;
    public AudioClip clickSound;
    public AudioClip winSound;

    private Deck currDeck;
    private MenuGame menuGame;
    private List<CardGame> cardsGame = new List<CardGame>();

    private int clickCount;

    private CardGame cardClickOne;
    private CardGame cardClickTwo;
    private GameManager gameManager;
    

    private void Start()
    {
        GameManager.instance.deckCurrent.Lvl = 3;
    }


    public void CreateField()
    {
        StopAllCoroutines();
        //Чистим при создании поля
        cardsGame.Clear();
        clickCount = 1;
        GameManager.instance.FailedCount = 0;
        foreach (Transform child in transform) Destroy(child.gameObject);
      


        /*
             Создадим виртуальную колоду исходя из текущего уровня с повторами по 2 карты
             и перемешаем ее
             затем присвоим картам на поле
        */
        List<CardInfo> cardsInfo = new List<CardInfo>();
        cardsInfo.Clear();
        for (int id_card = 0; id_card < GameManager.instance.deckCurrent.Lvl; id_card++)
        {
            for (int round = 0; round < 2; round++)//повтор
            {
                cardsInfo.Add(GameManager.instance.deckCurrent.cards[id_card]);
            }

        }
        //Перемешиваем карты случайным образом
        for (int i = cardsInfo.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            // обменять значения cardsInfo[j] и cardsInfo[i]
            var temp = cardsInfo[j];
            cardsInfo[j] = cardsInfo[i];
            cardsInfo[i] = temp;
        }

        //Тут создаем карты в самой игре
        for (int i = 0; i < GameManager.instance.deckCurrent.Lvl * 2; i++)
        {

            CardGame card = Instantiate(cardPref, transform)
                .GetComponent<CardGame>();

            card.CardInfo = cardsInfo[i];
            card.IsGood = false;
            cardsGame.Add(card);
        }

        StartCoroutine(ShowCards());//Запускаем показ карт
    }

    public void ClickCard(CardGame cardGame)
    {

        GameManager.instance.PlaySound(clickSound);
        cardGame.Show();

        if (clickCount % 2 != 0 && clickCount != 1)
        {
            //если карты совпали оставляем открытыми и проверяем на выигрыш
            if (cardClickOne.CardInfo.id == cardClickTwo.CardInfo.id)
            {
                cardClickOne.IsGood = true;
                cardClickTwo.IsGood = true;
                Win();//проверка на выигрыш
            }
            else //карты не совпали
            {
                GameManager.instance.FailedCount++;
                cardClickOne.Hide();
                cardClickTwo.Hide();
            }
            cardClickOne = cardGame;
        }
        else if (clickCount % 2 == 0)
        {
            cardClickTwo = cardGame;
            if (cardClickOne.CardInfo.id == cardClickTwo.CardInfo.id)
            {
                cardClickOne.IsGood = true;
                cardClickTwo.IsGood = true;
                Win();//проверка на выигрыш
            }
        }
        else//click==1
        {
            cardClickOne = cardGame;
        }



        clickCount++;
    }

    IEnumerator ShowCards()
    {
        foreach (CardGame card in cardsGame)
        {
            card.Show();
        }
        yield return new WaitForSeconds(GameManager.instance.deckCurrent.Lvl / 2);
        foreach (CardGame card in cardsGame)
        {
            card.Hide();
        }
    }

    private bool Win()
    {
        //пробегаем по всем картам и если все открыты то выиграли
        bool isWin = true;
        foreach (CardGame card in cardsGame)
        {
            if (!card.IsGood) isWin = false;
        }

        if (isWin)
        {
            GameManager.instance.PlaySound(winSound);
            GameManager.instance.menuGame.ActivatePanel(clickCount * Time.time);
        }

        return isWin;
    }


}
