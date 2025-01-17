using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordAction : MonoBehaviour
{
    public TextMesh textMesh;
    public string word;
    // Start is called before the first frame update

    void OnEnable() { }
    void OnDisable() { }
    void Start()
    {
        ChangWord();
    }
    public string GetWord()
    {
        return word;
    }
    public void SetWord(string word)
    {
        this.word = word;
    }
    public void ChangWord()
    {
        textMesh.text = word;
    }

    void DestoryAllWordsThisTurn()
    {
        //广播实现函数

    }
}
