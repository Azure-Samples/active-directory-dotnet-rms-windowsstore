//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System.Threading.Tasks;
using Windows.Storage;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Interface for protected document consumer
    /// </summary>
    internal interface IProtectedDocumentConsumer : IDocument
    {
        /// <summary>
        /// Gets the encrypted (protected) file
        /// </summary>
        IStorageFile EncryptedFile { get; }

        /// <summary>
        /// Acquires the protected file stream, loads the policy information
        /// </summary>
        /// <returns>Protected file stream result</returns>
        Task<GetProtectedFileStreamResult> LoadAsync();
    }
}
