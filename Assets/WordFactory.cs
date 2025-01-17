using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这个类型给生成地图上面的可食用文字
/// 生成单个字符 SpawnSingleWord(string word)
/// 生成一组字符 SpawnWords(string word)
/// </summary>
public class WordFactory : MonoBehaviour
{
    public GameObject wordPrefab;
    private float gridSize;
    public Vector2 tempWordPosition;
    public string[] words;
    public string singleWord;

    public GameObject SpawnSingleWord(string word)
    {
        tempWordPosition = new Vector2(
            Mathf.Round(Random.Range(-5, 5)) * gridSize,
            Mathf.Round(Random.Range(-5, 5)) * gridSize
        );
        return Instantiate(wordPrefab, tempWordPosition, Quaternion.identity);
    }
    public List<Transform> SpawnWords(string words)
    {
        List<Transform> wordList = new List<Transform>();
        for (int i = 0; i < words.Length; i++)
        {
            tempWordPosition = new Vector2(
                Mathf.Round(Random.Range(-5, 5)) * gridSize,
                Mathf.Round(Random.Range(-5, 5)) * gridSize
            );
            WordAction tempWordA = Instantiate(wordPrefab, tempWordPosition, Quaternion.identity).GetComponent<WordAction>();
            wordList.Add(tempWordA.gameObject.transform);
            
            //newWord.GetComponent<TextMesh>().word = word;
        }
        return wordList;
    }
    void DestoryAllWordsThisTurn(){
        //广播消息，通知所有Word对象销毁自己
        //GameObject[] allWords = GameObject.FindGameObjectsWithTag("Word");
    }
    
}
