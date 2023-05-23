using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;
public class Resource : MonoBehaviour
{
  public static Resource instance;

  // Get cinemachine virtual camera
  public CinemachineVirtualCamera virtualCamera;
  private float shakeTimer;
  private float shakeTimerTotal;
  private float startingIntensity;

  void Awake()
  {
    virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
  }

  void Start()
  {
    MakeSingleton();
    // if (!GameManager.instance.isHome && GameManager.instance.activePlayerInstance != null)
    // {
    //   setCameraFollow(GameManager.instance.activePlayerInstance);
    // }
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
      // DontDestroyOnLoad(gameObject);
    }
  }

  public void ShakeCamera(float intensity, float time)
  {
    Debug.Log("Shake Camera");
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    startingIntensity = intensity;
    shakeTimer = time;
    shakeTimerTotal = time;

  }

  void Update()
  {
    if (shakeTimer > 0)
    {
      shakeTimer -= Time.deltaTime;
      if (shakeTimer <= 0f)
      {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
      }
    }

    // if (Keyboard.current.spaceKey.wasPressedThisFrame)
    // {
    //   ShakeCamera(1f, 0.2f);
    // }
  }

  public void setCameraFollow(GameObject target)
  {
    virtualCamera.Follow = target.transform;
  }

}
