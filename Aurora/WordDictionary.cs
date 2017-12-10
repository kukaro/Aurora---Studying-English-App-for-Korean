using Dto;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data{
    public class WordDictionary
    {
        private Dictionary<string, Word> dict;
        public WordDictionary()
        {
            dict = new Dictionary<string, Word>();
        }

        public int add()
        {
            return dict.Count;
        }
    }
}