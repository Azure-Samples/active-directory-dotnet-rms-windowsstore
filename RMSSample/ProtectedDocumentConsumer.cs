//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    ///     Class that has consuption flow related functionalities
    /// </summary>
    internal class ProtectedDocumentConsumer
    {
        /// <summary>
        ///     Initializes a new instance of ProtectedDocumentConsumer with the file that needs to be decrypted
        /// </summary>
        /// <param name="encryptedfile">Encrypted or protected file</param>
        /// <param name="consentCallback">Consent callback method</param>
        /// <param name="authCallback">Authentication callback method</param>
        /// <param name="userId">User id or Email id</param>
        public ProtectedDocumentConsumer(IStorageFile encryptedfile, IConsentCallback consentCallback,
            IAuthenticationCallback authCallback, string userId)
        {
            if (encryptedfile == null)
            {
                throw new ArgumentNullException("encryptedfile");
            }

            if (!Utility.IsValidProtectedFileExtension(encryptedfile.FileType))
            {
                throw new ArgumentException("Invalid File.");
            }

            EncryptedFile = encryptedfile;
            UnencryptedFileName = Utility.GetUnencryptedFileName(encryptedfile.Name);
            AuthenticationCallback = authCallback;
            IsLoaded = false;
            UserId = userId;
            Result = null;
            ConsentCallback = consentCallback;
        }

        /// <summary>
        ///     User id or email id of the user
        /// </summary>
        private string UserId { get; set; }

        /// <summary>
        ///     IAuthenticationCallback
        /// </summary>
        private IAuthenticationCallback AuthenticationCallback { get; set; }

        /// <summary>
        ///     IConsentCallback
        /// </summary>
        private IConsentCallback ConsentCallback { get; set; }

        /// <summary>
        ///     Result of the aqcuiring protected stream
        /// </summary>
        protected GetProtectedFileStreamResult Result { get; private set; }

        /// <summary>
        ///     Flag to check if the protected stream is acquired
        /// </summary>
        private bool IsLoaded { get; set; }

        /// <summary>
        ///     Gets the un-encrypted file's name (no path)
        /// </summary>
        public string UnencryptedFileName { get; private set; }

        /// <summary>
        ///     Encrypted version of the file
        /// </summary>
        public IStorageFile EncryptedFile { get; private set; }

        /// <summary>
        ///     Acquires the protected file stream, loads the policy information
        /// </summary>
        /// <returns>Protected file stream result</returns>
        public async Task<GetProtectedFileStreamResult> LoadAsync()
        {
            if (!IsLoaded)
            {
                // Create a ProtectedFileStream to read the contents from the .ptxt or .pjpg file.
                // Note: if the opened file is not a .ptxt file, the ProtectedFileStream will read it as-is. 
                //       So we don't need to write a special case for regular .txt files.
                var encryptedFileStream = await EncryptedFile.OpenAsync(FileAccessMode.Read);
                Result = await ProtectedFileStream.AcquireAsync(
                    encryptedFileStream,
                    UserId,
                    AuthenticationCallback,
                    ConsentCallback,
                    PolicyAcquisitionOptions.None);

                IsLoaded = true;
            }

            return Result;
        }
    }
}