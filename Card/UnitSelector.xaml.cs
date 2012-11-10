using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Recipe_Box
{
    public sealed partial class UnitSelector : UserControl
    {
        public UnitSelector()
        {
            this.InitializeComponent();
            this.CarouselHost.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.CarouselHost.Items.Add("");
            this.CarouselHost.SelectedIndex = 0;
            this.CarouselHost.Items.Add("cup");
            this.CarouselHost.Items.Add("feet");
            this.CarouselHost.Items.Add("gram");
            this.CarouselHost.Items.Add("inch");
            this.CarouselHost.Items.Add("meter");
            this.CarouselHost.Items.Add("ounce");
            this.CarouselHost.Items.Add("pinch");
            this.CarouselHost.Items.Add("pound");
            this.CarouselHost.Items.Add("slice");
            this.CarouselHost.Items.Add("tbsp");
            this.CarouselHost.Items.Add("tsp");
            this.CarouselHost.Visibility = Windows.UI.Xaml.Visibility.Visible;
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
    }
}
