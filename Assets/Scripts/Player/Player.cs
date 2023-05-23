using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  public bool isDying = false;

  [Header("Particles")]
  [SerializeField] GameObject deathParticle;

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
    GameObject particle = Instantiate(deathParticle, transform.position, Quaternion.identity);
    Destroy(particle, 2f);
    GameManager.instance.RespawnPlayer();
    Destroy(gameObject);
  }


}
