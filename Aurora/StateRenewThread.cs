using CheckState;
using Data;
using System;
using System.Threading.Tasks;

namespace Utilities
{
    class StateRenewThread
    {
        private static CheckClipboard checkClipboard = CheckClipboard.getInstance();
        private static WordDictionary wordDictionary = WordDictionary.getInstance();

        async public void Run()
        {
            while (true)
            {
                if (checkClipboard.isChangeClipboard())
                {
                    //Console.WriteLine(checkClipboard.getState());
                    //Console.WriteLine(checkClipboard.StringClipboard);
                    checkClipboard.matchClipboard();
                    wordDictionary.add(KNRSeparator.sentenceSeparate(checkClipboard.StringClipboard));
                    Console.WriteLine(wordDictionary.ToString());
                    Console.WriteLine("변했다.");
                }
                //Console.WriteLine("Thread");
                await Task.Delay(500);
            }
        }
    }
}