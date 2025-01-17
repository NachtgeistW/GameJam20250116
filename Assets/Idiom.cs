﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts;
using Plutono.Util;
using UnityEngine;
using static Assets.Scripts.GameEvent;
using Random = System.Random;

namespace Assets
{
    public class ChinesePinyin
    {
        // 简化版拼音对照表，实际使用时可以扩充
        private static readonly Dictionary<char, string> pinyinDict = new()
        {
            { '一', "yi" }, { '心', "xin" }, { '同', "tong" }, { '合', "he" }, { '大', "da" },
            { '小', "xiao" }, { '异', "yi" }, { '意', "yi" }, { '德', "de" }, { '得', "de" },
            { '死', "si" }, { '生', "sheng" }, { '善', "shan" }, { '山', "shan" }, { '水', "shui" },
            { '火', "huo" }, { '为', "wei" }, { '无', "wu" }, { '情', "qing" }, { '清', "qing" },
            { '静', "jing" }, { '万', "wan" }, { '事', "shi" }, { '世', "shi" }, { '背', "bei" },
            { '北', "bei" }, { '南', "nan" }, { '男', "nan" }, { '女', "nv" }, { '如', "ru" },
            { '道', "dao" }, { '到', "dao" }, { '人', "ren" }, { '仁', "ren" }, { '表', "biao" },
            // 这里可以继续添加更多拼音对照
        };

        public static string GetPinyin(char character)
        {
            return pinyinDict.GetValueOrDefault(character, "");
        }

        public static bool IsSamePinyin(char char1, char char2)
        {
            var pinyin1 = GetPinyin(char1);
            var pinyin2 = GetPinyin(char2);
            return !string.IsNullOrEmpty(pinyin1) && !string.IsNullOrEmpty(pinyin2) && pinyin1 == pinyin2;
        }
    }

    public class Idiom
    {
        public string Word { get; set; }
        public List<char> Characters { get; set; }

        public Idiom(string word)
        {
            Word = word;
            Characters = word.ToCharArray().ToList();
        }
    }

    public class IdiomGame
    {
        public List<Idiom> idioms;
        private Random random;
        public List<string> usedIdioms;
        public char firstCharacter;
        public string firstCharacterPinyin;
        public List<string> WordList;

        private int curCharIndex;

        private char curFirstCharacter;
        private char curSecondCharacter;
        private char curThirdCharacter;
        private char curFourthCharacter;

        public IdiomGame(string filePath)
        {
            idioms = new List<Idiom>();
            usedIdioms = new List<string>();
            random = new Random();
            LoadIdioms(filePath);
            Init();

            void Init()
            {
                var wordList = idioms
                    .Select(i => i.Characters[0].ToString())
                    .ToList();
                var firstCharIndex = random.Next(0, wordList.Count);
                WordList = new List<string> { wordList[firstCharIndex] };
                wordList.RemoveAt(firstCharIndex);
                WordList.Add(wordList[random.Next(0, wordList.Count)]);

                curCharIndex = 1;
            }
        }

        private void LoadIdioms(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    idioms.Add(new Idiom(line.Trim()));
                }
            }
        }

        public List<char> GetPossibleSecondCharacters(char firstChar)
        {
            var possibleChars = idioms
                .Where(i => i.Characters[0] == firstChar && !usedIdioms.Contains(i.Word))
                .Select(i => i.Characters[1])
                .Distinct()
                .ToList();

            return possibleChars;
        }

        public List<char> GetPossibleThirdCharacters(char firstChar, char secondChar)
        {
            var possibleChars = idioms
                .Where(i => i.Characters[0] == firstChar &&
                            i.Characters[1] == secondChar &&
                            !usedIdioms.Contains(i.Word))
                .Select(i => i.Characters[2])
                .Distinct()
                .ToList();

            return possibleChars;
        }

        public List<char> GetPossibleFourthCharacters(char firstChar, char secondChar, char thirdChar)
        {
            var possibleChars = idioms
                .Where(i => i.Characters[0] == firstChar &&
                            i.Characters[1] == secondChar &&
                            i.Characters[2] == thirdChar &&
                            !usedIdioms.Contains(i.Word))
                .Select(i => i.Characters[3])
                .Distinct()
                .ToList();

            return possibleChars;
        }

        public List<char> GetNextPossibleFirstCharacters(char lastChar)
        {
            var lastCharPinyin = ChinesePinyin.GetPinyin(lastChar);

            var possibleChars = idioms
                .Where(i => !usedIdioms.Contains(i.Word))
                .Where(i => ChinesePinyin.GetPinyin(i.Characters[0]) == lastCharPinyin)
                .Select(i => i.Characters[0])
                .Distinct()
                .ToList();

            return possibleChars;
        }

        public bool ValidateIdiom(string word)
        {
            return idioms.Any(i => i.Word == word && !usedIdioms.Contains(word));
        }

        bool isFirstTune = true;
        bool gameOver = false;

        public void PlayGame(EatFoodEvent evt)
        {
            if (gameOver)
            {
                EventCenter.Broadcast(new GameOverEvent { isSucceed = true });
            }

            if (isFirstTune)
            {
                firstCharacter = evt.AteFoodWord.ToCharArray()[0];
                isFirstTune = false;
            }

            switch (curCharIndex)
            {
                case 1:
                    curFirstCharacter = evt.AteFoodWord.ToCharArray()[0];
                    var secondChars = GetPossibleSecondCharacters(curFirstCharacter);
                    if (secondChars.Count == 0)
                    {
                        EventCenter.Broadcast(new GameOverEvent { isSucceed = false });
                        break;
                    }

                    WordList = secondChars.Select(c => c.ToString()).ToList();
                    EventCenter.Broadcast(new UpdateWordlistEvent { WordList = WordList });
                    curCharIndex++;
                    break;
                case 2:
                    curSecondCharacter = evt.AteFoodWord.ToCharArray()[0];
                    var thirdChars = GetPossibleThirdCharacters(curFirstCharacter, curSecondCharacter);
                    if (thirdChars.Count == 0)
                    {
                        EventCenter.Broadcast(new GameOverEvent { isSucceed = false });
                        break;
                    }

                    WordList = thirdChars.Select(c => c.ToString()).ToList();
                    EventCenter.Broadcast(new UpdateWordlistEvent { WordList = WordList });
                    curCharIndex++;
                    break;
                case 3:
                    curThirdCharacter = evt.AteFoodWord.ToCharArray()[0];
                    var fourthChars =
                        GetPossibleFourthCharacters(curFirstCharacter, curSecondCharacter, curThirdCharacter);
                    if (fourthChars.Count == 0)
                    {
                        EventCenter.Broadcast(new GameOverEvent { isSucceed = false });
                        break;
                    }

                    WordList = fourthChars.Select(c => c.ToString()).ToList();
                    EventCenter.Broadcast(new UpdateWordlistEvent { WordList = WordList });
                    curCharIndex = 4;
                    break;
                case 4:
                    curFourthCharacter = evt.AteFoodWord.ToCharArray()[0];
                    var currentWord = $"{curFirstCharacter}{curSecondCharacter}{curThirdCharacter}{curFourthCharacter}";
                    if (ValidateIdiom(currentWord))
                    {
                        usedIdioms.Add(currentWord);
                        Debug.Log($"当前成语：{currentWord}");

                        // 检查是否完成循环（通过拼音匹配）
                        var lastCharPinyin = ChinesePinyin.GetPinyin(curFourthCharacter);
                        if (lastCharPinyin == firstCharacterPinyin)
                        {
                            Debug.Log("恭喜！成功完成首尾相接的成语接龙！");
                            Debug.Log("使用的成语：");
                            for (var i = 0; i < usedIdioms.Count; i++)
                            {
                                Debug.Log($"{i + 1}. {usedIdioms[i]}");
                            }

                            gameOver = true;
                        }
                        else
                        {
                            // 继续游戏
                            curFirstCharacter = curFourthCharacter;
                            Debug.Log($"\n下一个成语开始字：{curFirstCharacter}");
                            var firstChars = GetNextPossibleFirstCharacters(curFirstCharacter);
                            WordList = firstChars.Select(c => c.ToString()).ToList();
                            EventCenter.Broadcast(new UpdateWordlistEvent { WordList = WordList });
                        }

                        curCharIndex = 1;
                    }
                    else
                    {

                    }
                    break;
            }
        }

    }
}