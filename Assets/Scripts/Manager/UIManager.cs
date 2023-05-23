using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

  public static UIManager instance;

  [Header("Data Panel")]
  [SerializeField] GameObject dataPanel;
  [SerializeField] TextMeshProUGUI dataText;

  [Header("Control Panel")]
  [SerializeField] GameObject controlsPanel;

  [Header("Main Menu Panel")]
  [SerializeField] GameObject mainMenuPanel;

  [Header("Game Over Panel")]
  [SerializeField] GameObject gameOverPanel;
  [SerializeField] TextMeshProUGUI gameOverScoreText;

  [Header("Notification Panel")]
  [SerializeField] GameObject notificationPanel;
  [SerializeField] TextMeshProUGUI notificationText;

  [Header("Pause Menu Panel")]
  [SerializeField] GameObject pauseMenuPanel;

  [Header("Miscs")]
  [SerializeField] AudioClip tapSound;

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

  #region Control Panel
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
    yield return new WaitForSeconds(.5f);
    controlsPanel.SetActive(false);
  }
  #endregion

  public void onStartGameClickHandler()
  {
    Debug.Log("Start Game Clicked");
    StartCoroutine(onStartGameCoroutine());
  }

  IEnumerator onStartGameCoroutine()
  {

    GameManager.instance.StartGame();
    yield return new WaitForSeconds(1f);
    mainMenuPanel.SetActive(false);
    showControlPanel();

  }

  #region Game Over Panel

  public void showGameOverPanel()
  {
    string correctAnswerCount = GameManager.instance.correctAnswerCount.ToString();
    string totalDataStation = GameManager.instance.totalDataStation.ToString();

    gameOverScoreText.text = correctAnswerCount + " of " + totalDataStation + " data stations answered correctly.";

    gameOverPanel.SetActive(true);
  }

  public void hideGameOverPanel()
  {
    gameOverPanel.SetActive(false);
  }

  #endregion

  #region Notification Panel

  public void showNotificationPanel(string notification, float duration)
  {
    notificationText.text = notification;
    StartCoroutine(showNotificationPanelCoroutine(duration));
  }

  IEnumerator showNotificationPanelCoroutine(float duration)
  {
    notificationPanel.SetActive(true);
    yield return new WaitForSeconds(duration);
    notificationPanel.SetActive(false);
  }

  #endregion

  #region Pause Menu Panel

  public void onPauseGameHandler()
  {
    Time.timeScale = 0;
    pauseMenuPanel.SetActive(true);
  }

  public void onResumeGameHandler()
  {
    Time.timeScale = 1;
    pauseMenuPanel.SetActive(false);
  }

  public void backToMainMenu()
  {
    Time.timeScale = 1;
    pauseMenuPanel.SetActive(false);
    controlsPanel.SetActive(false);
    GameManager.instance.backToMainMenu();
    mainMenuPanel.SetActive(true);
  }

  public void retryGameHandler()
  {
    Time.timeScale = 1;
    pauseMenuPanel.SetActive(false);
    GameManager.instance.retryLevel();
  }

  #endregion

  public void playTapSound()
  {
    SoundManager.instance.playSound(tapSound);
  }

  public void showMainMenuPanel()
  {
    mainMenuPanel.SetActive(true);
  }


}
