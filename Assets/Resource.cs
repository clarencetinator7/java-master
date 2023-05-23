using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
  public static Resource instance;
  // Start is called before the first frame update
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
      DontDestroyOnLoad(gameObject);
    }
  }

}
