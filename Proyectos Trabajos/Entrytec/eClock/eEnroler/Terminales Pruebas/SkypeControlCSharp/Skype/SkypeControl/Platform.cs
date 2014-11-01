using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SkypeControl
{
    internal class Platform
    {
        [DllImport("user32.dll")]
        public static extern UInt32 RegisterWindowMessage(string lpString);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(IntPtr windowHandle,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam,
            SendMessageTimeoutFlags flags,
            uint timeout,
            out IntPtr result);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(IntPtr windowHandle,
            uint Msg,
            IntPtr wParam,
            ref CopyDataStruct lParam,
            SendMessageTimeoutFlags flags,
            uint timeout,
            out IntPtr result);

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0000,
            SMTO_BLOCK = 0x0001,
            SMTO_ABORTIFHUNG = 0x0002,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
        }

        public static readonly IntPtr HWND_BROADCAST = new IntPtr(-1);

        public static readonly uint WM_COPYDATA = 0x004a;

        [StructLayout(LayoutKind.Sequential)]
        public struct CopyDataStruct
        {
            public string ID;
            public int Length;
            public string Data;
        }

    }
}
