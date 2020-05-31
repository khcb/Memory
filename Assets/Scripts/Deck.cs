using System.Collections.Generic;
using UnityEngine;

//скрипт колоды карт
[CreateAssetMenu(fileName = "Deck", menuName = "ScriptableObjects/Deck", order = 2)]
public class Deck:ScriptableObject
{
    public int id;
    public bool isOpen;
    public Sprite deckSprite;//Обложка колоды
    public string title;//название колоды
    public List<CardInfo> cards;//массив карт
    public Sprite backSprite;//рубашка карт

    [SerializeField]
    private int lvl;

    public int Lvl {

        get => lvl;
        set
        {
            if (value >= 0 && value < cards.Count)
            {
                lvl = value;
            }
            else if (value < 0) lvl = 0;
            else
            {
                lvl = cards.Count - 1;
                if (id == GameManager.instance.IdEndDecks)//если id колоды которой играем равен IdEndDecks
                {
                    GameManager.instance.IdEndDecks++;//увеличиваем до куда открыты карты
                }
            }

            GameManager.instance.fieldGame.CreateField();
        }
    }
}
