using Dto;
using System;
using System.Collections;

namespace Dto
{
    class Word
    {
        private string word;
        /*
         * 내가 외부에서 등록한 횟수
         */
        private int appearanceFrequency;
        /*
         * 내가 어플리케이션 내부 찾기 기능을 사용해서 찾아본 횟수
         */
        private int searchFrequency;
        private ArrayList meanList;

        public int AppearanceFrequency
        {
            get { return appearanceFrequency; }
            set { appearanceFrequency = value; }
        }

        public int SearchFrequency
        {
            get { return searchFrequency; }
        }

        public ArrayList MeanList
        {
            get { return meanList; }
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

        public override string ToString()
        {
            return "word : " + word + ", appearanceFrequency : " + appearanceFrequency + ", searchFrequency : " + searchFrequency;
        }
    }
}