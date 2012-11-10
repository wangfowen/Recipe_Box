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

namespace Recipe_Box
{
    public sealed partial class MainPage : Page
    {
        public static string DraggedItem = "";
        public int FocusCount = 0;
        
        public MainPage()
        {
            this.InitializeComponent();

            //cardViewer viewer = new cardViewer();
            //this.mainGrid.Children.Add(viewer);
            //EditCard Card = new EditCard();
            //this.mainGrid.Children.Add(Card);


            /*************SETUP LOCAL STORAGE**********************/
            this.GotFocus += MainPage_GotFocus;

            this.AddCard.Click += AddCard_Click;
        }

        void AddCard_Click(object sender, RoutedEventArgs e)
        {
            // Implement adding card view here.
            throw new NotImplementedException();

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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
