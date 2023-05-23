using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  public bool isDying = false;

  [Header("Audio")]
  [SerializeField] AudioClip dieSound;

  private void Start()
  {
    GameManager.instance.activePlayerInstance = gameObject;
    DontDestroyOnLoad(gameObject);
  }

  public void Die()
  {
    Debug.Log("Player died");
    // Play die sound effect
    SoundManager.instance.playSound(dieSound);
    // Play particle effect
    GameManager.instance.RespawnPlayer();
    Destroy(gameObject);
  }


}
