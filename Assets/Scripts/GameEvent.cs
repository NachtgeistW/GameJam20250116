using System.Collections.Generic;
using Plutono.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameEvent
    {
        public struct EatFoodEvent : IEvent
        {
            public string AteFoodWord;
        }

        public struct UpdateWordlistEvent : IEvent
        {
            public List<string> WordList;
        }

        public struct GameOverEvent : IEvent
        {
            public bool isSucceed;
        }
    }
}