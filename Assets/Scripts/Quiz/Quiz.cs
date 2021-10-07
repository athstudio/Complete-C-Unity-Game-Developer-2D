using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currectQuestion;
    
    [Header("Answers")]
    [SerializeField] GameObject[] answerBtn;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
    
    [Header("Btn Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;


    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
            isComplete = true;
            return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetBtnState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetBtnState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore()+ "%";
    }

    void DisplayAnswer(int index)
    {
        Image btnImage;

        if(index == currectQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            btnImage = answerBtn[index].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else{
            
            //btnImage = answerBtn[index].GetComponent<Image>();
            //btnImage.sprite = wrongAnswerSprite;

            correctAnswerIndex = currectQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currectQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            btnImage = answerBtn[correctAnswerIndex].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
            }
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetBtnState(true);
            GetRandomQuestion();
            SetDefaultBtnSprites();
            DispleyQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currectQuestion = questions[index];

        if(questions.Contains(currectQuestion))
        {
            questions.Remove(currectQuestion);
        }
    }

    void DispleyQuestion()
    {
         questionText.text = currectQuestion.GetQuestion();

        for(int i = 0; i < answerBtn.Length; i++)
        {
            TextMeshProUGUI btnText = answerBtn[i].GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = currectQuestion.GetAnswer(i); 
        }
    }

    void SetBtnState(bool state)
    {
        for(int i = 0; i < answerBtn.Length; i++)
        {
            Button btn = answerBtn[i].GetComponent<Button>();
            btn.interactable = state;
        }
    }

    void SetDefaultBtnSprites()
    {
        for(int i = 0; i < answerBtn.Length; i++)
        {
            Image btnImage = answerBtn[i].GetComponent<Image>();
            btnImage.sprite = defaultAnswerSprite;
        }
    }
}
