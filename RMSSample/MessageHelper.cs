//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// Helper class to display the different types of message box in the app.
    /// </summary>
    internal class MessageHelper
    {
        /// <summary>
        /// Display simple message box.
        /// </summary>
        /// <param name="infoMessage">Informatory message on the message box</param>
        /// <param name="handler">Handler for the button on message box if developer wants to execute specific code on its click</param>
        public static async Task<IUICommand> DisplayInfoAsync(string infoMessage, UICommandInvokedHandler handler = null)
        {
            return await DisplayMessageAsync(infoMessage, MessageType.InfoMessage, handler);
        }

        /// <summary>
        /// Display simple message box.
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="handler">Handler for the button on message box if developer wants to execute specific code on its click</param>
        public static async Task<IUICommand> DisplayErrorAsync(string error, UICommandInvokedHandler handler = null)
        {
            return await DisplayMessageAsync(error, MessageType.ErrorMessage, handler);
        }

        private static async Task<IUICommand> DisplayMessageAsync(string message, MessageType messageType, UICommandInvokedHandler handler)
        {
            MessageDialog dialog = null;

            if (messageType.Equals(MessageType.ErrorMessage))
            {
                dialog = new MessageDialog(message, "Error");
            }
            else
            {
                dialog = new MessageDialog(message, "Information");
            }

            if (handler != null)
            {
                dialog.Commands.Add(new UICommand("Close", handler));
            }

            return await dialog.ShowAsync();
        }
    }
}
