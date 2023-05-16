using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Create a new question", order = 1)]
public class QuestionSO : ScriptableObject
{

  public string questionName;
  public string questionText;
  public string[] answerChoices;
  public int correctAnswerIndex;

}
