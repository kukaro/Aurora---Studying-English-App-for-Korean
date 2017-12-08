using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utilities
{
    /// <summary>
    /// A class that manages a global low level keyboard hook
    /// </summary>
    class GlobalKeyboardHook
    {
        #region Constant, Structure and Delegate Definitions
        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct lParam);

        public struct mouseHokkStruct
        {
            public int fosX;
            public int fosY;
        }

        public struct keyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        const int WH_KEYBOARD_LL = 0x0D;
        const int WH_MOUSE_LL = 0x0E;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;
        const int WM_LBUTTONDOWN = 0x201;
        const int WM_LBUTTONUP = 0x202;
        #endregion

        #region Instance Variables
        /// <summary>
        /// The collections of keys to watch for
        /// </summary>
        public List<Keys> hookedKeys = new List<Keys>();
        public List<Keys> hookedMouseKeys = new List<Keys>();
        /// <summary>
        /// Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        IntPtr hhook = IntPtr.Zero;
        IntPtr hhookM = IntPtr.Zero;
        private string mainWord;

        private bool isOnCapsLock;
        private bool isOnNumLock;
        private bool isOnScrollLock;
        private bool isOnHanguel;
        private bool isPressedLeftShift;
        private bool isPressedRightShift;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when one of the hooked keys is pressed
        /// </summary>
        public event KeyEventHandler KeyDown;
        /// <summary>
        /// Occurs when one of the hooked keys is released
        /// </summary>
        public event KeyEventHandler KeyUp;
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalKeyboardHook"/> class and installs the keyboard hook.
        /// </summary>
        public GlobalKeyboardHook()
        {
            hook();
            hookedKeys.Add(Keys.A);
            hookedKeys.Add(Keys.B);
            hookedKeys.Add(Keys.C);
            hookedKeys.Add(Keys.D);
            hookedKeys.Add(Keys.E);
            hookedKeys.Add(Keys.F);
            hookedKeys.Add(Keys.G);
            hookedKeys.Add(Keys.H);
            hookedKeys.Add(Keys.I);
            hookedKeys.Add(Keys.J);
            hookedKeys.Add(Keys.K);
            hookedKeys.Add(Keys.L);
            hookedKeys.Add(Keys.M);
            hookedKeys.Add(Keys.N);
            hookedKeys.Add(Keys.O);
            hookedKeys.Add(Keys.P);
            hookedKeys.Add(Keys.Q);
            hookedKeys.Add(Keys.R);
            hookedKeys.Add(Keys.S);
            hookedKeys.Add(Keys.T);
            hookedKeys.Add(Keys.U);
            hookedKeys.Add(Keys.V);
            hookedKeys.Add(Keys.W);
            hookedKeys.Add(Keys.X);
            hookedKeys.Add(Keys.Y);
            hookedKeys.Add(Keys.Z);
            hookedKeys.Add(Keys.Space);
            hookedKeys.Add(Keys.Enter);
            hookedKeys.Add(Keys.Back);
            hookedKeys.Add(Keys.OemQuotes);
            hookedKeys.Add(Keys.OemPeriod);
            hookedKeys.Add(Keys.OemMinus);
            hookedKeys.Add(Keys.Subtract);
            hookedMouseKeys.Add(Keys.LButton);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="GlobalKeyboardHook"/> is reclaimed by garbage collection and uninstalls the keyboard hook.
        /// </summary>
        ~GlobalKeyboardHook()
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
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hInstance, 0);
            hhookM = SetWindowsHookEx(WH_MOUSE_LL, hookMouseProc, hInstance, 0);
        }

        /// <summary>
        /// Uninstalls the global hook
        /// </summary>
        public void unhook()
        {
            UnhookWindowsHookEx(hhook);
            UnhookWindowsHookEx(hhookM);
        }

        /// <summary>
        /// The callback for the keyboard hook
        /// </summary>
        /// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anyting</param>
        /// <param name="wParam">The event type</param>
        /// <param name="lParam">The keyhook event information</param>
        /// <returns></returns>
        public int hookProc(int code, int wParam, ref keyboardHookStruct lParam)
        {
            //Console.WriteLine(toStringToggleKeyState());
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode; ;
                if (hookedKeys.Contains(key))
                {
                    KeyEventArgs kea = new KeyEventArgs(key);
                    if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                    {
                        object charTmp = makeChar(key);
                        if (charTmp != null)
                        {
                            mainWord += charTmp;
                        }
                        else
                        {
                            if (key == Keys.Back)
                            {
                                mainWord = mainWord.Substring(0, mainWord.Length - 1);
                            }
                            else if (key == Keys.Space)
                            {
                                mainWord += " ";
                            }
                            else if (key == Keys.Enter)
                            {
                                mainWord += "\n";
                            }
                            else if (key == Keys.OemQuotes)
                            {
                                mainWord += "\'";
                            }
                            else if (key == Keys.OemPeriod)
                            {
                                mainWord += ".";
                            }
                            else if (key == Keys.OemMinus || key == Keys.Subtract)
                            {
                                if (!(key == Keys.OemMinus && (isPressedLeftShift || isPressedRightShift)))
                                {
                                    mainWord += "-";
                                }
                            }
                        }
                        Console.WriteLine(mainWord);
                    }
                    if (kea.Handled)
                        return 1;
                }
            }
            return CallNextHookEx(hhook, code, wParam, ref lParam);
        }

        public int hookMouseProc(int code, int wParam, ref keyboardHookStruct lParam)
        {
            int posX = lParam.vkCode;
            int posY = lParam.scanCode;
            switch (wParam)
            {
                case WM_LBUTTONUP:
                    Console.WriteLine("UP : " + posX + ":" + posY + ":");
                    break;
                case WM_LBUTTONDOWN:
                    Console.WriteLine("DOWN : " + posX + ":" + posY + ":");
                    break;
            }
            //Console.WriteLine("гою╖ : "+wParam+":"+lParam.vkCode);
            return CallNextHookEx(hhookM, code, wParam, ref lParam);
        }

        /// <summary>
		/// Change toggle key's state
		/// </summary>
        private void changeState()
        {
            isOnCapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            isOnNumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
            isOnScrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
            isOnHanguel = (((ushort)GetKeyState(0x15)) & 0xffff) != 0;
            isPressedLeftShift = (((ushort)GetAsyncKeyState(0xA0)) & 0xffff) != 0;
            isPressedRightShift = (((ushort)GetAsyncKeyState(0xA1)) & 0xffff) != 0;
        }

        /// <summary>
		/// All state to String for debug
		/// </summary>
        private string toStringToggleKeyState()
        {
            return "[Cap : " + isOnCapsLock + ", Num : " + isOnNumLock + ", Scr : " + isOnScrollLock + ", Han : " + isOnHanguel + ", LShift : " + isPressedLeftShift + ", RShift : " + isPressedRightShift + "]";
        }

        public object makeChar(Keys key)
        {
            changeState();
            bool isLower = !isOnCapsLock && (!isPressedLeftShift && !isPressedRightShift);
            bool isAlpha = (int)key >= 'A' && (int)key <= 'Z';
            if (isOnCapsLock && (isPressedLeftShift || isPressedRightShift))
            {
                isLower = true;
            }
            if (isLower && isAlpha)
            {
                return key.ToString().ToLower()[0];
            }
            else if (!isLower && isAlpha)
            {
                return key.ToString()[0];
            }
            return null;
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
        static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        /// Unhooks the windows hook.
        /// </summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        /// Calls the next hook.
        /// </summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wparam.</param>
        /// <param name="lParam">The lparam.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHookStruct lParam);

        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="lpFileName">Name of the library</param>
        /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetAsyncKeyState(int keyCode);
        #endregion
    }
}
