using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public GameObject canvasShop;
    public GameObject canvasGame;
    public static GameManager instance = null;
    public Deck deckCurrent = null;
    public List<Deck> decks = new List<Deck>();//Колоды
    public Field fieldGame = null;
    public MenuGame menuGame = null;
    public Shop shop = null;
    public Text scoreText = null;
    public bool isNewDeck;

    private AudioSource audioPlayer;

    private int currLVL;
    private float score = 0;
    private int failedCount;
    private int idEndDecks;


    

    public float Score
    {
        get => score;
        set
        {
            score = value;
            if (scoreText != null)
            {
                if (score == 0) scoreText.text = "";//если прилетел ноль скрываем поле
                else scoreText.text = "Скорость: \n\n" + score.ToString("0.00");
            }
            
        }
    }

    public int FailedCount
    {
        get => failedCount;
        set
        {
            if (value >= 0 && value < deckCurrent.Lvl- 1)//Даем попыток в зависимости от уровня -1
            {
                failedCount = value;
            }
            else if (value >= deckCurrent.Lvl - 1)
            {
                failedCount = value;
                menuGame.ActivatePanel(true);
            }
            else failedCount = 0; 
            
        }
    }

    public int IdEndDecks
    {
        get => idEndDecks;
        set
        {
            
            if (value >= 0 && value < decks.Count)
            {
                isNewDeck = !decks[value].isOpen;
                decks[value].isOpen = true;

                
                
                idEndDecks = value;
            }
            
        }
    }

    void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGame();


    }

    //Initializes the game for each level.
    void InitGame()
    {
        NextScene(0);
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        
        audioPlayer.PlayOneShot(audioClip);
    }

    public void NextScene(int id)
    {
        if (id == 0)//0 если игра
        {
            canvasGame.SetActive(true);
            canvasShop.SetActive(false);
        }
        if (id == 1)//1 если меню
        {
            canvasGame.SetActive(false);
            canvasShop.SetActive(true);
        }
    }

}


