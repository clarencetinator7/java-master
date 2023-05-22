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
  int currentStationAttempts = 0;

  [SerializeField] GameObject questionPanel;
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] Button answerButton1;
  [SerializeField] Button answerButton2;
  [SerializeField] Button answerButton3;

  PlayerController pController;

  private void Awake()
  {
    MakeSingleton();

    // ((Number of Attempts - 1) / (3 - 1)) * (1000 - ((Time Answered * 1000) / 10)) + 100

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
    currentStationAttempts = currentStation.GetComponent<Interaction>().attempts;

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
      // Count as completed
      GameManager.instance.addDataStationAnswered();
      // Count as correct answer
      GameManager.instance.correctAnswerCount++;
      currentStation.GetComponent<Interaction>().attempts++;
      ClosePanel();
      UIManager.instance.showNotificationPanel("Data Station Restored....", 3f);
      return;
    }
    else
    {
      UIManager.instance.showNotificationPanel("ERROR: Access Denied....", 2f);
      // Count as completed
      // GameManager.instance.totalDataStationAnswered++;
      // TODO: Punish player
    }
    //STOP ALL COROUTINES
    increaseAttempts();
    ClosePanel();
  }



  IEnumerator questionTimer()
  {
    Debug.Log("Question Timer");
    yield return new WaitForSeconds(10);
    questionPanel.SetActive(false);
    ClosePanel();
  }

  public void ClosePanel()
  {
    StopAllCoroutines();
    questionPanel.SetActive(false);
    UIManager.instance.showControlPanel();
    GameManager.instance.switchActionMap("enable");
    currentQuestion = null;
    currentStation = null;
    currentStationAttempts = 0;
  }

  public void increaseAttempts()
  {
    currentStationAttempts++;
    currentStation.GetComponent<Interaction>().attempts = currentStationAttempts;
    Debug.Log("Attempts: " + currentStationAttempts);
    if (currentStationAttempts >= 3)
    {
      currentStation.GetComponent<Interaction>().closeStation();
      GameManager.instance.addDataStationAnswered();
      UIManager.instance.showNotificationPanel("You got it wrong 3 times.... \n ERROR: Data Station Lockdown Initiated", 3f);
      // ClosePanel();
      return;
    }
    // ClosePanel();
  }

}
