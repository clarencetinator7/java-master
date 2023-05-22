using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      Player player = collision.gameObject.GetComponent<Player>();
      // Kill Player
      if (player != null && !player.isDying)
      {
        Debug.Log("Player hit spikes");
        player.isDying = true;
        player.GetComponent<Player>().Die();
      }
    }
  }

}
