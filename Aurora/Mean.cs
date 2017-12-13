namespace Dto
{
    class Mean
    {
        private string wordClass;
        private string data;

        private string WordClass
        {
            get { return wordClass; }
            set { WordClass = value; }
        }

        private string Data
        {
            get { return data; }
            set { data = value; }
        }

        public Mean(string wordClass, string data)
        {
            this.wordClass = wordClass;
            this.data = data;
        }

        override public string ToString()
        {
            return "WordClass : " + wordClass + ", Data : " + data;
        }
    }
}