using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utilities
{
    class GlobalMouseHook
    {
        #region Constant, Structure and Delegate Definitions
        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int MouseHookProc(int code, int wParam, ref MouseHookStruct lParam);


        public struct MouseHookStruct
        {
            public int posX;
            public int posY;
        }

        const int WH_GETMESSAGE = 0x03;
        const int WH_KEYBOARD_LL = 0x0D;
        const int WH_CBT = 0x05;
        const int WH_JOURNALRECORD = 0x00;
        const int WH_SYSMSGFILTER = 0x06;
        const int WH_MSGFILTER = -1;
        const int WH_JOURNALPLAYBACK = 0x01;
        const int WH_FOREGROUNDIDLE = 0x0b;
        const int WH_CALLWNDPROCRET = 0x0c;
        const int WH_MOUSE_LL = 0x0E;

        const int WM_LBUTTONDOWN = 0x201;
        const int WM_LBUTTONUP = 0x202;
        #endregion

        #region static Variables
        private static MouseHookProc mhook;
        #endregion

        #region Instance Variables
        /// <summary>
        /// The collections of keys to watch for
        /// </summary>
        public List<Keys> hookedKeys = new List<Keys>();

        /// <summary>
        /// Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        IntPtr hhook = IntPtr.Zero;
        #endregion

        #region Constructors and Destructors
        public GlobalMouseHook()
        {
            hook();
            hookedKeys.Add(Keys.LButton);
        }

        ~GlobalMouseHook()
        {
            unhook();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Installs the global hook
        /// </summary>
        public void hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            mhook = hookProc;
            hhook = SetWindowsHookEx(WH_MOUSE_LL, mhook, hInstance, 0);
        }

        /// <summary>
        /// Uninstalls the global hook
        /// </summary>
        public void unhook()
        {
            UnhookWindowsHookEx(hhook);
        }

        /// <summary>
        /// The callback for the Mouse hook
        /// </summary>
        /// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anyting</param>
        /// <param name="wParam">The event type</param>
        /// <param name="lParam">The mouse event information</param>
        /// <returns></returns>
        public int hookProc(int code, int wParam, ref MouseHookStruct lParam)
        {

            int posX = lParam.posX;
            int posY = lParam.posY;
            switch (wParam)
            {
                case WM_LBUTTONUP:
                    //Console.WriteLine("UP : " + posX + ":" + posY + ":");
                    //Console.WriteLine(Clipboard.GetText());
                    break;
                case WM_LBUTTONDOWN:
                    //Console.WriteLine("DOWN : " + posX + ":" + posY + ":");
                    //Console.WriteLine(Clipboard.GetText());
                    break;
            }
            return CallNextHookEx(hhook, code, wParam, ref lParam);
        }
        #endregion

        #region DLL imports
        /// <summary>
        /// Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
        /// </summary>
        /// <param name="idHook">The id of the event you want to hook</param>
        /// <param name="callback">The callback.</param>
        /// <param name="hInstance">The handle you want to attach the event to, can be null</param>
        /// <param name="threadId">The thread you want to attach the event to, can be null</param>
        /// <returns>a handle to the desired hook</returns>
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, MouseHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        /// Unhooks the windows hook.
        /// </summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="lpFileName">Name of the library</param>
        /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// Calls the next hook.
        /// </summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wparam.</param>
        /// <param name="lParam">The lparam.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref MouseHookStruct lParam);
        #endregion
    }
}