using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, IInteractable
{
  [SerializeField] QuestionSO questionSO;
  [SerializeField] Animator animator;
  public bool isStationOpen = true;

  public void Start()
  {
    if (animator == null)
    {
      animator = GetComponent<Animator>();
    }

    if (questionSO == null)
    {
      closeStation();
      Debug.Log("No questionSO found on " + gameObject.name);
    }

  }

  public void Interact(GameObject interactor)
  {
    if (isStationOpen)
    {
      Debug.Log("Interacting with " + gameObject.name);
      // Create question
      Question question = new Question(questionSO);
      QuestionManager.instance.showQuestion(question, gameObject);
    }
  }

  public void closeStation()
  {
    isStationOpen = false;
    // Play close animation
    animator.SetBool("isActive", false);
  }

}
