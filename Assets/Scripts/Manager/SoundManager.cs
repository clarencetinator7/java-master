using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager instance;

  [SerializeField] private AudioSource _musicSource, _sfxSource;

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

  public void playSound(AudioClip clip)
  {
    _sfxSource.PlayOneShot(clip);
  }
}
