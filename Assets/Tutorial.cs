using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
  public GameObject tutorialPanel;
  public TextMeshProUGUI tutorialText;
  [TextArea(3, 10)]
  public string[] lines;
  public float textSpeed = 0.5f;
  private int currentLine = 0;
  public bool isTriggered = false;
  bool isOngoing = false;

  [Header("Audio")]
  [SerializeField] AudioClip typingSound;

  void Start()
  {
    // tutorialText.text = string.Empty;
  }

  void Update()
  {
    // Next line on touch screen
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isOngoing)
    {
      if (tutorialText.text == lines[currentLine])
      {
        NextLine();
      }
      else
      {
        StopAllCoroutines();
        tutorialText.text = lines[currentLine];
      }
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && !isTriggered)
    {
      tutorialText.text = string.Empty;
      isOngoing = true;
      tutorialPanel.SetActive(true);
      UIManager.instance.hideControlPanel();
      isTriggered = true;
      StartDialogue();
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player") && isOngoing)
    {
      tutorialPanel.SetActive(false);
      UIManager.instance.showControlPanel();
      isOngoing = false;
      isTriggered = false;
    }
  }

  void StartDialogue()
  {
    currentLine = 0;
    StartCoroutine(TypeLine());
  }

  IEnumerator TypeLine()
  {
    foreach (char c in lines[currentLine].ToCharArray())
    {
      tutorialText.text += c;
      SoundManager.instance.playSound(typingSound);
      yield return new WaitForSeconds(textSpeed);
    }
  }

  void NextLine()
  {
    if (currentLine < lines.Length - 1)
    {
      currentLine++;
      tutorialText.text = string.Empty;
      StartCoroutine(TypeLine());
    }
    else
    {
      isOngoing = false;
      isTriggered = true;
      tutorialPanel.SetActive(false);
      UIManager.instance.showControlPanel();
    }
  }


}

