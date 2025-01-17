using Assets;
using Assets.Scripts;
using Plutono.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public IdiomGame Game { get; private set; }
    private void OnEnable()
    {
        EventCenter.AddListener<GameEvent.EatFoodEvent>(Game.PlayGame);
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener<GameEvent.EatFoodEvent>(Game.PlayGame);
    }

    protected override void Awake()
    {
        base.Awake();

        const string filePath = "´Ê¿â.txt";
        Game = new IdiomGame(filePath);
    }
}
