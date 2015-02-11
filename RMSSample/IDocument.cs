//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Interface for document. This document can be unprotected or RMS protected file.
    /// </summary>
    internal interface IDocument
    {
        /// <summary>
        /// Gets the file name (without path) for encrypted document
        /// </summary>
        string EncryptedFileName { get; }

        /// <summary>
        /// Gets the file name (without path) for decrypted document
        /// </summary>
        string UnencryptedFileName { get; }
    }
}
