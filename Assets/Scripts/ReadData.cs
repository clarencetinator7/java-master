using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour, IInteractable
{

  [SerializeField] string chipData = "This chip has no data.";

  [SerializeField] GameObject dataPanel;

  public void Interact()
  {
    Debug.Log("Interacting with " + gameObject.name);
    // Read data from chip
    readData(chipData);
  }

  public void readData(string data)
  {
    // Select the text from the child of dataPanel
    dataPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = data;
    dataPanel.SetActive(true);

    // Hide data panel after 5 seconds
    StartCoroutine(HideDataPanel());

  }

  // Data panel coroutine will hide after 5 seconds
  IEnumerator HideDataPanel()
  {
    yield return new WaitForSeconds(5);
    dataPanel.SetActive(false);
    Debug.Log("Data panel hidden");
  }

}
