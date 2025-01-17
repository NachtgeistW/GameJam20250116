using Plutono.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
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
        EventCenter.AddListener<GameEvent.EatFoodEvent>(OnEatFoodEvent);
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener<GameEvent.EatFoodEvent>(OnEatFoodEvent);
    }

    private void Start()
    {
        gridSize = wordRenderer.bounds.size.x;

        var firstWordList =
        GameManager.Instance.Game.idioms
            .Select(i => i.Characters[0].ToString())
            .ToList();

        currentWordList = new List<string>();
        var random = new System.Random();
        var firstCharIndex = random.Next(0, firstWordList.Count);
        currentWordList.Add(firstWordList[firstCharIndex]);
        firstWordList.RemoveAt(firstCharIndex);
        currentWordList.Add(firstWordList[random.Next(0, firstWordList.Count)]);

        SpawnWordInMap();
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

    private void OnEatFoodEvent(GameEvent.EatFoodEvent _)
    {
        SpawnWordInMap();
    }

    private void SpawnWordInMap()
    {
        Snake.currentFood = SpawnWords(currentWordList);
    }

}
