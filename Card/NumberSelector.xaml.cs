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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace recipecards
{
    public sealed partial class NumberSelector : UserControl
    {
        public event EventHandler OnChange;
        private int changed = 0;
        public NumberSelector()
        {
            this.InitializeComponent();
            this.CarouselHost.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            for (int i = 1000; i >= 500; i -= 50) {
                this.CarouselHost.Items.Add(i);
            }
            for (int i = 500; i >= 100; i -= 25)
            {
                this.CarouselHost.Items.Add(i);
            }
            for (int i = 100; i >= 1; i -= 1)
            {
                this.CarouselHost.Items.Add(i);
            }
            this.CarouselHost.Items.Add("");
            this.CarouselHost.SelectedIndex = this.CarouselHost.Items.Count - 1;
            this.CarouselHost.Items.Add("7/8");
            this.CarouselHost.Items.Add("3/4");
            this.CarouselHost.Items.Add("2/3");
            this.CarouselHost.Items.Add("5/8");
            this.CarouselHost.Items.Add("1/2");
            this.CarouselHost.Items.Add("3/8");
            this.CarouselHost.Items.Add("1/3");
            this.CarouselHost.Items.Add("1/4");
            this.CarouselHost.Items.Add("1/8");
            this.CarouselHost.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.CarouselHost.SelectionChanged += CarouselHost_SelectionChanged;
            this.CarouselHost.PointerEntered += CarouselHost_PointerEntered;
        }

        public String getSelectedValue()
        {
            return this.CarouselHost.SelectedItem.ToString();
        }

        void CarouselHost_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.CarouselHost.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        void CarouselHost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (changed == 0)
            {
                OnChange(this, new EventArgs());
            }
            changed++;
        }
    }
}
