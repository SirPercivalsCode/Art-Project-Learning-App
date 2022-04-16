using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public List<Card> cardlist;
    public List<Card> cardQueue;
    public Card currentCard;
    private state gamestate;

    [Header("References")]
    public StatsManager statsManager;

    public Text answerDisplay;
    public Text questionDisplay;

    public GameObject winscreen;
    public GameObject infobox;

    public GameObject btnEasy;
    public GameObject btnHard;
    public GameObject btnDiscover;
    public GameObject cardGO;
    //public GameObject categoryPrefab;
    public GameObject startpanel;

    [System.Serializable]
    public class Card{
        public string question;
        public string answer;
        public int difficulty;
        public int startDifficulty = 3;
    }

    private enum state {Idle, Question, Answered, Finished};

    void Start()
    {
        gamestate = state.Idle;

        //ShowCategories();

    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && gamestate == state.Finished){
            BackToMainMenu();
        }

        if(Input.GetKeyUp(KeyCode.Space) && gamestate == state.Question){
            ShowAnswer();
        }
        if(gamestate == state.Answered)
        {
            if(Input.GetKeyUp("1"))
            {
                RateQuestion(true);
            }
            else if(Input.GetKeyUp("2"))
            {
                RateQuestion(false);
            }
        }
    }

    /*
    public void ShowCategories()
    {
        
    }
    */

    public void ShowAnswer()
    {
        gamestate = state.Answered;
        answerDisplay.text = currentCard.answer;


        btnEasy.SetActive(true);
        btnHard.SetActive(true);
        btnDiscover.SetActive(false);
    }

    public void RateQuestion(bool easy)
    {
        gamestate = state.Question;

        if(easy)
        {
            currentCard.difficulty--;
        }
        else
        {
            currentCard.difficulty++;
        }

        if(currentCard.difficulty <= 0)
        {
            cardQueue.Remove(currentCard);
            if(cardQueue.Count <= 0)
            {
                GameWon();
                return;
            }
        }

        PickNextCard();
        questionDisplay.text = currentCard.question;
        answerDisplay.text = "";

        btnEasy.SetActive(false);
        btnHard.SetActive(false);
        btnDiscover.SetActive(true);
    }

    public void StartGame()
    {
        //reset card difficulty
        foreach (Card card in cardlist)
        {
            card.difficulty = card.startDifficulty;
        }

        answerDisplay.text = "";

        cardQueue = new List<Card>(cardlist);


        gamestate = state.Question;
        PickNextCard();

        questionDisplay.text = currentCard.question;
        
        startpanel.SetActive(false);
        cardGO.SetActive(true);
        
        btnEasy.SetActive(false);
        btnHard.SetActive(false);
        btnDiscover.SetActive(true);
    }

    public void PickNextCard()
    {
        if(cardQueue.Count <= 0)
        {
            Debug.Log("Cardqueue is already empty.");
            return;
        }
        currentCard = cardQueue[Random.Range(0, cardQueue.Count)];
    }

    public void GameWon()
    {
        gamestate = state.Finished;
        statsManager.AddToScore(1);

        cardGO.SetActive(false);
        winscreen.SetActive(true);
    }

    public void BackToMainMenu()
    {
        gamestate = state.Idle;
        winscreen.SetActive(false);
        startpanel.SetActive(true);
    }

    public void ToggleInfoBox()
    {
        if(infobox.active)
        {
            infobox.SetActive(false);
        } else {
            infobox.SetActive(true);
        }
    }
}