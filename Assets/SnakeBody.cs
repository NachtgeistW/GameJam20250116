using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public TextMesh textMesh;
    public string word;

    private void Start()
    {
        //textMesh=getComponent<TextMesh>();
        ChandeText();
    }
    public void SetWord(string word)
    {
        this.word = word;
    }
    public void ChandeText()
    {
        textMesh.text = word;
    }
    
}
