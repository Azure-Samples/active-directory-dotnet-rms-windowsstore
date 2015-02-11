//---------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
//---------------------------------------------------------------- 

using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.RightsManagement.Apps.RMSSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProtectedTextConsumptionPage : Page
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public ProtectedTextConsumptionPage()
        {
            this.InitializeComponent();
            PermissionsViewer.IsAutoDismissEnabled = false;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.ProgressGrid.Visibility = Visibility.Visible;
                this.ProgressRing.IsActive = true;

                // ProtectedTextConsumptionPage must get a valid file object as a parameter
                if (e.Parameter != null)
                {
                    IStorageFile file = e.Parameter as IStorageFile;

                    if (file != null)
                    {
                        var userId = (App.Current as App).UserEmailId;
                        // Initiate Protected text document consumer.
                        ProtectedTextDocumentConsumer consumer = new ProtectedTextDocumentConsumer(file,
                            new ConsentManager(userId),
                            new AuthenticationManager(userId),
                            userId);
                        GetProtectedFileStreamResult result = await consumer.LoadAsync();

                        if (result.Status != GetUserPolicyResultStatus.Success)
                        {
                            throw new RMSException(String.Format("Error in reading the document. Your policy status is {0}", result.Status.ToString()));
                        }

                        this.TextContent.Text = await consumer.GetTextAsync();
                        PermissionsViewer.Policy = result.Stream.Policy;
                        PermissionsViewer.IsOpen = true;
                        this.FileNameText.Text = consumer.UnencryptedFileName;
                    }
                    else
                    {
                        throw new ArgumentException("Input file not provided.");
                    }
                }
                else
                {
                    throw new ArgumentException("Input file not provided.");
                }
            }
            catch(Exception ex)
            {
                exception = ex;                
            }
            finally
            {
                this.ProgressRing.IsActive = false;
                this.ProgressGrid.Visibility = Visibility.Collapsed;
            }

            if (exception != null)
            {
                await MessageHelper.DisplayErrorAsync(exception.Message);
            }
        }
        private void RootGridTapped(object sender, TappedRoutedEventArgs e)
        {
            PermissionsViewer.IsOpen = !PermissionsViewer.IsOpen;
        }
    }
}
