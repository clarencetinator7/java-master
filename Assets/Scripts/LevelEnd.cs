using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

  bool isGoingToNextLevel = false;
  [SerializeField] float levelEndDelay = 5f;
  [SerializeField] string nextLevelName = "Main Menu";
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


      // FOR TESTING PURPOSES
      // isGoingToNextLevel = true;
      // StartCoroutine(levelEnd());

    }
  }

  IEnumerator levelEnd()
  {
    // TODO: SHOW LEVEL END UI AND SCORE
    yield return new WaitForSeconds(levelEndDelay);
    GameManager.instance.levelEnd(nextLevelName);
  }
}
