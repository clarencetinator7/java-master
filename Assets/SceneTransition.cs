using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class SceneTransition : MonoBehaviour
{
  public static SceneTransition instance;
  public TransitionManager transitionManager;


  void Start()
  {
    MakeSingleton();
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

  public void LoadScene(string _sceneName)
  {
    transitionManager.LoadScene(_sceneName, "DoubleWipe", 0f);
  }


}
