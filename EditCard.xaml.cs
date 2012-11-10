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
using System.Collections.ObjectModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Recipe_Box
{
    public sealed partial class EditCard : UserControl
    {
        private static ObservableCollection<Tag> tagsList;
        private static ObservableCollection<string> categoriesList;
        private static TagDatabase tagDatabase;
        private static string DraggedTag = null;
        public static Boolean reversed = false;

        public EditCard()
        {
            tagsList = new ObservableCollection<Tag>();
            categoriesList = new ObservableCollection<string>();
            //TODO: load database from memory
            tagDatabase = new TagDatabase();

            this.InitializeComponent();

            categoriesList = tagDatabase.CategoriesCollection();

            this.TagView.DataContext = tagsList;
            this.CategoryView.DataContext = categoriesList;



            /*****NEW STUFF BINDING CARDS AND DRAG AND DROP TOGETHER****/
            MyUserControl1 control = new MyUserControl1(600, 400);
            control.Width = 600;
            control.Height = 400;
            control.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            control.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            
            this.LeftPanel.Children.Add(control);
            
            
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            if (DraggedTag != null)
            {
                TextBox SendBox = (TextBox)sender;
            }
        }

        private void TagView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            DraggedTag = e.Items[0].ToString();
            MainPage.DraggedItem = e.Items[0].ToString();
            //this.TitleTextBox.Text = "dragging";
        }

        private void FilterTags(object sender, ItemClickEventArgs e)
        {
            string category = e.ClickedItem.ToString();

            tagsList = tagDatabase.TagCollectionFromCategory(category);
            this.TagView.DataContext = tagsList;

            this.TagCategory.Text = category;

            this.CategoryPanel.Visibility = Visibility.Collapsed;
            this.TagPanel.Visibility = Visibility.Visible;
        }

        private void AddTag(object sender, RoutedEventArgs e)
        {
            string parsed_entry = this.TagTextBox.Text;
            string category = this.TagCategory.Text;

            if (parsed_entry.Trim() != "")
            {
                tagDatabase.AddTag(parsed_entry, category);
                //this should only exist for the session, doesn't matter what the category is
                tagsList.Add(new Tag(parsed_entry, -1));
            }

            this.TagTextBox.Text = "";
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            string parsed_entry = this.CategoryTextBox.Text;

            if ((parsed_entry.Trim() != "") && (!tagDatabase.HasCategory(parsed_entry)))
            {
                tagDatabase.AddCategory(parsed_entry);
                categoriesList.Add(parsed_entry);
            }
            else
            {
                //notify category already exists
            }

            this.CategoryTextBox.Text = "";
        }

        private void ClearBox(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            box.Text = "";
        }

        private void SubmitTagForm(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                AddTag(sender, e);
            }
        }

        private void SubmitCategoryForm(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                AddCategory(sender, e);
            }
        }

        private void ShowCategoryPanel(object sender, RoutedEventArgs e)
        {
            this.CategoryPanel.Visibility = Visibility.Visible;
            this.TagPanel.Visibility = Visibility.Collapsed;
        }
        
        //public string 


        //public void Flip()
        //{
        //    if (!reversed)
        //    {
        //        Storyboard sbFlip = Resources["sbFlip"];
        //        sbFlip.Begin();
        //        reversed = true;
        //    }
        //    else
        //    {
        //        reversed = false;
        //        Storyboard sbReverse = Resources["sbReverse"];
        //        sbReverse.Begin();
        //    }
        //}
    }
}
