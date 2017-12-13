using Dto;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data{
    public sealed class WordDictionary
    {
        private static WordDictionary instance = new WordDictionary();
        private Dictionary<string, Word> dict;
        private WordDictionary()
        {
            dict = new Dictionary<string, Word>();
        }

        public int add(string wordName)
        {
            dict.Add(wordName, new Word(wordName));
            System.Console.WriteLine("**********");
            System.Console.WriteLine(dict[wordName]);
            return dict.Count;
        }

        public static WordDictionary getInstance()
        {
            return instance;
        }
    }
}