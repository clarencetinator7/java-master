using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DCXX", menuName = "Scriptable Objects/Create a new datachip", order = 1)]
public class DatachipsSO : ScriptableObject
{
  public string datachipName;

  [TextArea(5, 10)]
  public string datachipText;
  public QuestionSO[] relatedQuestion;
}
