using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<GameObject> SpawnWords()
    {
        List<GameObject> wordList = new List<GameObject>();
        foreach (string word in words)
        {
            tempWordPosition = new Vector2(
                Mathf.Round(Random.Range(-5, 5)) * gridSize,
                Mathf.Round(Random.Range(-5, 5)) * gridSize
            );
            wordList.Add(Instantiate(wordPrefab, tempWordPosition, Quaternion.identity));
            //newWord.GetComponent<TextMesh>().word = word;
        }
        return wordList;
    }
    void DestoryAllWordsThisTurn(){
        //广播消息，通知所有Word对象销毁自己
        //GameObject[] allWords = GameObject.FindGameObjectsWithTag("Word");
    }
    
}
