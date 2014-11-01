/*
	Copyright IMPRA, Inc. 2011
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
using System.Text;
using System.Text.RegularExpressions;

namespace PAEEEM.Helpers
{
    public class GlossaryField
    {
        private string _column_name = "";
        private string _data_type = "";
        private bool _is_primary = false;
        private bool _is_foreign = false;
        private string _parent_table = "";
        private string _parent_column = "";
        private string _display_column = "";
        private bool _is_display = false;
        private string _display_name = "";
        private bool _is_identity = false;
        private bool _is_mandatory = false;
        private bool _is_phone = false;
        private bool _is_email = false;
        private bool _is_zip = false;
        private string _owned_table = "";
        private const string PHONE_FORMAT_EXPRE = @"^(\d{10})$";
        private const string EMAIL_FORMAT_EXPRE = @"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$";
        private const string ZIP_FORMAT_EXPRE = @"^(\d{5})$";

        public string ColumnName
        {
            get { return this._column_name; }
            set { this._column_name = value; }
        }

        public string DataType
        {
            get { return this._data_type; }
            set { this._data_type = value; }
        }

        public bool IsPrimary
        {
            get { return this._is_primary; }
            set { this._is_primary = value; }
        }

        public bool IsForeign
        {
            get { return this._is_foreign; }
            set { this._is_foreign = value; }
        }

        public string ParentTable
        {
            get { return this._parent_table; }
            set { this._parent_table = value; }
        }

        public string ParentColumn
        {
            get { return this._parent_column; }
            set { this._parent_column = value; }
        }

        public string DisplayColumn
        {
            get { return this._display_column; }
            set { this._display_column = value; }
        }

        public bool IsDisplay
        {
            get { return this._is_display; }
            set { this._is_display = value; }
        }

        public string DisplayName
        {
            get { return this._display_name; }
            set { this._display_name = value; }
        }

        public bool IsIdentity
        {
            get { return this._is_identity; }
            set { this._is_identity = value; }
        }

        public bool IsMandatory
        {
            get { return this._is_mandatory; }
            set { this._is_mandatory = value; }
        }

        public bool IsPhone
        {
            get { return this._is_phone; }
            set { this._is_phone = value; }
        }

        public bool IsEmail
        {
            get { return this._is_email; }
            set { this._is_email = value; }
        }

        public bool IsZip
        {
            get { return this._is_zip; }
            set { this._is_zip = value; }
        }

        public string OwnedTable
        {
            get { return this._owned_table; }
            set { this._owned_table = value; }
        }

        public bool IsValidEmail(string emailAddress)
        {
            return Regex.IsMatch(emailAddress, EMAIL_FORMAT_EXPRE, RegexOptions.CultureInvariant);
        }

        public bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, PHONE_FORMAT_EXPRE, RegexOptions.CultureInvariant);
        }

        public bool IsValidZipCode(string zipCode)
        {
            return Regex.IsMatch(zipCode, ZIP_FORMAT_EXPRE, RegexOptions.CultureInvariant);
        }
    }
}
