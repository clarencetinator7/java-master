using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

  public static GameManager instance;


  // Player Stats
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

  void Awake()
  {
    MakeSingleton();
    playerSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
    if (playerSpawnPoint != null)
    {
      Debug.Log("Player Spawn Found");
    }
  }

  void Start()
  {
    // SpawnPlayer();
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
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
      // SceneManager.LoadScene("Game");
      playerDie();
    }
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

  public void playerDie()
  {
    Destroy(activePlayerInstance);
    // Play death animation
    // Play death sound
    // Play death particle
    respawnPlayer();
  }

  void respawnPlayer()
  {
    if (activeCheckpoint != null)
    {
      activePlayerInstance = Instantiate(playerPref, activeCheckpoint.position, Quaternion.identity);
      GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = activePlayerInstance.transform;
    }
    else
    {
      activePlayerInstance = Instantiate(playerPref, playerSpawnPoint.position, Quaternion.identity);

      GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = activePlayerInstance.transform;
    }

  }

}
