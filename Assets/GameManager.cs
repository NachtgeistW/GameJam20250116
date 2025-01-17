using Assets;
using Assets.Scripts;
using Plutono.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public IdiomGame Game { get; private set; }
    //private void OnEnable()
    //{
    //    EventCenter.AddListener<GameEvent.GetFirstCharacterEvent>();
    //    EventCenter.AddListener<GameEvent.GetSecondCharacterEvent>();
    //    EventCenter.AddListener<GameEvent.GetThirdCharacterEvent>();
    //    EventCenter.AddListener<GameEvent.GetFourthCharacterEvent>();
    //}

    //private void OnDisable()
    //{
    //    EventCenter.RemoveListener<GameEvent.GetFirstCharacterEvent>();
    //    EventCenter.RemoveListener<GameEvent.GetSecondCharacterEvent>();
    //    EventCenter.RemoveListener<GameEvent.GetThirdCharacterEvent>();
    //    EventCenter.RemoveListener<GameEvent.GetFourthCharacterEvent>();
    //}

    protected override void Awake()
    {
        base.Awake();

        const string filePath = "´Ê¿â.txt";
        Game = new IdiomGame(filePath);
    }
}
