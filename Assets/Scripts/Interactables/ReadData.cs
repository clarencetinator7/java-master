using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour, IInteractable
{

  [SerializeField] DatachipsSO datachip;

  public void Start()
  {
    if (datachip == null)
    {
      Debug.Log(gameObject.name + " is null");
    }
  }

  public void Interact(GameObject interactor)
  {
    Debug.Log("Interacting with " + gameObject.name);
    UIManager.instance.readData(datachip, gameObject);

  }

}
