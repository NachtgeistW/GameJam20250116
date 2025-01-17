using System.Collections.Generic;
using Plutono.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameEvent
    {
        public struct EatFoodEvent : IEvent { }

        public struct GenerateFirstCharacterEvent : IEvent
        {
            public List<string> charList;
        }
        public struct GetSecondCharacterEvent : IEvent { }
        public struct GetThirdCharacterEvent : IEvent { }
        public struct GetFourthCharacterEvent : IEvent { }
    }
}