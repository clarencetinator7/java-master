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
      // Get Player Rigid Body
      Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
      if (playerRb.velocity.y > 0.01f)
      {
        // Damage this enemy
      }
      else
      {
        // Damage player
        playerController.Hurt(gameObject);
      }


    }
  }

}
