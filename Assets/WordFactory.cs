using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordFactory : MonoBehaviour
{
    public GameObject wordPrefab;
    public float gridSize;
    public Vector2 tempWordPosition;
    public string[] words;
    public string singleWord;

    void SpawnSingleWord(string word)
    {
        tempWordPosition = new Vector2(
            Mathf.Round(Random.Range(-5, 5)) * gridSize,
            Mathf.Round(Random.Range(-5, 5)) * gridSize
        );
        Instantiate(wordPrefab, tempWordPosition, Quaternion.identity);
    }
    void SpawnWords()
    {
        foreach (string word in words)
        {
            tempWordPosition = new Vector2(
                Mathf.Round(Random.Range(-5, 5)) * gridSize,
                Mathf.Round(Random.Range(-5, 5)) * gridSize
            );
            GameObject newWord = Instantiate(wordPrefab, tempWordPosition, Quaternion.identity);
            //newWord.GetComponent<TextMesh>().word = word;
        }
    }
    void DestoryAllWordsThisTurn(){
        //广播消息，通知所有Word对象销毁自己
        //GameObject[] allWords = GameObject.FindGameObjectsWithTag("Word");
    }
    
}
