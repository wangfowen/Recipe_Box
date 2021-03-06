﻿using System;
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
        public static LocalAppData AllData = new LocalAppData();
        public static List<MyUserControl1> AllCards = new List<MyUserControl1>();
        public int CurrentViewingCard = -1;
        
        public MainPage()
        {
            this.InitializeComponent();
            
            //cardViewer viewer = new cardViewer();
            //this.mainGrid.Children.Add(viewer);


            /*************SETUP LOCAL STORAGE**********************/
            
            this.Loaded += MainPage_Loaded;

            this.AddCard.Click += AddCard_Click;
            this.ViewCard.Click += ViewCard_Click;
        }

        void ViewCard_Click(object sender, RoutedEventArgs e)
        {
            MyUserControl1 card = this.cardCarousel.releaseFrontCard();
            card.PointerPressed += card_PointerReleased;
            this.cardCarousel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.mainGrid.Children.Add(card);
        }

        void card_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as MyUserControl1).PointerPressed -= card_PointerReleased;
            this.mainGrid.Children.Remove((sender as MyUserControl1));
            this.cardCarousel.returnFrontCard((sender as MyUserControl1));
            this.cardCarousel.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (FocusCount == 0)
            {
                SaveAndLoad DataSaverLoader = new SaveAndLoad();
                await DataSaverLoader.CreateAppLocalData();

                AllData = await DataSaverLoader.LoadData();

                foreach (CardObj Card in AllData.AllCardData)
                {
                    MyUserControl1 control = new MyUserControl1(500, 300, Card);
                    control.Width = 600;
                    control.Height = 400;
                    control.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    control.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                    control.disableEditing();
                    MainPage.AllCards.Add(control);
                }
                this.cardCarousel.setup();
            }
            FocusCount++;
        }

        void AddCard_Click(object sender, RoutedEventArgs e)
        {
            // Implement adding card view here.
            //throw new NotImplementedException();
            EditCard Card = new EditCard(null);
            this.mainGrid.Children.Add(Card);
            Card.DoneEditing += Card_DoneEditing;
            this.cardCarousel.Visibility = Visibility.Collapsed;
        }

        void Card_DoneEditing(object sender, EventArgs e)
        {
            this.cardCarousel.Visibility = Visibility.Visible;
            this.mainGrid.Children.Remove((EditCard)sender);
        }
    
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
