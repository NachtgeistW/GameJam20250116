using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordAction : MonoBehaviour
{
    public TextMesh textMesh;
    public string word;
    // Start is called before the first frame update
    void Start()
    {
        ChangWord();
    }
    public void SetWord(string word)
    {
        this.word = word;
    }
    public void ChangWord()
    {
        textMesh.text = word;
    }
}
