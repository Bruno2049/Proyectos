/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace PAEEEM.Helpers
{
    /// <summary>
    /// Helper class
    /// </summary>
    /// <remarks>
    /// Provide common tools in this application
    /// </remarks>
    static public class LsUtility
    {
        private static readonly object lockObject = new object();
        /// <summary>
        /// Get the number sequence
        /// </summary>
        /// <returns></returns>
        public static int GetNumberSequence(string type)
        {
            //string resultNumber = ""; // Comment by Tina 2011/08/24
            string SQL = "";
            //object lockObject = new object();
            int num = 0;// Update by Tina 2011/08/24

            try
            {
                SQL = "SELECT Secuencia_No FROM H_NUMERO_SECUENCIA WHERE Secuencia_Tipo = '" + type + "'";
                LsApplicationState ApplicationState = new LsApplicationState(HttpContext.Current.Application);
                LsDatabase Database = new LsDatabase(ApplicationState.SQLConnString);

                lock (lockObject)//prevent concurrency access
                {
                    object o = Database.ExecuteSQLScalar(SQL);
                    if (o != null)
                    {
                        num = Convert.ToInt32(o) + 1; // Update by Tina 2011/08/24
                        //resultNumber = num.ToString(); //Comment by Tina 2011/08/24
                        SQL = "UPDATE H_NUMERO_SECUENCIA SET Secuencia_No = '" + num + "' WHERE Secuencia_Tipo = '" + type + "'";
                        Database.ExecuteSQLNonQuery(SQL);
                    }
                }
            }
            catch (LsDAException ex)
            {
                throw new LsDAException(null, "Get Number Sequence Failed." + ex.Message, ex, true);
            }

            return num; // Update by Tina 2011/08/24
        }

        public static DataTable GetStatusByType(string type)
        {
            string SQL = "";
            DataTable dt = null;
            try
            {
                SQL = "SELECT StatusID, StatusName FROM Status WHERE StatusClass = '" + type + "'";
                LsApplicationState ApplicationState = new LsApplicationState(HttpContext.Current.Application);
                LsDatabase Database = new LsDatabase(ApplicationState.SQLConnString);

                Database.ExecuteSQLDataTable(SQL, out dt);
            }
            catch (LsDAException ex)
            {
                throw new LsDAException(null, "Get Status Failed." + ex.Message, ex, true);
            }

            return dt;
        }
        /// <summary>
        /// Get permissions for a specific user
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>Permission List</returns>
        static public List<string> GetPermissions(string userid)
        {
            List<string> permissions = new List<string>();
            string SQL = "";

            try
            {
                SQL = "SELECT PermissionID FROM Role_Permission WHERE RoleID = (SELECT RoleID FROM User WHERE UserID = '" + userid + "'" + ")";
                LsApplicationState ApplicationState = new LsApplicationState(HttpContext.Current.Application);
                LsDatabase Database = new LsDatabase(ApplicationState.SQLConnString);

                DataTable PermissionIDs = null;
                Database.ExecuteSQLDataTable(SQL, out PermissionIDs);
                if (PermissionIDs.Rows.Count > 0)
                {
                    foreach (DataRow row in PermissionIDs.Rows)
                    {
                        SQL = "SELECT PermissionName FROM Permission WHERE ID = " + Convert.ToInt32(row[0]) + "";
                        object o = Database.ExecuteSQLScalar(SQL);
                        if (o != null)
                        {
                            permissions.Add(o.ToString());
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(null, "Get Permissions Failed." + ex.Message, ex, true);
            }
            return permissions;
        }
        /// <summary>
        /// Convert bool value to string
        /// </summary>
        /// <param name="bvalue">bool value</param>
        /// <returns>string</returns>
        static public string ConvertBoolToString(bool bvalue)
        {
            string strValue = "";

            if (bvalue == true)
            {
                strValue = "y";
            }
            else
            {
                strValue = "f";
            }
            return strValue;
        }
        /// <summary>
        /// Convert string to bool
        /// </summary>
        /// <param name="strValue">string value</param>
        /// <returns>bool</returns>
        static public bool ConvertStringToBool(string strValue)
        {
            bool bValue = false;

            switch (strValue.ToUpper())
            {
                case "YES":
                case "Y":
                    bValue = true;
                    break;
                case "NO":
                case "N":
                    bValue = false;
                    break;
                default:
                    break;
            }
            return bValue;
        }
        /// <summary>
        /// Convert string to datetime
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>DateTime</returns>
        static public DateTime ConvertStringToDateTime(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime shortdate;
                string[] dateparams;
                if (date.Contains('/'))
                {
                    dateparams = date.Split('/');
                }
                else if (date.Contains('-'))
                {
                    dateparams = date.Split('-');
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "zh-cn")
                    {
                        dateparams = new string[] { "1900", "1", "1" };
                    }
                    else
                    {
                        dateparams = new string[] { "1", "1", "1900" };
                    }
                }
                if (dateparams.Length >= 3)
                {
                    if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "zh-cn")
                    {
                        shortdate = new DateTime(Convert.ToInt32(dateparams[0]), Convert.ToInt32(dateparams[1]), Convert.ToInt32(dateparams[2]));
                    }
                    else
                    {
                        shortdate = new DateTime(Convert.ToInt32(dateparams[2]), Convert.ToInt32(dateparams[0]), Convert.ToInt32(dateparams[1]));
                    }
                }
                else
                {
                    shortdate = DateTime.Now;
                }
                return shortdate;
            }
            else
            {
                return DateTime.Now;
            }
        }
        /// <summary>
        /// Convert English month to number
        /// </summary>
        /// <param name="month">Month</param>
        /// <returns>Number</returns>
        static public int ConvertMonthToNumber(string month)
        {
            int iMonth = 0;
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Equals("en-US"))
            {
                switch (month)
                {
                    case "January":
                        iMonth = 1;
                        break;
                    case "February":
                        iMonth = 2;
                        break;
                    case "March":
                        iMonth = 3;
                        break;
                    case "April":
                        iMonth = 4;
                        break;
                    case "May":
                        iMonth = 5;
                        break;
                    case "June":
                        iMonth = 6;
                        break;
                    case "July":
                        iMonth = 7;
                        break;
                    case "August":
                        iMonth = 8;
                        break;
                    case "September":
                        iMonth = 9;
                        break;
                    case "October":
                        iMonth = 10;
                        break;
                    case "November":
                        iMonth = 11;
                        break;
                    case "December":
                        iMonth = 12;
                        break;
                    default:
                        break;
                }
            }
            else if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Equals("es-MX"))
            {
                switch (month)
                {
                    case "enero":
                        iMonth = 1;
                        break;
                    case "febrero":
                        iMonth = 2;
                        break;
                    case "marzo":
                        iMonth = 3;
                        break;
                    case "abril":
                        iMonth = 4;
                        break;
                    case "mayo":
                        iMonth = 5;
                        break;
                    case "junio":
                        iMonth = 6;
                        break;
                    case "julio":
                        iMonth = 7;
                        break;
                    case "agosto":
                        iMonth = 8;
                        break;
                    case "septiembre":
                        iMonth = 9;
                        break;
                    case "octubre":
                        iMonth = 10;
                        break;
                    case "noviembre":
                        iMonth = 11;
                        break;
                    case "diciembre":
                        iMonth = 12;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                return iMonth;
            }
            return iMonth;
        }

        /// <summary>
        /// Convert number to month
        /// </summary>
        /// <param name="month">Month</param>
        /// <returns>Number</returns>
        static public string ConvertNumberToMonthName(int iMonth)
        {
            string month = "";

            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Equals("es-MX"))
            {
                switch (iMonth)
                {
                    case 1:
                        month = "enero";
                        break;
                    case 2:
                        month = "febrero";
                        break;
                    case 3:
                        month = "marzo";
                        break;
                    case 4:
                        month = "abril";
                        break;
                    case 5:
                        month = "mayo";
                        break;
                    case 6:
                        month = "junio";
                        break;
                    case 7:
                        month = "julio";
                        break;
                    case 8:
                        month = "agosto";
                        break;
                    case 9:
                        month = "septiembre";
                        break;
                    case 10:
                        month = "octubre";
                        break;
                    case 11:
                        month = "noviembre";
                        break;
                    case 12:
                        month = "diciembre";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (iMonth)
                {
                    case 1:
                        month = "January";
                        break;
                    case 2:
                        month = "February";
                        break;
                    case 3:
                        month = "March";
                        break;
                    case 4:
                        month = "April";
                        break;
                    case 5:
                        month = "May";
                        break;
                    case 6:
                        month = "June";
                        break;
                    case 7:
                        month = "July";
                        break;
                    case 8:
                        month = "August";
                        break;
                    case 9:
                        month = "September";
                        break;
                    case 10:
                        month = "October";
                        break;
                    case 11:
                        month = "November";
                        break;
                    case 12:
                        month = "December";
                        break;
                    default:
                        break;
                }
            }
            return month;
        }
        /// <summary>
        /// Get program code from program name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static public string FromNameToCode(string name)
        {
            string code = string.Empty;
            switch (name)
            {
                case "LsWeb":
                    code = "100";
                    break;
                default:
                    break;
            }
            return code;
        }
        /// <summary>
        /// Remove the Checksum character from scanned barcode number
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        static public string ProcessBarCode(string barcode)
        {
            return barcode.Substring(0, barcode.Length - 1);
        }
        //edit by coco 2012-03-29
        /// <SUMMARY>
        /// Pictures zoom
        /// </SUMMARY>
        /// <PARAM name="sourceFile">Source image path</PARAM>
        /// <PARAM name="destFile">Destination image path</PARAM>
        /// <PARAM name="destHeight">Destination image height</PARAM>
        /// <PARAM name="destWidth">Destination image width</PARAM>
        /// <RETURNS></RETURNS>
        public static bool ScalingAndUploadImage(Stream SourceStream, string destFile, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = System.Drawing.Image.FromStream(SourceStream); 
            //System.Drawing.Image imgSource = System.Drawing.Image.FromFile(@sourceFile);
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // By scaling
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;

            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }               
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.WhiteSmoke);

            // setup the quality of drawing panel
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //(destWidth - sW) / 2, (destHeight - sH) / 2
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // setup the compression quality
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            try
            {
                //get the built-in image encoder info stored in ImageCodecInfo object
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];//set JPEG code
                        break;
                    }
                }

                if (jpegICI != null)
                {
                    outBmp.Save(destFile, jpegICI, encoderParams);
                }
                else
                {
                    outBmp.Save(destFile, thisFormat);
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                imgSource.Dispose();
                outBmp.Dispose();
            }
        }
    }
}
