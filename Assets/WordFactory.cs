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
    public SpriteRenderer wordRenderer;
    private float gridSize;
    private Vector2 tempWordPosition;

    static List<string> currentWordList;//

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        gridSize = wordRenderer.bounds.size.x;

        currentWordList = new List<string>();
        currentWordList.Add("为");
        currentWordList.Add("你");
        currentWordList.Add("祝");
        currentWordList.Add("幸");
        currentWordList.Add("福");
        currentWordList.Add("康");
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

    private IEnumerator SpawnWordInMap()
    {
        if (currentWordList.Count > 0)
        {
            SnakeGame.currentFood = SpawnWords(currentWordList);
        }
        yield return WordBeDestroyed();

    }

    private static IEnumerator WordBeDestroyed()
    {
        //接收广播本次销毁开始
        yield return null;
    }

    private void DestroyAllWordsThisTurn()
    {
        //广播实现函数

    }
}
