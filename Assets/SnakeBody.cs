using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 使用SetWord（string）设置外部传入的字符串
/// 该脚本用于显示蛇身上的文字
/// 脚本创建时候就会确定蛇身上的文字
/// </summary>

public class SnakeBody : MonoBehaviour
{
    public TextMesh textMesh;
    public string word;

    private void Start()
    {
        //textMesh=getComponent<TextMesh>();
        ChangeText();
    }
    public void SetWord(string word)
    {
        this.word = word;
    }
    public void ChangeText()
    {
        textMesh.text = word;
    }
    
}
