using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ʹ��SetWord��string�������ⲿ������ַ���
/// �ýű�������ʾ�����ϵ�����
/// �ű�����ʱ��ͻ�ȷ�������ϵ�����
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
