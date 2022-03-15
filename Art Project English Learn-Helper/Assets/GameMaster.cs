using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public List<Card> cardlist;
    public List<Card> cardQueue;
    public Card currentCard;

    [Header("References")]
    public Text answerDisplay;
    public Text questionDisplay;

    public GameObject btnEasy;
    public GameObject btnHard;
    public GameObject btnDiscover;
    private state gamestate;

    [System.Serializable]
    public class Card{
        public string question;
        public string answer;
        public int difficulty = 3;
    }

    private enum state {Idle, Question, Answered, Finished};

    void Start()
    {
        gamestate = state.Idle;

        cardQueue = new List<Card>(cardlist);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && gamestate == state.Question){
            ShowAnswer(currentCard);
        }
        if(Input.GetKeyUp("1") && gamestate == state.Answered){
            RateQuestion(true);
        }
        if(Input.GetKeyUp("2") && gamestate == state.Answered){
            RateQuestion(false);
        }
    }

    public void ShowAnswer(Card card)
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
        
        Debug.Log("Difficulty of " + currentCard.question + ": " + currentCard.difficulty);

        if(currentCard.difficulty <= 0)
        {
            Debug.Log("Removing card " + currentCard.question);
            cardQueue.Remove(currentCard);
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
        gamestate = state.Question;
        PickNextCard();

        questionDisplay.text = currentCard.question;
        
        btnEasy.SetActive(false);
        btnHard.SetActive(false);
        btnDiscover.SetActive(true);
    }

    public void PickNextCard()
    {
        currentCard = cardQueue[Random.Range(0, cardQueue.Count)];
    }
}