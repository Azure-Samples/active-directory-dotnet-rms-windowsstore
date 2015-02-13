//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Different constants 
    /// </summary>
    internal class Constants
    {
        /// <summary>
        /// File extension of the protected text file
        /// </summary>
        public const string ProtectedTextFileExtension = ".ptxt";

        /// <summary>
        /// File extension of the protected jpg file
        /// </summary>
        public const string ProtectedJpgFileExtension = ".pjpg";

        /// <summary>
        /// File extension of the generic protected file
        /// </summary>
        public const string ProtectedGenericFileExtension = ".pfile";

        /// <summary>
        /// Key against which user email id is stored in the app data
        /// </summary>
        public const string UserEmailIdKey = "RMSUserEmailId";

        /// <summary>
        /// ADAL error code when authentication canceled
        /// </summary>
        public const string AuthenticationCanceledErrorCode = "authentication_canceled";
    }
}
