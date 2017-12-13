using System;
using System.Windows.Forms;

namespace CheckState
{
    class CheckClipboard
    {
        private static CheckClipboard instance = new CheckClipboard();
        private string presClipbaord = Clipboard.GetText();
        private string newClipboard = Clipboard.GetText();

        public string StringClipboard
        {
            get { return newClipboard; }
        }

        private CheckClipboard()
        {
            /*pass*/
        }

        public bool setNewClipboard()
        {
            try
            {
                newClipboard = Clipboard.GetText();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

        public bool isChangeClipboard()
        {
            if (!presClipbaord.Equals(newClipboard))
            {
                return true;
            }
            return false;
        }

        public void matchClipboard()
        {
            presClipbaord = newClipboard;
        }

        public string getState()
        {
            return "presClipboard : " + presClipbaord + ", newClipboard :" + newClipboard;
        }

        public static CheckClipboard getInstance()
        {
            return instance;
        }
    }
}