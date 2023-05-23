using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GameManager.instance.setSpawnPoint(gameObject.transform);

    GameObject playerInstance = GameObject.FindGameObjectWithTag("Player");
    // Check if there is a player existing
    if (playerInstance != null)
    {
      playerInstance.transform.position = gameObject.transform.position;
      GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerInstance.transform;
      // Resource.instance.setCameraFollow(playerInstance);
    }
    else
    {
      GameManager.instance.SpawnPlayer();
    }
  }

}
