//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// File (decrypted) that has been selected by the user
        /// </summary>
        private IStorageFile _userSelectedFile;

        /// <summary>
        /// Initializes the page
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // If user clicks the file to launch this application, then that file is passed
            // as e.Parameter
            if (e.Parameter != null)
            {
                _userSelectedFile = e.Parameter as IStorageFile;
                if ((App.Current as App).UserEmailId != null)
                {
                    EmailIdText.Text = (App.Current as App).UserEmailId;
                }
            }
        }

        /// <summary>
        /// Handler for next button click.
        /// Stores the email id in the app data that is entered by the user. Also navigates to 
        /// appropriate view based on the input file type.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_userSelectedFile == null)
            {
                await MessageHelper.DisplayErrorAsync("Please open application using by clicking a file.");
            }
            else if (Utility.IsValidEmailAddress(EmailIdText.Text))
            {
                // Store this email id in the app data as well as in global user eamil id field
                (App.Current as App).UserEmailId = EmailIdText.Text.Trim();

                if (Utility.IsProtectedTextFileExtension(_userSelectedFile.FileType))
                {
                    Frame.Navigate(typeof(ProtectedTextConsumptionPage), _userSelectedFile);
                }
                else if (Utility.IsProtectedJpgFileExtension(_userSelectedFile.FileType))
                {
                    Frame.Navigate(typeof(ProtectedImageConsumptionPage), _userSelectedFile);
                }
                else if (Utility.IsProtectedGenericFileExtension(_userSelectedFile.FileType))
                {
                    Frame.Navigate(typeof(GenericProtectionConsumptionPage), _userSelectedFile);
                }
                else
                {
                    await MessageHelper.DisplayErrorAsync("Unexpected error, wrong file extension");
                }
            }
            else
            {
                await MessageHelper.DisplayErrorAsync("Please enter valid Email address.");
            }
        }
    }
}
