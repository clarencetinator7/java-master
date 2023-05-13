using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  void Update()
  {
    // LOG POSITION TO WORLD SPACE
    // Debug.Log(transform.TransformPoint(Vector3.zero));
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      Debug.Log("Player hit");

      // Get Player Controller
      PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
      if (playerController != null)
      {
        playerController.Hurt(gameObject);
      }


    }
  }

}
