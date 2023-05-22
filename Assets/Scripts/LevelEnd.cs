using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

  bool isGoingToNextLevel = false;
  [SerializeField] float levelEndDelay = 2f;
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
        UIManager.instance.showNotificationPanel("Looks like there are still uncleared data stations.", 2f);
      }

      // FOR TESTING PURPOSES
      // isGoingToNextLevel = true;
      // StartCoroutine(levelEnd());

    }
  }

  IEnumerator levelEnd()
  {
    // TODO: SHOW LEVEL END UI AND SCORE
    UIManager.instance.showNotificationPanel("Level Cleared! Please wait....", 3f);
    yield return new WaitForSeconds(levelEndDelay);
    UIManager.instance.showGameOverPanel();
  }
}
