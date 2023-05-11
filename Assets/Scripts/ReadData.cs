using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour, IInteractable
{

  [SerializeField] string chipData = "This chip has no data.";

  public void Interact()
  {
    Debug.Log("Interacting with " + gameObject.name);
    // Read data from chip
    Debug.Log(chipData);
  }

}
