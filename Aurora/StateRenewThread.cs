using System;
using System.Threading.Tasks;

namespace Utilities
{
    class StateRenewThread
    {
        async public void Run()
        {
            while (true)
            {
                Console.WriteLine("Thread");
                await Task.Delay(500);
            }
        }
    }
}