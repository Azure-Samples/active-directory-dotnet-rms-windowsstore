//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Handles the user authentication part, see  Microsoft.IdentityModel.Clients.ActiveDirectory library documentation.
    /// </summary>
    internal class AuthenticationManager : IAuthenticationCallback
    {
        /// <summary>
        /// User id or email id of the user
        /// </summary>
        protected string userId { get; private set; }

        /// <summary>
        /// Authentication Manager C'tor.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        public AuthenticationManager(string userId)
        {
            this.userId = userId; 
        }
        /// <summary>
        /// Authenticates the user and gets the security token
        /// </summary>
        /// <param name="authenticationParameters">Authentication parameters such as authority, resource etc.</param>
        /// <returns>Security token (Access token)</returns>
        public IAsyncOperation<string> GetTokenAsync(Microsoft.RightsManagement.AuthenticationParameters authenticationParameters)
        {
            return this.GetTokenInternalAsync(authenticationParameters).AsAsyncOperation();
        }

        /// <summary>
        /// Internal method for GetTokenAsync
        /// </summary>
        /// <param name="parameters">Authentication parameters such as authority, resource etc.</param>
        /// <returns>Security token (Access token)</returns>
        private async Task<string> GetTokenInternalAsync(Microsoft.RightsManagement.AuthenticationParameters parameters)
        {
            Task<AuthenticationResult> authenticationResultTask = null;

            // As authenticating the user involves UI, we need to do it on the UI thread
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    var authenticationContext = new AuthenticationContext(parameters.Authority, false);

                    // This requires filling in by the developer, using 
                    var clientId = "some-client-id"; // Requires generation once per application. 
                    var redirectionUrl = "some-redirection-url" + "://authorize"; // Requires generation once per application. 

                    authenticationResultTask = authenticationContext.AcquireTokenAsync(
                        parameters.Resource,
                        clientId,
                        new Uri(redirectionUrl),
                        PromptBehavior.Auto
                        ).AsTask();
                });
            // Wait until the authentication is finished and return the result
            var authenticationResult = await authenticationResultTask;

            if (authenticationResult.Status != AuthenticationStatus.Success)
            {
                switch (authenticationResult.Error)
                {
                    // Handle user canceling the authentication
                    case Constants.AuthenticationCanceledErrorCode:
                        throw new OperationCanceledException("Authentication was canceled by the user.");

                    default:
                        throw new Exception(
                            String.Format(
                            "ADAL authentication failed. Error: {0} ErrorDescription: {1}", 
                            authenticationResult.Error, 
                            authenticationResult.ErrorDescription)
                            );
                }               
            }
            
            return authenticationResult.AccessToken;
        }
    }
}
