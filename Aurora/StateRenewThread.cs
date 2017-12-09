using CheckState;
using System;
using System.Threading.Tasks;

namespace Utilities
{
    class StateRenewThread
    {
        private static CheckClipboard checkClipboard = CheckClipboard.getInstacnce();
        async public void Run()
        {
            while (true)
            {
                if (checkClipboard.isChangeClipboard())
                {
                    checkClipboard.matchClipboard();
                    Console.WriteLine("변했다.");
                }
                //Console.WriteLine("Thread");
                await Task.Delay(500);
            }
        }
    }
}