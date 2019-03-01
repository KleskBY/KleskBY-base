using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Orion.Managers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Orion.Other
{
    internal static class Imports
    {
        public static bool IsWindowFocues(Process procName)
        {
            IntPtr activatedHandle = Imports.GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero) return false;
            Imports.GetWindowThreadProcessId(activatedHandle, out int activeProcId);
            return activeProcId == procName.Id;
        }

        public static int GetClassID(int id)
        {
            int classID = MemoryManager.ReadMemory<int>(id + 0x8);
            int classID2 = MemoryManager.ReadMemory<int>(classID + 2 * 0x4);
            int classID3 = MemoryManager.ReadMemory<int>(classID2 + 1);
            return MemoryManager.ReadMemory<int>(classID3 + 20);
        }

        public static System.Drawing.Size GetControlSize(IntPtr hWnd, Rectangle pRect)
        {
            System.Drawing.Size cSize = new System.Drawing.Size();

            // get coordinates relative to window
            GetWindowRect(hWnd, pRect);

            cSize.Width = pRect.Right - pRect.Left;
            cSize.Height = pRect.Bottom - pRect.Top;

            return cSize;
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, Rectangle rect);

        [DllImport("user32.dll")] //watcher
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImportAttribute("user32.dll")] //move menu
        public static extern bool ReleaseCapture();

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        //colo
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(int hObject);
        public static string CutString(string mystring)
        {
            char[] chArray = mystring.ToCharArray();
            string str = "";
            for (int i = 0; i < mystring.Length; i++)
            {
                if ((chArray[i] == ' ') && (chArray[i + 1] == ' '))
                {
                    return str;
                }
                if (chArray[i] == '\0')
                {
                    return str;
                }
                str = str + chArray[i].ToString();
            }
            return mystring.TrimEnd(new char[] { '0' });
        }

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Int32 vKey);

        [DllImport("User32.dll")]
        public static extern short GetKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("User32.dll")]
        public static extern short GetKeyState(System.Int32 vKey);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long GetAsyncKeyState(long vKey);

    }
}
