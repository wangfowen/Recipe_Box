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
    public sealed partial class MyUserControl1 : UserControl
    {
        public event EventHandler DoneBuilding;
        public HashSet<String> tags = new HashSet<string>();
        private int width = 0;
        private int height = 0;
        public MyUserControl1(int width, int height, CardObj CardData)
        {
            this.InitializeComponent();
            this.width = width;
            this.height = height;

            if (CardData != null)
            {
                this.LoadDataOfCard(CardData);
            }
            else
            {
                addRow();
                for (int i = 0; i < 10; ++i)
                {
                    TextBox tx = new TextBox();
                    tx.BorderThickness = new Thickness(0);
                    tx.FontSize = 16;
                    tx.FontFamily = new FontFamily("Segoe UI");
                    tx.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    tx.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    tx.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                    if (i == 0)
                    {
                        tx.Foreground = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                        tx.Text = "Enter Instructions";
                        tx.GotFocus += OnEnter;
                    }
                    else
                    {
                        tx.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                        tx.Text = "";
                    }

                    tx.Width = width;
                    tx.AllowDrop = true;
                    tx.Drop += DragEventHandler;
                    tx.DragEnter += Highlight;
                    tx.DragLeave += Unhighlight;
                    tx.KeyDown += OnPressEnter;
                    this.reversePanel.Children.Add(tx);
                }
                Button SaveButt = new Button();
                //SaveButt.BorderThickness = new Thickness(0);
                SaveButt.FontSize = 16;
                
                
                SaveButt.Visibility = Visibility.Visible;
                //SaveButt.Opacity = 5000;
                SaveButt.Foreground = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
                SaveButt.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                SaveButt.Width = 500;
                //SaveButt.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                SaveButt.Content = "Save";
                SaveButt.Click += SaveButt_Click;
                this.reversePanel.Children.Add(SaveButt);
            }

        }

        void SaveButt_Click(object sender, RoutedEventArgs e)
        {
            this.SaveDataOfCard();
            DoneBuilding(this, new EventArgs());
        }

        public bool addTag(String tag)
        {
            return tags.Add(tag);
        }

        public List<String> getTags()
        {
            return tags.ToList<String>();
        }

        public void addRow()
        {
            StackPanel sp = new StackPanel();
            sp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            sp.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            sp.Orientation = Orientation.Horizontal;
            sp.Width = this.width;
            NumberSelector numselector = new NumberSelector();
            numselector.Width = 50;
            numselector.OnChange += numselector_OnChange;
            sp.Children.Add(numselector);
            UnitSelector unitselector = new UnitSelector();
            unitselector.Width = 80;
            sp.Children.Add(unitselector);
            TextBox tx = new TextBox();
            tx.FontSize = 20;
            tx.FontFamily = new FontFamily("Segoe UI");
            tx.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));
            tx.BorderBrush = new SolidColorBrush(Color.FromArgb(0,0,0,0));
            tx.Foreground = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            tx.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            tx.Text = "Drag and drop ingredient here";
            tx.Width = width - 130;
            tx.AllowDrop = true;

            //tx.Drop += ((EditCard)this.Parent).TextBox_Drop;
            tx.Drop += DragEventHandler;
            tx.GotFocus += OnEnter1;
            tx.LostFocus += tx_LostFocus;
            tx.DragEnter += Highlight;
            tx.DragLeave += Unhighlight;
            tx.KeyDown += OnPressEnter;
            sp.Children.Add(tx);
            this.recipePanel.Children.Add(sp);
        }

        void tx_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tx = (TextBox)sender;
            StackPanel parent = (StackPanel)tx.Parent;
            finalizeAndAddRow(parent);

            if (tx.Text == "")
            {
                tx.Text = "Drag and drop ingredient here";
                tx.Foreground = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }
        }

        void numselector_OnChange(object sender, EventArgs e)
        {
            NumberSelector ns = (NumberSelector)sender;
            StackPanel parent = (StackPanel)ns.Parent;
            finalizeAndAddRow(parent);  
        }

        private void OnPressEnter(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                TextBox tx = (TextBox)sender;
                StackPanel parent = (StackPanel)tx.Parent;
                finalizeAndAddRow(parent);
            }
        }

        void finalizeAndAddRow(StackPanel parent)
        {
            NumberSelector NS = (NumberSelector)parent.Children[0];
            UnitSelector US = (UnitSelector)parent.Children[1];
            TextBox tx = (TextBox)parent.Children[2];

            if (tx.Text != "Drag and drop ingredient here" && tx.Text != "" && parent.Children.Count == 3 && parent.Orientation == Orientation.Horizontal)
            {
                if (NS.getSelectedValue() != "")
                {
                    tx.Text = NS.getSelectedValue() + " " + US.getSelectedValue() + " " + tx.Text;
                    tx.Width = width;
                    parent.Children.Clear();
                    StackPanel parentparent = (StackPanel)parent.Parent;
                    int index = parentparent.Children.IndexOf(parent);
                    parentparent.Children.Remove(parent);
                    parentparent.Children.Insert(index, tx);
                    addRow();
                }
            }
        }

        private void DragEventHandler(object sender, DragEventArgs e)
        {
            TextBox SenderBox = (TextBox)sender;

            if (SenderBox.Text == "Drag and drop ingredient here")
            {
                SenderBox.Text = "";
            }

            SenderBox.Text += " " + MainPage.DraggedItem;
            SenderBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            SenderBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            StackPanel parent = (StackPanel)SenderBox.Parent;
            finalizeAndAddRow(parent);
		}

        private void Highlight(object sender, DragEventArgs e)
        {
            TextBox SenderBox = (TextBox)sender;

            SenderBox.Background = new SolidColorBrush(Color.FromArgb(100, 116, 40, 148));
        }

        private void Unhighlight(object sender, DragEventArgs e)
        {
            TextBox SenderBox = (TextBox)sender;

            SenderBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }    

        private void OnEnter(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	TextBox tx = (TextBox)sender;
            if (tx.Text == "Enter Title" || tx.Text == "Enter Instructions")
            {
                tx.Text = "";
                tx.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }       
        }

        private void OnLeave(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	TextBox tx = (TextBox)sender;
            if (tx.Text == "")
            {
                tx.Text = "Enter Title";
                tx.Foreground = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }
        }

        private void OnEnter1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TextBox tx = (TextBox)sender;
            if (tx.Text == "Drag and drop ingredient here")
            {
                tx.Text = "";
                tx.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        public void disableEditing()
        {

            //Assume all textboxes now
            foreach (UIElement IngredientEntry in this.recipePanel.Children)
            {
                try
                {
                    TextBox EntryBox = (TextBox)IngredientEntry;
                    EntryBox.IsEnabled = false;
                }
                catch (Exception e)
                {

                }
            }

            //Assume all textboxes now
            foreach (UIElement InstructionEntry in this.reversePanel.Children)
            {
                try
                {
                    TextBox EntryBox = (TextBox)InstructionEntry;
                    EntryBox.IsEnabled = false;
                }
                catch (Exception e)
                {

                }
            }
        }
        public void enableEditing()
        {
            //Assume all textboxes now
            foreach (UIElement IngredientEntry in this.recipePanel.Children)
            {
                try
                {
                    TextBox EntryBox = (TextBox)IngredientEntry;
                    EntryBox.IsEnabled = true;
                }
                catch (Exception e)
                {

                }
            }

            //Assume all textboxes now
            foreach (UIElement InstructionEntry in this.reversePanel.Children)
            {
                 try
                 {
                    TextBox EntryBox = (TextBox)InstructionEntry;
                    EntryBox.IsEnabled = true;
                 }
                catch (Exception e)
                {

                }
            }
        }

        public async void SaveDataOfCard()
        {
            SaveAndLoad DataSaverLoader = new SaveAndLoad();
            CardObj DataToBeSaved = new CardObj();
            StackPanel Tmp_SP = new StackPanel();
            TextBox Tmp_TX = new TextBox();

            //First time loop through to delete and correct empty entries
            foreach (UIElement IngredientEntry in this.recipePanel.Children)
            {
               
                if (IngredientEntry.GetType() == Tmp_SP.GetType())
                {
                    
                    StackPanel ThisEntry = (StackPanel)IngredientEntry;
                    //this entry could be empty
                    NumberSelector NS = (NumberSelector)ThisEntry.Children[0];
                    UnitSelector US = (UnitSelector)ThisEntry.Children[1];
                    TextBox TX = (TextBox)ThisEntry.Children[2];
                    //non empty entry
                    if (NS.getSelectedValue() != "" && TX.Text.ToString() != "Drag and drop ingredient here")
                    {
                        TextBox FinalEntryBox = new TextBox();
                        FinalEntryBox.Text = NS.getSelectedValue() + " " + US.getSelectedValue() + " " + TX.Text;
                        /******INSERT TAG******/
                        DataToBeSaved.TagsList.Add(TX.Text);
                        /******INSERT TAG******/
                        FinalEntryBox.Width = width;
                        ThisEntry.Children.Clear();
                        StackPanel Card = (StackPanel)ThisEntry.Parent;
                        int index = Card.Children.IndexOf(ThisEntry);
                        Card.Children.Remove(ThisEntry);
                        Card.Children.Insert(index, FinalEntryBox);
                    }
                    //empty entry
                    else
                    {
                        ThisEntry.Children.Clear();
                        StackPanel Card = (StackPanel)ThisEntry.Parent;
                        Card.Children.Remove(ThisEntry);
                    }
                        
                    
                }
                
            }


            //All entries should be textboxes now 
            disableEditing();

            for (int i = 0; i < this.recipePanel.Children.Count; i++)
            {
                TextBox ThisEntry = (TextBox)this.recipePanel.Children[i];
                DataToBeSaved.IngredientList.Add(ThisEntry.Text.ToString());
                if (i == 0) //first entry is title add to tags
                {
                    DataToBeSaved.TagsList.Add(ThisEntry.Text.ToString());
                    DataToBeSaved.CardTitle = ThisEntry.Text.ToString();
                }
            }



            for (int i = 0; i < this.reversePanel.Children.Count; i++)
            {
                TextBox ThisEntry;
                if (this.reversePanel.Children[i].GetType() == Tmp_TX.GetType())
                {
                    ThisEntry = (TextBox)this.reversePanel.Children[i];
                    DataToBeSaved.InstructionList.Add(ThisEntry.Text.ToString());

                    if (i == 0) //first entry is title add to tags
                        DataToBeSaved.TagsList.Add(ThisEntry.Text.ToString());
                }

             
            }



            LocalAppData AllCardsCollection = await DataSaverLoader.LoadData();
            int ReplaceIndex = -1;
            for (int i = 0; i < AllCardsCollection.AllCardData.Count; i++)
            {
                if (DataToBeSaved.CardTitle == AllCardsCollection.AllCardData[i].CardTitle)
                {
                    ReplaceIndex = i;
                    break;
                }
            }

            if (ReplaceIndex != -1) //Replace
            {
                AllCardsCollection.AllCardData[ReplaceIndex] = DataToBeSaved;
            }
            else
            {
                AllCardsCollection.AddCard(DataToBeSaved);
            }
            
            
            //Create File name
            //string CardFileName = "CardFile" + CurrentCardCount.ToString();


            await DataSaverLoader.SaveData(AllCardsCollection);


        }


        public void LoadDataOfCard(CardObj CardForThisPage)
        {
            //First Read 
            //SaveAndLoad DataSaverLoader = new SaveAndLoad();
            //LocalAppData AllCardsCollection = await DataSaverLoader.LoadData();

            //CardObj CardData = AllCardsCollection.AllCardData[AllCardsCollection.CardCount - 1];

            for (int i = 0; i < CardForThisPage.IngredientList.Count; i++)
            {
                if (i == 0)
                {
                    TextBox tx = (TextBox) this.recipePanel.Children[0];
                    tx.Text = CardForThisPage.IngredientList[i].ToString();
                }
                else
                {
                    TextBox tx = new TextBox();
                    tx.BorderThickness = new Thickness(0);
                    tx.FontSize = 16;
                    tx.FontFamily = new FontFamily("Segoe UI");
                    tx.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    tx.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    tx.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                    tx.Text = CardForThisPage.IngredientList[i].ToString();
                    this.recipePanel.Children.Add(tx);
                }
               
            }


            foreach (string Instruction in CardForThisPage.InstructionList)
            {
                TextBox tx = new TextBox();
                tx.BorderThickness = new Thickness(0);
                tx.FontSize = 16;
                tx.FontFamily = new FontFamily("Segoe UI");
                tx.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                tx.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                tx.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                tx.Text = Instruction;
                this.reversePanel.Children.Add(tx);
            }

        }
	}
}
