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
            set { searchFrequency = value; }
        }

        public int SearchFrequency
        {
            get { return searchFrequency; }
            set { searchFrequency = value; }
        }
}