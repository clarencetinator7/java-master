using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

  public static UIManager instance;


  [SerializeField] GameObject dataPanel;
  PlayerController pController;

  void Awake()
  {
    MakeSingleton();
  }

  void Start()
  {
    pController = GameObject.Find("Player").GetComponent<PlayerController>();
  }
  void MakeSingleton()

  {
    if (instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      instance = this;
      // DontDestroyOnLoad(gameObject);
    }
  }

  public void readData(string data, GameObject chipInstance)
  {
    // Select the text from the child of dataPanel
    dataPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = data;

    // Hide data panel after 5 seconds
    StartCoroutine(ToggleDataPanel(chipInstance));
  }

  // Data panel coroutine will hide after 5 seconds
  IEnumerator ToggleDataPanel(GameObject chipInstance)
  {
    dataPanel.SetActive(true);
    // STOP PLAYER MOVEMENT WHILE PANEL IS SHOWN  
    pController.switchActMap("disable");
    yield return new WaitForSeconds(5);
    dataPanel.SetActive(false);
    // Enable player movement
    pController.switchActMap("enable");
    // Destroy this gameObject
    Destroy(chipInstance);
  }

}
