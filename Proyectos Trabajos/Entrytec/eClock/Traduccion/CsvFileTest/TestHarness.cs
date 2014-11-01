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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CsvFile;

namespace CsvFileTest
{
    /// <summary>
    /// Testing class for testing the CSV Read and Write classes.
    /// </summary>
    public class TestHarness
    {
        public void RunTests()
        {
            TestReader();
            TestWriter();
        }

        private void TestReader()
        {
            List<string> columns;

            columns = TestReaderLine(String.Empty, false);

            TestReaderLine("", false, EmptyLineBehavior.EmptyColumn);
            TestReaderLine("", false, EmptyLineBehavior.EndOfFile);
            TestReaderLine("", false, EmptyLineBehavior.Ignore);
            TestReaderLine("", false, EmptyLineBehavior.NoColumns);

            columns = TestReaderLine("\r\n123,456\r\n", true, EmptyLineBehavior.EmptyColumn);
            Debug.Assert(columns.Count == 1);
            Debug.Assert(columns[0] == "");

            TestReaderLine("\r\n123,456\r\n", false, EmptyLineBehavior.EndOfFile);

            columns = TestReaderLine("\r\n\r\n\r\n\r\n\r\n123,456\r\n", true, EmptyLineBehavior.Ignore);
            Debug.Assert(columns.Count == 2);
            Debug.Assert(columns[0] == "123");
            Debug.Assert(columns[1] == "456");

            columns = TestReaderLine("\r\n123,456\r\n", true, EmptyLineBehavior.NoColumns);
            Debug.Assert(columns.Count == 0);

            columns = TestReaderLine("a,");
            Debug.Assert(columns.Count == 2);
            Debug.Assert(columns[0] == "a");
            Debug.Assert(columns[1] == "");

            columns = TestReaderLine(", ,,");
            Debug.Assert(columns.Count == 4);
            Debug.Assert(columns[0] == "");
            Debug.Assert(columns[1] == " ");
            Debug.Assert(columns[2] == "");
            Debug.Assert(columns[3] == "");

            columns = TestReaderLine("123,456,789,0");
            Debug.Assert(columns.Count == 4);
            Debug.Assert(columns[0] == "123");
            Debug.Assert(columns[1] == "456");
            Debug.Assert(columns[2] == "789");
            Debug.Assert(columns[3] == "0");

            columns = TestReaderLine("123,\"456");
            Debug.Assert(columns.Count == 2);
            Debug.Assert(columns[0] == "123");
            Debug.Assert(columns[1] == "456");

            columns = TestReaderLine("123,\"456\"789,0");
            Debug.Assert(columns.Count == 3);
            Debug.Assert(columns[0] == "123");
            Debug.Assert(columns[1] == "456789");
            Debug.Assert(columns[2] == "0");

            columns = TestReaderLine("123,\"456\r\n\r\n.\r\n\r\n789\" ,0");
            Debug.Assert(columns.Count == 3);
            Debug.Assert(columns[0] == "123");
            Debug.Assert(columns[1] == "456\r\n\r\n.\r\n\r\n789 ");
            Debug.Assert(columns[2] == "0");

            columns = TestReaderLine("123, \"456,789\",0");
            Debug.Assert(columns.Count == 4);
            Debug.Assert(columns[0] == "123");
            Debug.Assert(columns[1] == " \"456");
            Debug.Assert(columns[2] == "789\"");
            Debug.Assert(columns[3] == "0");
        }

        private List<string> TestReaderLine(string line, bool expectedReturnValue = true, EmptyLineBehavior emptyLineBehavior = EmptyLineBehavior.NoColumns)
        {
            List<string> columns = new List<string>();
            bool result;
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(line));
            using (CsvFileReader reader = new CsvFileReader(stream, emptyLineBehavior))
            {
                result = reader.ReadRow(columns);
            }
            Debug.Assert(result == expectedReturnValue);
            return columns;
        }

        private void TestWriter()
        {
            List<string> columns = new List<string>();
            string result;

            result = TestWriterLine("123", "456", "789");
            Debug.Assert(result == "123,456,789");

            result = TestWriterLine("123", "456,789", "0");
            Debug.Assert(result == "123,\"456,789\",0");

            result = TestWriterLine("123", "456\r\n\r\n789", "0");
            Debug.Assert(result == "123,\"456\r\n\r\n789\",0");

            result = TestWriterLine("123", "456\"789", "0");
            Debug.Assert(result == "123,\"456\"\"789\",0");

            result = TestWriterLine("123", "456\r789", "0");
            Debug.Assert(result == "123,\"456\r789\",0");

            result = TestWriterLine("123", "456\n789", "0");
            Debug.Assert(result == "123,\"456\n789\",0");

            result = TestWriterLine("", "", "");
            Debug.Assert(result == ",,");

            result = TestWriterLine("", "");
            Debug.Assert(result == ",");

            result = TestWriterLine("");
            Debug.Assert(result == "");

            result = TestWriterLine();
            Debug.Assert(result == "");
        }

        private string TestWriterLine(params string[] columns)
        {
            // Write line to memory stream
            MemoryStream stream = new MemoryStream();
            using (CsvFileWriter writer = new CsvFileWriter(stream))
            {
                writer.WriteRow(new List<string>(columns));
            }
            // Get data written
            byte[] buffer = stream.GetBuffer();
            string s = Encoding.UTF8.GetString(buffer);
            // Remove trailing zeros
            int pos = s.IndexOf('\0');
            if (pos != -1)
                s = s.Substring(0, pos);
            // Remove trailing new line
            Debug.Assert(s.Length >= Environment.NewLine.Length);
            Debug.Assert(s.Substring(pos - Environment.NewLine.Length, Environment.NewLine.Length) == Environment.NewLine);
            s = s.Substring(0, s.Length - Environment.NewLine.Length);
            return s;
        }
    }
}
