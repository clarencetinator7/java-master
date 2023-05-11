using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, IInteractable
{
  [SerializeField] GameObject questionManager;
  [SerializeField] QuestionSO questionSO;

  public void Interact()
  {
    Debug.Log("Interacting with " + gameObject.name);
    // Create question
    Question question = new Question(questionSO);
    // Show question ui
    questionManager.GetComponent<QuestionManager>().showQuestion(question);
  }
}
