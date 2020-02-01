using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Word", menuName ="Data/ Word")]
public class WordData : ScriptableObject
{
    public List<string> wordList = new List<string>();
}
