using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using EasyTransition;
public class GameManager : MonoBehaviour
{

  public static GameManager instance;

  [Header("Level Info")]
  public int currentLevel = 1;
  // int totalLevels = 3;
  public int totalDataStation = 0;
  public int totalDataStationAnswered = 0;
  public int correctAnswerCount = 0;
  public TextMeshProUGUI dataStationText;



  [Header("Player Info")]
  // Cognitive Integrity is the player's health
  [HideInInspector]
  public int cognitiveIntegrity = 5;
  [HideInInspector]
  public int chipsCollected = 0;

  [SerializeField] GameObject playerPref;
  [HideInInspector]
  public GameObject activePlayerInstance;

  Transform playerSpawnPoint;
  public Transform activeCheckpoint;

  public bool isHome = true;
  public EasyTransition.TransitionManager transitionManager;

  void Awake()
  {
    MakeSingleton();

    if (!isHome)
    {
      playerSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
      if (playerSpawnPoint != null)
      {
        Debug.Log("Player Spawn Found");
      }
    }

  }

  void Start()
  {
    // SpawnPlayer();
    dataStationText.text = totalDataStation.ToString();
  }
  public void setSpawnPoint(Transform spawnPointPos)
  {
    playerSpawnPoint = spawnPointPos;
    Debug.Log("Spawn point set");
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
    }
  }

  void Update()
  {
    // FOR TESTING PURPOSE
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
      // SceneManager.LoadScene("Game");
      // resetLevelData();
      // playerDie();
    }
  }

  public void StartGame()
  {
    Debug.Log("Starting Game ....");
    isHome = false;
    SceneManager.LoadScene("Level" + currentLevel);
  }

  public void resetLevelData()
  {
    totalDataStation = 0;
    totalDataStationAnswered = 0;
    correctAnswerCount = 0;
  }

  public void ReduceLife()
  {
    cognitiveIntegrity--;
    Debug.Log("Life count: " + cognitiveIntegrity);

    if (cognitiveIntegrity <= 0)
    {
      // TODO: Call Game Over State
      Debug.Log("Game Over");
    }
  }

  public void SpawnPlayer()
  {
    activePlayerInstance = Instantiate(playerPref, playerSpawnPoint.position, Quaternion.identity);

    GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = activePlayerInstance.transform;
    // Resource.instance.setCameraFollow(activePlayerInstance);
  }

  public void switchActionMap(string mode)
  {
    if (mode == "enable")
    {
      activePlayerInstance.GetComponent<PlayerController>().switchActMap("enable");
    }
    else if (mode == "disable")
    {
      activePlayerInstance.GetComponent<PlayerController>().switchActMap("disable");
    }
  }

  public void RespawnPlayer()
  {
    StartCoroutine(RespawnCoroutine());
  }

  IEnumerator RespawnCoroutine()
  {
    Debug.Log("Respawning Player....");
    yield return new WaitForSeconds(2f);
    Debug.Log("Player Respawned");
    if (activeCheckpoint != null)
    {
      activePlayerInstance = Instantiate(playerPref, activeCheckpoint.position, Quaternion.identity);
      // GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = activePlayerInstance.transform;
      Resource.instance.setCameraFollow(activePlayerInstance);
    }
    else
    {
      activePlayerInstance = Instantiate(playerPref, playerSpawnPoint.position, Quaternion.identity);

      // GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = activePlayerInstance.transform;
      Resource.instance.setCameraFollow(activePlayerInstance);
    }
  }

  public void retryLevel()
  {
    UIManager.instance.hideGameOverPanel();
    resetLevelData();
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void continueToNextLevel()
  {
    UIManager.instance.hideGameOverPanel();
    resetLevelData();
    if (currentLevel == 3)
    {
      // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      UIManager.instance.hideControlPanel();
      Destroy(activePlayerInstance);
      SceneManager.LoadScene("EndGame");
    }
    else
    {
      currentLevel++;
      // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      SceneManager.LoadScene("Level" + currentLevel);
    }

  }

  public void backToMainMenu()
  {
    Destroy(activePlayerInstance);
    isHome = true;
    SceneManager.LoadScene("Main Menu");
  }

  #region  Data Station

  public void addDataStation()
  {
    totalDataStation++;
    dataStationText.text = totalDataStation.ToString();
  }

  public void updateActiveDataStation()
  {
    int activeDataStation = totalDataStation - totalDataStationAnswered;
    dataStationText.text = activeDataStation.ToString();
  }

  public void addDataStationAnswered()
  {
    totalDataStationAnswered++;
    updateActiveDataStation();
    // dataStationText.text = totalDataStation.ToString();
  }

  public void addCorrectAnswer()
  {
    correctAnswerCount++;
  }

  #endregion

  public void ExitGame()
  {
    Application.Quit();
  }
}
