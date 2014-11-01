using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;




    /// <summary>
    /// Windows API constants and functions
    /// </summary>
    public sealed class WinApis
    {
        public const uint MAX_PATH = 512;
        public const uint STGM_READ = 0x00000000;
        public const uint SHDVID_SSLSTATUS = 33;
        public const int GWL_WNDPROC = -4;
        public const uint KEYEVENTF_EXTENDEDKEY = 0x01;
        public const uint KEYEVENTF_KEYUP = 0x02;

        public const short
            //defined inWTypes.h
            // 0 == FALSE, -1 == TRUE
            //typedef short VARIANT_BOOL;
            VAR_TRUE = -1,
            VAR_FALSE = 0;

        #region Methods - GetWindowName - GetWindowClass

        public static int HiWord(int number)
        {
            if ((number & 0x80000000) == 0x80000000)
                return (number >> 16);
            else
                return (number >> 16) & 0xffff;
        }

        public static int LoWord(int number)
        {
            return number & 0xffff;
        }

        public static int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        public static string GetWindowName(IntPtr Hwnd)
        {
            if (Hwnd == IntPtr.Zero)
                return string.Empty;
            // This function gets the name of a window from its handle
            StringBuilder Title = new StringBuilder((int)WinApis.MAX_PATH);
            WinApis.GetWindowText(Hwnd, Title, (int)WinApis.MAX_PATH);

            return Title.ToString().Trim();
        }

        public static string GetWindowClass(IntPtr Hwnd)
        {
            if (Hwnd == IntPtr.Zero)
                return string.Empty;
            // This function gets the name of a window class from a window handle
            StringBuilder Title = new StringBuilder((int)WinApis.MAX_PATH);
            WinApis.RealGetWindowClass(Hwnd, Title, (int)WinApis.MAX_PATH);

            return Title.ToString().Trim();
        }

     
        /// <summary>
        /// UrlCache functionality is taken from:
        /// Scott McMaster (smcmaste@hotmail.com)
        /// CodeProject article
        /// 
        /// There were some issues with preparing URLs
        /// for RegExp to work properly. This is
        /// demonstrated in AllForms.SetupCookieCachePattern method
        /// 
        /// urlPattern:
        /// . Dump the entire contents of the cache.
        /// Cookie: Lists all cookies on the system.
        /// Visited: Lists all of the history items.
        /// Cookie:.*\.example\.com Lists cookies from the example.com domain.
        /// http://www.example.com/example.html$: Lists the specific named file if present
        /// \.example\.com: Lists any and all entries from *.example.com.
        /// \.example\.com.*\.gif$: Lists the .gif files from *.example.com.
        /// \.js$: Lists the .js files in the cache.
        /// </summary>
        /// <param name="urlPattern"></param>
        /// <returns></returns>
      


        #endregion

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
           UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int CallWindowProc(
            IntPtr lpPrevWndFunc, IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(
            IntPtr hWnd, int nIndex, IntPtr newProc);

        [DllImport("ole32.dll", SetLastError = true,
        ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int RevokeObjectParam(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszKey);

    

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
   
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth,
           int nHeight);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

      
       

        [DllImport("ole32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CreateBindCtx(
            [MarshalAs(UnmanagedType.U4)] uint dwReserved,
            [Out, MarshalAs(UnmanagedType.Interface)] out IBindCtx ppbc);

       

        [DllImport("urlmon.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CreateURLMoniker(
            [MarshalAs(UnmanagedType.Interface)] IMoniker pmkContext,
            [MarshalAs(UnmanagedType.LPWStr)] string szURL,
            [Out, MarshalAs(UnmanagedType.Interface)] out IMoniker ppmk);

        public const uint URL_MK_LEGACY = 0;
        public const uint URL_MK_UNIFORM = 1;
        public const uint URL_MK_NO_CANONICALIZE = 2;
        [DllImport("urlmon.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CreateURLMonikerEx(
            [MarshalAs(UnmanagedType.Interface)] IMoniker pmkContext,
            [MarshalAs(UnmanagedType.LPWStr)] string szURL,
            [Out, MarshalAs(UnmanagedType.Interface)] out IMoniker ppmk,
            uint URL_MK_XXX); //Flags, one of 

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg,
            IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg,
            IntPtr wParam, ref StringBuilder lParam);

        
        [DllImport("ole32.dll", CharSet = CharSet.Auto)]
        public static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease,
          [MarshalAs(UnmanagedType.Interface)] out IStream ppstm);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

      

        //MessageBox(new IntPtr(0), "Text", "Caption", 0 );
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(
            IntPtr hWnd, String text, String caption, uint type);

       

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder title, int size);

        [DllImport("user32.dll")]
        public static extern uint RealGetWindowClass(IntPtr hWnd, StringBuilder pszType, uint cchType);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

      

        [DllImport("ole32.dll")]
        public static extern void ReleaseStgMedium(
            [In, MarshalAs(UnmanagedType.Struct)]
            ref System.Runtime.InteropServices.ComTypes.STGMEDIUM pmedium);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern uint DragQueryFile(IntPtr hDrop, uint iFile,
           [Out] StringBuilder lpszFile, uint cch);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        public static extern UIntPtr GlobalSize(IntPtr hMem);

       

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long FindCloseUrlCache(IntPtr hEnumHandle);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern IntPtr FindFirstUrlCacheEntry(string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, out UInt32 lpdwFirstCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long FindNextUrlCacheEntry(IntPtr hEnumHandle, IntPtr lpNextCacheEntryInfo, out UInt32 lpdwNextCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool GetUrlCacheEntryInfo(string lpszUrlName, IntPtr lpCacheEntryInfo, out UInt32 lpdwCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long DeleteUrlCacheEntry(string lpszUrlName);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern IntPtr RetrieveUrlCacheEntryStream(string lpszUrlName, IntPtr lpCacheEntryInfo, out UInt32 lpdwCacheEntryInfoBufferSize, long fRandomRead, UInt32 dwReserved);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern IntPtr ReadUrlCacheEntryStream(IntPtr hUrlCacheStream, UInt32 dwLocation, IntPtr lpBuffer, out UInt32 lpdwLen, UInt32 dwReserved);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long UnlockUrlCacheEntryStream(IntPtr hUrlCacheStream, UInt32 dwReserved);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam,
            IntPtr pvParam, uint fWinIni);

        private const uint SPIF_UPDATEINIFILE = 0x0001;
        private const uint SPIF_SENDWININICHANGE = 0x0002;
        //SPIF_SENDCHANGE       SPIF_SENDWININICHANGE
        private const uint SPI_SETBEEP = 0x0002;

        //For older windows
        public static bool SetSystemBeep(bool bEnable)
        {
            if (bEnable)
                return SystemParametersInfo(SPI_SETBEEP, 1, IntPtr.Zero, (SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE));
            else
                return SystemParametersInfo(SPI_SETBEEP, 0, IntPtr.Zero, (SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE));
        }

        //Pass IntPtr.Zero for hInternet to indicate global
        //dwOption, one of INTERNET_OPTION_XXXX flags

        //To retrieve all cookies for a particular domain, call 
        //InternetGetCookie[Ex]. To delete them, call InternetSetCookie[Ex]: pass 
        //IntPtr.Zero for cookie data to delete a cookie.

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(IntPtr hInternet,
        int dwOption,
        IntPtr lpBuffer,
            //Len of lpBuffer in bytes
            //If lpBuffer contains a string, the size is in TCHARs. If lpBuffer contains anything other than a string, the size is in bytes.
        int lpdwBufferLength);

        //call DoOrganizeFavDlg( this.Handle.ToInt64(), null );
        [DllImport("shdocvw.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long DoOrganizeFavDlg(long hWnd, string lpszRootFolder);

        [DllImport("shdocvw.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern long AddUrlToFavorites(long hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string pszUrlW,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTitleW, //If null, url value is used
            [MarshalAs(UnmanagedType.Bool)] bool fDisplayUI);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetGetCookie(string lpszUrlName, string lpszCookieName,
             [Out] string lpszCookieData, [MarshalAs(UnmanagedType.U4)] out int lpdwSize);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName,
             IntPtr lpszCookieData);

    }
