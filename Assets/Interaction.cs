using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interaction : MonoBehaviour, IInteractable
{

  [SerializeField] GameObject questionPanel;

  public void Interact()
  {
    Debug.Log("Interacting with " + gameObject.name);
    questionPanel.SetActive(true);
    // Test change
  }
}
