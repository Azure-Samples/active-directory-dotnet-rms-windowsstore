//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Class that has consuption flow related functionalities
    /// </summary>
    internal class ProtectedDocumentConsumer : IProtectedDocumentConsumer
    {
        /// <summary>
        /// User id or email id of the user
        /// </summary>
        protected string userId { get; private set; }

        /// <summary>
        /// IAuthenticationCallback 
        /// </summary>
        protected IAuthenticationCallback authenticationCallback { get; private set; }

        /// <summary>
        /// IConsentCallback
        /// </summary>
        protected IConsentCallback consentCallback { get; private set; }

        /// <summary>
        /// Result of the aqcuiring protected stream
        /// </summary>
        protected GetProtectedFileStreamResult result { get; private set; }

        /// <summary>
        /// Flag to check if the protected stream is acquired 
        /// </summary>
        protected bool isLoaded { get; private set; }

        /// <summary>
        /// Initializes a new instance of ProtectedDocumentConsumer with the file that needs to be decrypted
        /// </summary>
        /// <param name="encryptedfile">Encrypted or protected file</param>
        /// <param name="consetCallback">Consent callback method</param>
        /// <param name="authCallback">Authentication callback method</param>
        /// <param name="userId">User id or Email id</param>
        public ProtectedDocumentConsumer(IStorageFile encryptedfile, IConsentCallback consentCallback, IAuthenticationCallback authCallback, string userId)
        {
            if (encryptedfile == null)
            {
                // TODO:
                throw new ArgumentNullException("The file parameter cannot be null.");
            }

            // TODO if file extension is not of the supported types (e.g .ptxt, .pjpg...) then throw error
            if (!Utility.IsValidProtectedFileExtension(encryptedfile.FileType))
            {
                throw new ArgumentException("Invalid File.");
            }
            else
            {
                this.EncryptedFile = encryptedfile;
                this.EncryptedFileName = this.EncryptedFile.Name;
                this.UnencryptedFileName = Utility.GetUnencryptedFileName(this.EncryptedFileName);
                this.authenticationCallback = authCallback;
                this.isLoaded = false;
                this.userId = userId;
                this.result = null;
                this.consentCallback = consentCallback;
            }
        }

        /// <summary>
        /// Gets the encrypted file's name (no path)
        /// </summary>
        public string EncryptedFileName { get; private set; }

        /// <summary>
        /// Gets the un-encrypted file's name (no path)
        /// </summary>
        public string UnencryptedFileName { get; private set; }

        /// <summary>
        /// Encrypted version of the file
        /// </summary>
        public IStorageFile EncryptedFile { get; private set; }

        /// <summary>
        /// Acquires the protected file stream, loads the policy information
        /// </summary>
        /// <returns>Protected file stream result</returns>
        public virtual async Task<GetProtectedFileStreamResult> LoadAsync()
        {
            if (!this.isLoaded)
            {
                // Create a ProtectedFileStream to read the contents from the .ptxt or .pjpg file.
                // Note: if the opened file is not a .ptxt file, the ProtectedFileStream will read it as-is. 
                //       So we don't need to write a special case for regular .txt files.
                IRandomAccessStream encryptedFileStream = await this.EncryptedFile.OpenAsync(FileAccessMode.Read);
                this.result = await ProtectedFileStream.AcquireAsync(
                    encryptedFileStream,
                    this.userId,
                    this.authenticationCallback,
                    this.consentCallback,
                    PolicyAcquisitionOptions.None);

                this.isLoaded = true;
            }

            return this.result;
        }      
    }
}
