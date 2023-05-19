using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
  public void Interact(GameObject interactor)
  {
    Debug.Log("Interacting with " + gameObject.name);
    // Set checkpoint
    GameManager.instance.activeCheckpoint = gameObject.transform;
    Debug.Log("Checkpoint set to " + GameManager.instance.activeCheckpoint);
  }

}
