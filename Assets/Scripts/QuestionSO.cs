using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 5)][SerializeField] string question = "Add a new question text here";
    [SerializeField] string[] answers = {"Add answer 1", "Add answer 2", "Add answer 3", "Add answer 4"};
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion() {
        return question;
    }

    public string[] GetAnswers() {
        return answers;
    }

    public string GetAnswerByIndex(int index) {
        return answers[index];
    }

    public int GetCorrectAnswerIndex() {
        return correctAnswerIndex;
    }
  
}
