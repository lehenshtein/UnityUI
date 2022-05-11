using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();

    [Header("Answers")]
    [SerializeField] GameObject[] buttons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete = false;
    
    void Awake() {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        // currentQuestion = ScriptableObject.CreateInstance<QuestionSO>();
        currentQuestion = FindObjectOfType<QuestionSO>();
    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion && questions.Count > 0) {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) {
            DisplayAnswer(-1);
            ToggleButtonsActivity(false);
        }
    }

    void SetVariables() {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        questionText.text = currentQuestion.GetQuestion();
    }

    void FillQuiz() {
        for (int i = 0; i < buttons.Length; i++) {
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswerByIndex(i);
        }
    }

    void GetNextQuestion() {
        progressBar.value++;
        scoreKeeper.IncrementQuestionsSeen();
        ToggleButtonsActivity(true);
        SetButtonsDefaultSprite();
        GetRandomQuestion();
        SetVariables();
        FillQuiz();
    }

    void GetRandomQuestion() {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
        }
    }

    void ToggleButtonsActivity(bool active) {
        for (int i = 0; i < buttons.Length; i++) {
            Button button = buttons[i].GetComponent<Button>();
            button.interactable = active;
        }
    }

    void SetButtonsDefaultSprite() {
        for (int i = 0; i < buttons.Length; i++) {
            Image buttonImage = buttons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        ToggleButtonsActivity(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if(progressBar.value == progressBar.maxValue) {
            isComplete = true;
        }
    }

    void DisplayAnswer(int index) {
        if (index == correctAnswerIndex) {
            questionText.text = "Correct";
            scoreKeeper.IncrementCorrectAnswers();
        } else {
            string correctAnswer = currentQuestion.GetAnswerByIndex(correctAnswerIndex);
            questionText.text = "Wrong! The correct answer is:\n" + correctAnswer;
        }
        Image buttonImage = buttons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }

}
