using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour, IInteractable
{

  [SerializeField] string chipData = "This chip has no data.";

  [SerializeField] GameObject dataPanel;

  PlayerController pController;

  public void Start()
  {
    pController = GameObject.Find("Player").GetComponent<PlayerController>();
  }

  public void Interact(GameObject interactor)
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
    // STOP PLAYER MOVEMENT WHILE PANEL IS SHOWN  
    pController.switchActMap("disable");
    yield return new WaitForSeconds(5);
    dataPanel.SetActive(false);
    // Enable player movement
    pController.switchActMap("enable");
    // Destroy this gameObject
    Destroy(gameObject);
  }

}
