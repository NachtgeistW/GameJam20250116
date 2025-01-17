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

    static List<string> currentWordList;//

    void OnEnable() { }
    void OnDisable() { }
    void Start()
    {
        currentWordList = new List<string>();
        StartCoroutine(SpawnWordInMap());
    }
    public GameObject SpawnSingleWord(string word)
    {
        tempWordPosition = new Vector2(
            Mathf.Round(Random.Range(-5, 5)) * gridSize,
            Mathf.Round(Random.Range(-5, 5)) * gridSize
        );
        return Instantiate(wordPrefab, tempWordPosition, Quaternion.identity);
    }
    public List<Transform> SpawnWords(List<string> words)
    {
        List<Transform> wordList = new List<Transform>();
        foreach (string w in words)
        {
            tempWordPosition = new Vector2(
                Mathf.Round(Random.Range(-5, 5)) * gridSize,
                Mathf.Round(Random.Range(-5, 5)) * gridSize
            );
            WordAction tempWordA = Instantiate(wordPrefab, tempWordPosition, Quaternion.identity).GetComponent<WordAction>();
            tempWordA.word = w;
            wordList.Add(tempWordA.gameObject.transform);
            
            //newWord.GetComponent<TextMesh>().word = word;
        }
        return wordList;
    }
   
    IEnumerator SpawnWordInMap()
    {
        if (currentWordList.Count > 0)
        {
            SpawnWords(currentWordList);
        }
        yield return WordBeDestroyed();

    }
    IEnumerator WordBeDestroyed()
    {
        //接收广播本次销毁开始
        yield return null;
    }
    void DestoryAllWordsThisTurn()
    {
        //广播实现函数

    }
}
