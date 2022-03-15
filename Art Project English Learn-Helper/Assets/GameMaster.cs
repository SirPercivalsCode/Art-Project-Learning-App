using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Card[] cardlist;
    public List<Card> cardQueue;
    public Card currentCard;
    public Card nextCard;

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

        for (int i = 0; i < cardlist.Length; i++)
        {
            Card randomCard = cardlist[Random.Range(0, cardlist.Length)];
            cardQueue.Append(randomCard);
        }
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

        currentCard = cardlist[Random.Range(0, cardlist.Length)];
        

        btnEasy.SetActive(true);
        btnHard.SetActive(true);
        btnDiscover.SetActive(false);
    }

    public void RateQuestion(bool easy)
    {
        gamestate = state.Question;
        questionDisplay.text = currentCard.question;
        answerDisplay.text = null;

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
        }

        btnEasy.SetActive(false);
        btnHard.SetActive(false);
        btnDiscover.SetActive(true);
    }

    public void StartGame()
    {
        gamestate = state.Question;
    }

    public void PickNextCard()
    {
        nextCard = cardlist[Random.Range(0, cardlist.Length)];
        nextCard.difficulty--;
    }
}