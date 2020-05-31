using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MenuGame : MonoBehaviour
{
    [SerializeField]
    private Text title = null;


    [SerializeField]
    private Button buttonPlay = null;
    [SerializeField]
    private Text textButtonPlay = null;

    private Image image;
    private string gameId = "3615418";


    private void Start()
    {
        image = GetComponent<Image>();
        gameObject.SetActive(false);
        Advertisement.Initialize(gameId, false);
    }

    public void ActivatePanel()//Запуск меню паузы
    {
        buttonPlay.gameObject.SetActive(true);
        title.text = "Пауза";
        textButtonPlay.text = "Продолжить игру";
        buttonPlay.onClick.RemoveAllListeners();
        buttonPlay.onClick.AddListener(Play);

        GameManager.instance.Score = 0;
        gameObject.SetActive(true);
        StartCoroutine(AnimOpen());
        
    }

    public void ActivatePanel(float score)//Запуск мены при победе
    {

        if (GameManager.instance.isNewDeck)
        {
            Advertisement.Show();
            title.text = "Молодец!!!\nОткрыты новые карточки";
        }
        else title.text = "Молодец";

        buttonPlay.gameObject.SetActive(true);
        buttonPlay.onClick.RemoveAllListeners();
        buttonPlay.onClick.AddListener(NextLVL);
        textButtonPlay.text = "Следующий\nуровень";
        
        GameManager.instance.Score = score;
        gameObject.SetActive(true);
        StartCoroutine(AnimOpen());
    }

    public void ActivatePanel(bool lose)//Запуск мены при победе
    {
        if (lose)
        {
            title.text = "Неудача";
            buttonPlay.gameObject.SetActive(false);
            GameManager.instance.Score = 0;
            gameObject.SetActive(true);
            StartCoroutine(AnimOpen());
        }
    }

    public void Play()
    {
        GameManager.instance.isNewDeck = false;
        gameObject.SetActive(false);
    }

    public void NextLVL()
    {
        GameManager.instance.deckCurrent.Lvl++;
        gameObject.SetActive(false);
    }

    

    IEnumerator AnimOpen()
    {
        
        //Time.timeScale = 0;
        Color thisColor = image.color;
        thisColor.a = 0f;
        image.color = thisColor;

        for (float i = 0f; i <= .9f; i+=0.1f)
        {
            thisColor.a = i;
            image.color = thisColor;
            yield return new WaitForSeconds(0.03f);
        }

    }
}
