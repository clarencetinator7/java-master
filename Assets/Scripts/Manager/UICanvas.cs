using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
  public static UICanvas instance; // Start is called before the first frame update
  void Awake()
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
      DontDestroyOnLoad(gameObject);
    }
  }

}
