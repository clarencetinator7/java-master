using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, IInteractable
{

  [SerializeField] GameObject questionPanel;

  // Question text mesh pro
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] Button answerButton1;
  [SerializeField] Button answerButton2;
  [SerializeField] Button answerButton3;
  [SerializeField] int correctAnswerIndex;

  public void Interact()
  {
    Debug.Log("Interacting with " + gameObject.name);

    // Set question text
    questionText.text = "What is the capital of France?";

    // Set answer text
    answerButton1.GetComponentInChildren<TextMeshProUGUI>().text = "Paris";
    answerButton2.GetComponentInChildren<TextMeshProUGUI>().text = "London";
    answerButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Berlin";

    correctAnswerIndex = 0;


    questionPanel.SetActive(true);
  }

  public void AnswerQuestion(int answerIndex)
  {
    if (answerIndex == correctAnswerIndex)
    {
      Debug.Log("Correct!");
    }
    else
    {
      Debug.Log("Wrong!");
    }
    questionPanel.SetActive(false);
  }
}
