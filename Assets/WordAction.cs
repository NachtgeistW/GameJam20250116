using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordAction : MonoBehaviour
{
    public TextMesh textMesh;
    public string word;
    // Start is called before the first frame update

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
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

    private void DestroyAllWordsThisTurn()
    {
        //广播实现函数

    }
}
