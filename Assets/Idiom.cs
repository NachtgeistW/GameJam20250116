using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public IdiomGame(string filePath)
        {
            idioms = new List<Idiom>();
            usedIdioms = new List<string>();
            random = new Random();
            LoadIdioms(filePath);
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
    }
}