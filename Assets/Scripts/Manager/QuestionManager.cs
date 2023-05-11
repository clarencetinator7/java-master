using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question
{
  public string questionText;
  public string[] answers;
  public int correctAnswerIndex;

  // SAVE
  // public Question(string questionText, string[] answers, int correctAnswerIndex)
  // {
  //   this.questionText = questionText;
  //   this.answers = answers;
  //   this.correctAnswerIndex = correctAnswerIndex;
  // }

  public Question(QuestionSO questionSO)
  {
    this.questionText = questionSO.questionText;
    this.answers = questionSO.answerChoices;
    this.correctAnswerIndex = questionSO.correctAnswerIndex;
  }
}

public class QuestionManager : MonoBehaviour
{

  Question currentQuestion;
  int correctAnswerIndex;

  [SerializeField] GameObject questionPanel;
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] Button answerButton1;
  [SerializeField] Button answerButton2;
  [SerializeField] Button answerButton3;

  public void showQuestion(Question question)
  {
    currentQuestion = question;

    // Set question text
    questionPanel.transform.Find("QuestionText").GetComponent<TMPro.TextMeshProUGUI>().text = question.questionText;

    // Set answer text
    answerButton1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = question.answers[0];
    answerButton2.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = question.answers[1];
    answerButton3.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = question.answers[2];

    // Set correct answer index
    correctAnswerIndex = question.correctAnswerIndex;

    questionPanel.SetActive(true);
  }

  public void AnswerQuestion(int answerIndex)
  {
    if (answerIndex == correctAnswerIndex)
    {
      Debug.Log("Correct!");
      // Reward player
    }
    else
    {
      Debug.Log("Wrong!");
      // Punish player
    }
    questionPanel.SetActive(false);
  }

}
