using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Recipe_Box
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CardMainPage : Page
    {
        int FocusCount = 0;
        public CardMainPage()
        {
            this.InitializeComponent();

            this.GotFocus += MainPage_GotFocus;
            MyUserControl1 control = new MyUserControl1(600, 400);
            control.Width = 600;
            control.Height = 400;
            control.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            control.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            this.MainGrid.Children.Add(control);
        }

        private async void MainPage_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FocusCount == 0)
            {
                SaveAndLoad DataSaverLoader = new SaveAndLoad();
                await DataSaverLoader.CreateAppLocalData();
            }
            FocusCount++;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
