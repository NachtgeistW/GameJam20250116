using Assets.Scripts;
using Plutono.Util;
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
        EventCenter.AddListener<GameEvent.EatFoodEvent>(DestroyAllWordsThisTurn);
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener<GameEvent.EatFoodEvent>(DestroyAllWordsThisTurn);
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

    private void DestroyAllWordsThisTurn(GameEvent.EatFoodEvent _)
    {
        Destroy(gameObject);
    }
}
