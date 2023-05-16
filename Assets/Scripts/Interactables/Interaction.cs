using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, IInteractable
{
  [SerializeField] GameObject questionManager;
  [SerializeField] QuestionSO questionSO;
  [SerializeField] Animator animator;
  public bool isStationOpen = true;

  public void Start()
  {
    if (animator == null)
    {
      animator = GetComponent<Animator>();
    }
  }

  public void Interact()
  {
    if (isStationOpen)
    {
      Debug.Log("Interacting with " + gameObject.name);
      // Create question
      Question question = new Question(questionSO);
      // Show question ui
      questionManager.GetComponent<QuestionManager>().showQuestion(question, gameObject);
    }
  }

  public void closeStation()
  {
    isStationOpen = false;
    // Play close animation
    animator.SetBool("isActive", false);
  }

}
