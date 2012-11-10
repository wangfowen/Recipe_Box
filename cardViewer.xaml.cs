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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Recipe_Box
{
    public sealed partial class cardViewer : UserControl
    {
        int y1 = -1, y2 = -1;
        int front = 5;
        public cardViewer()
        {
            this.InitializeComponent();
            this.cardGrid.PointerPressed += cardGrid_PointerPressed;
            this.cardGrid.PointerExited += cardGrid_PointerReleased;
            this.cardGrid.PointerReleased += cardGrid_PointerReleased;

            for (int i = 0; i < 8; i++)
            {
                Border b = new Border();
            }
        }

        void cardGrid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            this.cardGrid.PointerMoved -= mainGrid_PointerMoved;
        }

        void cardGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.cardGrid.PointerMoved += mainGrid_PointerMoved;
        }

        void mainGrid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Point p = e.GetCurrentPoint(this).Position;
            if (y1 == -1)
            {
                y1 = (int)p.Y; 
            }
            y2 = (int)p.Y;
            if (Math.Abs(y2 - y1) >= 50)
            {
                if (y1 < y2)
                {
                    shift();
                }
                y1 = (int)p.Y;
            }
        }
        Storyboard storyBoard = new Storyboard();
        void shift()
        {
            storyBoard.SkipToFill();
            storyBoard = new Storyboard();
            front = (front + 1) % 8;
            int index = front;
            Reorder(front);
            for (int i = 0; i <8; i++)
            {
                Border element = (Border)this.cardGrid.Children[7 - i];
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.EnableDependentAnimation = true;
                doubleAnimation.From = (i + 1) * 35;
                doubleAnimation.To = i * 35;
                doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                Storyboard.SetTarget(doubleAnimation, element);
                Storyboard.SetTargetProperty(doubleAnimation, "(UIElement.Projection).(GlobalOffsetX)");
                storyBoard.Children.Add(doubleAnimation);
                DoubleAnimation doubleAnimation2 = new DoubleAnimation();
                doubleAnimation2.EnableDependentAnimation = true;
                doubleAnimation2.From = (i + 1) * -50;
                doubleAnimation2.To = i * -50;
                doubleAnimation2.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                Storyboard.SetTarget(doubleAnimation2, element);
                Storyboard.SetTargetProperty(doubleAnimation2, "(UIElement.Projection).(GlobalOffsetY)");
                storyBoard.Children.Add(doubleAnimation2);
                DoubleAnimation doubleAnimation3 = new DoubleAnimation();
                doubleAnimation3.EnableDependentAnimation = true;
                doubleAnimation3.From = (i + 1) * -35;
                doubleAnimation3.To = i * -35;
                doubleAnimation3.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                Storyboard.SetTarget(doubleAnimation3, element);
                Storyboard.SetTargetProperty(doubleAnimation3, "(UIElement.Projection).(GlobalOffsetZ)");
                storyBoard.Children.Add(doubleAnimation3);
                index = (index + 1) % 8;
            }
            try
            {
                storyBoard.Begin();
            }
            catch (Exception ex)
            {
                String x = ex.ToString();
            }
        }
        bool first = true;
        void Reorder(int front)
        {
            if (true)
            {
                UIElement[] children = { null, null, null, null, null, null, null, null };
                this.cardGrid.Children.CopyTo(children, 0);
                this.cardGrid.Children.Clear();
                for (int i = 7; i >= 0; i--)
                {
                    this.cardGrid.Children.Add(children[i]);
                }
                first = false;
            }
            UIElement oldelem;
            UIElement newelem;
            int index = front;
            newelem = this.cardGrid.Children[front];
            this.cardGrid.Children.RemoveAt(front);
            index = ((index - 1) + 7) % 7;
            for (int i = 0; i < 7; i++)
            {
                oldelem = this.cardGrid.Children[index];
                this.cardGrid.Children.RemoveAt(index);
                this.cardGrid.Children.Insert(index, newelem);
                newelem = oldelem;
                index = ((index - 1) + 7) % 7;
            }
            this.cardGrid.Children.Insert(front, newelem);
            if (true)
            {
                UIElement[] children = { null, null, null, null, null, null, null, null };
                this.cardGrid.Children.CopyTo(children, 0);
                this.cardGrid.Children.Clear();
                for (int i = 7; i >= 0; i--)
                {
                    this.cardGrid.Children.Add(children[i]);
                }
                first = false;
            }
        }
    }
}
