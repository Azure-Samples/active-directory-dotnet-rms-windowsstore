//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Different utility methods consumed by the other classes
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// Checks if given file extension is supported protected extension type by this app or not
        /// </summary>
        /// <param name="fileExtension">File extension e.g. ".ptxt"</param>
        /// <returns>True if given file extension is supported protected extension type</returns>
        public static bool IsValidProtectedFileExtension(string fileExtension)
        {
            //TODO fileExtension starts with "." or not
            if (Utility.IsProtectedTextFileExtension(fileExtension)
                || Utility.IsProtectedJpgFileExtension(fileExtension)
                || Utility.IsProtectedGenericFileExtension(fileExtension)
                )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Constructs the file name for un-encrypted file
        /// </summary>
        /// <param name="encryptedFileName">Encrypted file name</param>
        /// <returns>File name for un-encrypted file</returns>
        public static string GetUnencryptedFileName(string encryptedFileName)
        {
            string originalExtension = Path.GetExtension(encryptedFileName); //TODO fileExtension starts with "." or not

            // If file does not have any extension
            if (originalExtension == string.Empty)
            {
                return encryptedFileName;
            }

            // TODO need more generic logic below
            string newExtension = string.Empty;

            if (Utility.IsProtectedTextFileExtension(originalExtension))
            {
                newExtension = ".txt";
            }
            else if (Utility.IsProtectedJpgFileExtension(originalExtension))
            {
                newExtension = ".jpg";
            }
            else if (Utility.IsProtectedGenericFileExtension(originalExtension))
            {
                newExtension = string.Empty;
            }
            else
            {
                newExtension = originalExtension;
            }

            return string.Format("{0}{1}", Path.GetFileNameWithoutExtension(encryptedFileName), newExtension);
        }

        /// <summary>
        /// Checks if format of the email id/user id is valid
        /// </summary>
        /// <param name="emailId">Email id</param>
        /// <returns>True if the </returns>
        public static bool IsValidEmailAddress(string emailId)
        {
            if (string.IsNullOrWhiteSpace(emailId))
            {
                return false;
            }

            Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                  RegexOptions.IgnoreCase);
            return re.IsMatch(emailId);
        }


        /// <summary>
        /// Checks if file extension is of protected text type or not
        /// </summary>
        /// <param name="fileExtension">File extension e.g. ".ptxt"</param>
        /// <returns>True if file extension is of protected text type</returns>
        public static bool IsProtectedTextFileExtension(string fileExtension)
        {
            if (string.Compare(
                fileExtension,
                Constants.ProtectedTextFileExtension,
                StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if file extension is of protected jpg type or not
        /// </summary>
        /// <param name="fileExtension">File extension e.g. ".pjpg"</param>
        /// <returns>True if file extension is of protected jpg type</returns>
        public static bool IsProtectedJpgFileExtension(string fileExtension)
        {
            if (string.Compare(
                fileExtension,
                Constants.ProtectedJpgFileExtension,
                StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if file extension is of protected generic type or not
        /// </summary>
        /// <param name="fileExtension">File extension e.g. ".pfile"</param>
        /// <returns>True if file extension is of protected generic type</returns>
        public static bool IsProtectedGenericFileExtension(string fileExtension)
        {
            if (string.Compare(
                fileExtension,
                Constants.ProtectedGenericFileExtension,
                StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
