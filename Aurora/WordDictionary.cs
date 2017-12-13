using Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data
{
    public sealed class WordDictionary
    {
        private static WordDictionary instance = new WordDictionary();
        private Dictionary<string, Word> dict;
        private WordDictionary()
        {
            dict = new Dictionary<string, Word>();
        }

        public int add(string sentenceName)
        {
            ArrayList wordArray = spaceSeparate(sentenceName);
            foreach (string word in wordArray)
            {
                try
                {
                    dict.Add(word, new Word(word));
                }
                catch (System.ArgumentException e)
                {
                    Console.WriteLine(e);
                    dict[word].AppearanceFrequency += 1;
                }

            }
            Console.WriteLine(ToString());
            return dict.Count;
        }

        public ArrayList spaceSeparate(string sentenceName)
        {
            try
            {
                ArrayList arr = new ArrayList();
                string[] wordArray = sentenceName.Split(' ');
                foreach (string word in wordArray)
                {
                    arr.Add(word);

                }
                return arr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public override string ToString()
        {
            string str = "";
            foreach (Word word in dict.Values)
            {
                str += word.ToString() + "\n";
            }
            return str;
        }

        public static WordDictionary getInstance()
        {
            return instance;
        }
    }
}