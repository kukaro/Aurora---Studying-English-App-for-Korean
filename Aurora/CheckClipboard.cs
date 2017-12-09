using System;
using System.Windows.Forms;

namespace CheckState
{
    class CheckClipboard
    {
        private static CheckClipboard instance = new CheckClipboard();
        private string presClipbaord = "";
        private string newClipboard = "";

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

        public static CheckClipboard getInstacnce()
        {
            return instance;
        }
    }
}