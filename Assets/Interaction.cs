using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, IInteractable
{

  [SerializeField] GameObject questionManager;

  [SerializeField] string questionText;
  [SerializeField] string[] answers;
  [SerializeField] int correctAnswerIndex;


  public void Interact()
  {
    Debug.Log("Interacting with " + gameObject.name);

    // Create question
    Question question = new Question(questionText, answers, correctAnswerIndex);
    questionManager.GetComponent<QuestionManager>().showQuestion(question);

  }

}
