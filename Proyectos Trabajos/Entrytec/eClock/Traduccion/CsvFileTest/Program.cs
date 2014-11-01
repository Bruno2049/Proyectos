//////////////////////////////////////////////////////////////////////////////
// This source code and all associated files and resources are copyrighted by
// the author(s). This source code and all associated files and resources may
// be used as long as they are used according to the terms and conditions set
// forth in The Code Project Open License (CPOL).
//
// Copyright (c) 2012 Jonathan Wood
// http://www.blackbeltcoder.com
//

using System;
using System.Windows.Forms;

namespace CsvFileTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
