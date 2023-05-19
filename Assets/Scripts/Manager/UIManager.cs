using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

  public static UIManager instance;


  [SerializeField] GameObject dataPanel;
  [SerializeField] TextMeshProUGUI dataText;
  [SerializeField] GameObject controlsPanel;

  void Awake()
  {
    MakeSingleton();
  }

  void Start()
  {

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

  public void readData(DatachipsSO dataChip, GameObject chipInstance)
  {
    // Select the text from the child of dataPanel
    // dataPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = data;
    dataText.text = dataChip.datachipText;
    dataPanel.SetActive(true);
    Destroy(chipInstance);
  }

  public void hideDataPanel()
  {
    dataPanel.SetActive(false);
  }

  public void showControlPanel()
  {
    StartCoroutine(showControlPanelDelay());
  }

  // <summary>
  // Delay due to a bug where it would throw an error.
  // More details here: 
  // https://stackoverflow.com/questions/68604217/unity-new-input-system-touch-menu-error
  // </summary>

  IEnumerator showControlPanelDelay()
  {
    yield return new WaitForSeconds(.5f);
    controlsPanel.SetActive(true);
  }

  public void hideControlPanel()
  {
    StartCoroutine(hideControlPanelDelay());
  }

  IEnumerator hideControlPanelDelay()
  {
    yield return new WaitForSeconds(1);
    controlsPanel.SetActive(false);
  }

}
