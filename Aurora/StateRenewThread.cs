using CheckState;
using System;
using System.Threading.Tasks;

namespace Utilities
{
    class StateRenewThread
    {
        private static CheckClipboard checkClipboard = CheckClipboard.getInstance();
        async public void Run()
        {
            while (true)
            {
                if (checkClipboard.isChangeClipboard())
                {
                    Console.WriteLine(checkClipboard.getState());
                    checkClipboard.matchClipboard();
                    Console.WriteLine("변했다.");
                }
                //Console.WriteLine("Thread");
                await Task.Delay(500);
            }
        }
    }
}