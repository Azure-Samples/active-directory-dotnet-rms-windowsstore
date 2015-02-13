//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Class that has functionalities specific to protected image consuption flow
    /// </summary>
    internal class ProtectedImageDocumentConsumer : ProtectedDocumentConsumer
    {
        /// <summary>
        /// Bitmap image of the decrypted jpg file
        /// </summary>
        private BitmapImage _decryptedBitmap;

        /// <summary>
        /// Initializes a new instance of ProtectedImageDocumentConsumer class with the file that needs to be decrypted
        /// </summary>
        /// <param name="encryptedfile">Protected file that needs to be decrypted</param>
        /// <param name="consentCallback">consent callback method</param>
        /// <param name="authCallback">Authentication callback method</param
        /// <param name="userId">User id or Email id</param>
        public ProtectedImageDocumentConsumer(IStorageFile encryptedfile, IConsentCallback consentCallback, IAuthenticationCallback authCallback, string userId) :
            base(encryptedfile, consentCallback, authCallback, userId)
        {
            _decryptedBitmap = null;
        }

        /// <summary>
        /// Gets the bitmap image of the decrypted jpg file
        /// </summary>
        /// <returns>Bitmap image</returns>
        public async Task<BitmapImage> GetBitmapImageAsync()
        {
            await LoadAsync();

            if (Result.Status != GetUserPolicyResultStatus.Success)
            {
                throw new RMSException(
                    String.Format(
                    "Error in reading the document. Your policy status is {0}",
                    Result.Status.ToString())
                    );
            }

            if (_decryptedBitmap == null)
            {
                _decryptedBitmap = new BitmapImage();
                await _decryptedBitmap.SetSourceAsync(this.Result.Stream);
            }

            return _decryptedBitmap;
        }
    }
}
