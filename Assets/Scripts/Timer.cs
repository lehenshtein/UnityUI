using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool loadNextQuestion;
    public bool isAnsweringQuestion = true;
    public float fillFraction;
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;
    void Update() {
        UpdateTimer();
    }

    public void CancelTimer() {
        timerValue = 0;
    }

    public void UpdateTimer() {
        timerValue -= Time.deltaTime;
        
        if (isAnsweringQuestion) {
            if (timerValue > 0) {
                fillFraction = timerValue / timeToCompleteQuestion;
            } else {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
        } else {
            if (timerValue > 0) {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            } else {
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
        }

        // Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}
