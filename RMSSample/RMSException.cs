//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// RMS sharing app specific exception
    /// </summary>
    public class RMSException : Exception
    {
        /// <summary>
        /// Initializes a new instance of RMSException
        /// </summary>
        public RMSException()
            : this("An error occurred in the app. Please try again.")
        {
        }

        /// <summary>
        /// Initializes a new instance of RMSException
        /// </summary>
        /// <param name="message">Exception message</param>
        public RMSException(string message)
            : base(message)
        {
        }
    }
}
