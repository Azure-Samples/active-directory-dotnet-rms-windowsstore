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
    public sealed partial class GenericProtectionConsumptionPage : Page
    {
        public GenericProtectionConsumptionPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Exception exception = null;

            try
            {
                ProgressGrid.Visibility = Visibility.Visible;
                ProgressRing.IsActive = true;

                // ProtectedTextConsumptionPage must get a valid file object as a parameter
                if (e.Parameter != null)
                {
                    IStorageFile file = e.Parameter as IStorageFile;

                    if (file != null)
                    {
                        var userId = (App.Current as App).UserEmailId;
                        var consumer = new ProtectedDocumentConsumer(file, new ConsentManager(userId), new AuthenticationManager(userId), userId);
                        var result = await consumer.LoadAsync();

                        if (result.Status != GetUserPolicyResultStatus.Success)
                        {
                            throw new RMSException(String.Format("In reading the document. Your policy status is {0}", result.Status.ToString()));
                        }

                        TextContent.Text = "Viewing the content of the generic file format is not supported yet.";
                        PermissionsViewer.HostingPage = this;
                        PermissionsViewer.Policy = result.Stream.Policy;
                        PermissionsViewer.IsOpen = true;
                        FileNameText.Text = consumer.UnencryptedFileName;
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
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                ProgressRing.IsActive = false;
                ProgressGrid.Visibility = Visibility.Collapsed;
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
