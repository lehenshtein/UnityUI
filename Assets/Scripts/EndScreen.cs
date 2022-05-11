using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        
    }

    public void ShowFinalScore() {
        finalScoreText.text = "You win!\n" +
            "Your score is: " + scoreKeeper.CalculateScore() + "%";
    }

}
