//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

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
        private IStorageFile userSelectedFile = null;

        /// <summary>
        /// Initializes the page
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            
//            var localSettings = ApplicationData.Current.LocalSettings;
//            var container = localSettings.CreateContainer("MSIPCThin", ApplicationDataCreateDisposition.Always);
//            container.Values["ServiceRootURLOverride"] = "https://api.hostedrms.com" + "/my/v1";
//            container.Values["TESTHOOK_IsCacheLookupDisabled"] = true;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // If user clicks the file to launch this application, then that file is passed
            // as e.Parameter
            if (e.Parameter != null)
            {
                this.userSelectedFile = e.Parameter as IStorageFile;

                if (this.userSelectedFile == null)
                {
                    // TODO throw error
                }

                if ((App.Current as App).UserEmailId != null)
                {
                    this.EmailIdText.Text = (App.Current as App).UserEmailId;
                }
            }

            // User might have launched the application through tiles without selecting any file.
            // TODO: Show default message screen to the user           
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
            if (Utility.IsValidEmailAddress(EmailIdText.Text))
            {
                // Store this email id in the app data as well as in global user eamil id field
                (App.Current as App).UserEmailId = this.EmailIdText.Text.Trim();

                if (Utility.IsProtectedTextFileExtension(this.userSelectedFile.FileType))
                {
                    this.Frame.Navigate(typeof(ProtectedTextConsumptionPage), this.userSelectedFile);
                }
                else if (Utility.IsProtectedJpgFileExtension(this.userSelectedFile.FileType))
                {
                    this.Frame.Navigate(typeof(ProtectedImageConsumptionPage), this.userSelectedFile);
                }
                else if (Utility.IsProtectedGenericFileExtension(this.userSelectedFile.FileType))
                {
                    this.Frame.Navigate(typeof(GenericProtectionConsumptionPage), this.userSelectedFile);
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
