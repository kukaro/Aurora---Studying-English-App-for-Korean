using dto;
using System;
using System.Collections;

namespace Dto
{
    class Word
    {
        private string word;
        private int appearanceFrequency;
        private int searchFrequency;
        private ArrayList meanList;

        public int AppearanceFrequency
        {
            get { return appearanceFrequency; }
        }

        public int SearchFrequency
        {
            get { return searchFrequency; }
        }

        public Word(string word)
        {
            this.word = word;
            appearanceFrequency = 1;
            searchFrequency = 0;
            meanList = new ArrayList();
        }

        public void increaseSearchfrequency()
        {
            searchFrequency++;
        }

        public void increaseAppearancefrequency()
        {
            appearanceFrequency++;
        }

        public bool addMean(Mean m)
        {
            try
            {
                meanList.Add(m);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}