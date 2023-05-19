using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

  bool isGoingToNextLevel = false;
  [SerializeField] float levelEndDelay = 5f;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && !isGoingToNextLevel)
    {

      float totalDataStation = GameManager.instance.totalDataStation;
      float totalDataStationAnswered = GameManager.instance.totalDataStationAnswered;

      if (totalDataStation == totalDataStationAnswered)
      {
        if (!isGoingToNextLevel)
        {
          isGoingToNextLevel = true;
          StartCoroutine(levelEnd());
        }
      }
      else
      {
        Debug.Log("Not all data stations answered");
      }

    }
  }

  IEnumerator levelEnd()
  {
    yield return new WaitForSeconds(levelEndDelay);
    GameManager.instance.levelEnd();
  }
}
