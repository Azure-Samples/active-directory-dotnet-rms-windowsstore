//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Class that has functionalities specific to protected text consuption flow
    /// </summary>
    internal class ProtectedTextDocumentConsumer : ProtectedDocumentConsumer, IDocument, IProtectedDocumentConsumer
    {
        /// <summary>
        /// Text content of the decrypted file
        /// </summary>
        private string decryptedText;

        /// <summary>
        /// Text content of the decrypted file
        /// </summary>
        private const int MaximumTextSize = 65536;


        /// <summary>
        /// Initializes a new instance of ProtectedTextDocumentConsumer class with the file that needs to be decrypted
        /// </summary>
        /// <param name="encryptedfile">Protected file that needs to be decrypted</param>
        /// <param name="consentCallback">consent callback method</param>
        /// <param name="authCallback">Authentication callback method</param>
        /// <param name="userId">User id or Email id</param>
        public ProtectedTextDocumentConsumer(IStorageFile encryptedfile, IConsentCallback consentCallback, IAuthenticationCallback authCallback, string userId) :
            base(encryptedfile, consentCallback, authCallback, userId)
        {
            this.decryptedText = null;
        }

        /// <summary>
        /// Gets the text content of the file
        /// </summary>
        /// <returns></returns>
        public async Task<String> GetTextAsync()
        {
            await this.LoadAsync();

            if (this.result.Status != GetUserPolicyResultStatus.Success)
            {
                throw new RMSException(
                    String.Format(
                    "Error in reading the document. Your policy status is {0}",
                    this.result.Status.ToString())
                    );
            }

            if (this.decryptedText == null)
            {
                // Since user can do reads multiple times in the app, it is safer to
                // set stream position at 0 before each read.
                this.result.Stream.Seek(0);
                using (var reader = new DataReader(this.result.Stream))
                {
                    try
                    {
                        // Note that stream.Size might return the encrypted size of the content 
                        // which may be different from the original size. So we need to use the actual size of the content
                        uint actualSize = await reader.LoadAsync((uint)this.result.Stream.Size);
                        this.decryptedText = reader.ReadString(actualSize > MaximumTextSize ? MaximumTextSize : actualSize);
                    }
                    finally
                    {
                        if (reader != null)
                        {
                            reader.DetachStream();
                        }
                    }
                }
            }

            return this.decryptedText;
        }
    }
}
