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


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Recipe_Box
{

    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Tag> tagsList = new ObservableCollection<Tag>();
        public string DraggedTag = null;
        public TagDatabase tagDatabase = new TagDatabase();

        public MainPage()
        {
            this.InitializeComponent();

            this.TagGridView.DataContext = tagsList;
            this.CategoryGridView.DataContext = tagDatabase.CategoriesCollection();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void TitleTextBox_Drop(object sender, DragEventArgs e)
        {
            if (DraggedTag != null)
            {
                this.TitleTextBox.Text = DraggedTag;
            }
        }

        private void TagGridView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            DraggedTag = e.Items[0].ToString();
            this.TitleTextBox.Text = "dragging";
        }

        private void FilterTags(object sender, ItemClickEventArgs e)
        {
            string category = e.ClickedItem.ToString();

            tagsList = tagDatabase.TagCollectionFromCategory(category);
            this.TagGridView.DataContext = tagsList;
        }
    }
}
