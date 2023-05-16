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

  public static QuestionManager instance;

  Question currentQuestion;
  int correctAnswerIndex;
  static GameObject currentStation;

  [SerializeField] GameObject questionPanel;
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] Button answerButton1;
  [SerializeField] Button answerButton2;
  [SerializeField] Button answerButton3;

  PlayerController pController;

  private void Awake()
  {
    MakeSingleton();
  }

  private void MakeSingleton()
  {
    if (instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      instance = this;
      // DontDestroyOnLoad(gameObject);
    }
  }

  public void showQuestion(Question question, GameObject quizStation)
  {
    currentQuestion = question;
    currentStation = quizStation;

    GameManager.instance.switchActionMap("disable");
    UIManager.instance.hideControlPanel();

    // Set question text
    questionText.text = question.questionText;

    // Set answer text
    answerButton1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = question.answers[0];
    answerButton2.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = question.answers[1];
    answerButton3.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = question.answers[2];

    // Set correct answer index
    correctAnswerIndex = question.correctAnswerIndex;

    questionPanel.SetActive(true);

    // Hide question panel after 5 seconds
    StartCoroutine(questionTimer());

  }
  public void AnswerQuestion(int answerIndex)
  {
    if (answerIndex == correctAnswerIndex)
    {
      Debug.Log("Correct!");
      // TODO: Reward player

      // Play close animation
      currentStation.GetComponent<Interaction>().closeStation();

    }
    else
    {
      Debug.Log("Wrong!");
      // TODO: Punish player
    }
    //STOP ALL COROUTINES
    StopAllCoroutines();
    ClosePanel();
    // Enable player movement
    GameManager.instance.switchActionMap("enable");
    UIManager.instance.showControlPanel();
  }



  IEnumerator questionTimer()
  {
    Debug.Log("Question Timer");
    yield return new WaitForSeconds(10);
    questionPanel.SetActive(false);
    // Enable player movement
    GameManager.instance.switchActionMap("enable");
    ClosePanel();

  }

  public void ClosePanel()
  {
    questionPanel.SetActive(false);
    UIManager.instance.showControlPanel();
    currentQuestion = null;
    currentStation = null;
  }

}
