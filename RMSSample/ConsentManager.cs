//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    internal class ConsentManager : IConsentCallback
    {

        /// <summary>
        /// Email of the user.
        /// </summary>
        private string userId;

        public ConsentManager(string userId)
        {
            this.userId = userId;
        }

        public IAsyncOperation<IEnumerable<IConsent>> ConsentsAsync(IEnumerable<IConsent> consents)
        {
            var newList = new List<IConsent>(consents);
            // Should contain UI Element to request user consent for URL access, currently approves consent requests. 
            foreach (var consent in newList)
            {    
                consent.Result = new ConsentResult(true, false, this.userId);
            }
            return Task.FromResult(newList as IEnumerable<IConsent>).AsAsyncOperation();
        }
    }
}
