using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
  public static SoundManager instance;

  [SerializeField] private AudioSource _musicSource, _sfxSource;

  [Header("Sounds Options")]
  [SerializeField] private Button soundToggleBtn;
  [SerializeField] private Sprite soundOn, soundOff;
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

  public void toggleSounds()
  {
    if (!_musicSource.mute && !_sfxSource.mute)
    {
      _sfxSource.mute = true;
      _musicSource.mute = true;
      // change sprite to sound off
      soundToggleBtn.GetComponent<Image>().sprite = soundOff;
      // change color to #FF645D
      soundToggleBtn.GetComponent<Image>().color = new Color32(255, 100, 93, 255);
    }
    else
    {
      _sfxSource.mute = false;
      _musicSource.mute = false;
      // change sprite to sound on
      soundToggleBtn.GetComponent<Image>().sprite = soundOn;
      // change color to #FF00FF
      soundToggleBtn.GetComponent<Image>().color = new Color32(255, 0, 255, 255);

    }
  }
}
