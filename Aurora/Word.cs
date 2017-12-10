namespace Dto
{
    class Word
    {
        private string word;
        private int appearanceFrequency;
        private int searchFrequency;

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
        }

        public void increaseSearchfrequency()
        {
            searchFrequency++;
        }

        public void increaseAppearancefrequency()
        {
            appearanceFrequency++;
        }
    }
}