using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CourceWork
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        bool ButVip = false;
        bool ButFreeTable = false;
        public HomePage()
        {
            InitializeComponent();
            if (SerializedTables.tables.Count == 0)
                SerializedTables.IsDesirializeTables();
           
            TablesItems.ItemsSource = SerializedTables.tables;
        }

        public void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = (Button)sender;
            var table = SerializedTables.tables.FirstOrDefault(tabel => tabel.Id == Convert.ToInt32(button.Tag));
            SerializedTables.tables.Remove(table);
            SerializedTables.IsUpdateSerializeTables();
            SerializedTables.IsDesirializeTables();
            TablesItems.ItemsSource = SerializedTables.tables;
        }

        private void ButtonDeleteBoocked_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var id = Convert.ToInt32(((Button)sender).Tag);
            SerializedTables.mainWindow.WindowPage.NavigationService.Navigate(new TableInfo(id));
            SerializedTables.mainWindow.GroupListBox.SelectedIndex = -1;
        }
        public void InitTable(bool ButtonVip,bool ButtonFreeTabel,string SearchText)
        {
            List<Table> NewSources = new List<Table>();
            foreach(Table t in SerializedTables.tables)
            {
                if (ButtonVip)
                    if (t.Vip != System.Windows.Visibility.Visible) continue;
                if (ButtonFreeTabel)
                    if (t.BookedItems.Count > 0)
                    {
                        BookedItem x = t.BookedItems.Find(BookedItem => BookedItem.Date.ToShortDateString() == DateTime.Now.ToShortDateString());
                        if (x != null)
                        {
                            if (x.BoockedItemVisitor.Any(BoockedItemVisitor => BoockedItemVisitor.Time == (DateTime.Now.Hour.ToString() + ":00")))
                                continue;
                        }
                    }
                if (!String.IsNullOrEmpty(SearchText))
                {
                    if (!t.Name.Contains(SearchText))
                        continue;
                }
                NewSources.Add(t);
            }
            TablesItems.ItemsSource = NewSources;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitTable(ButVip, ButFreeTable, SearchTextBox.Text);
        }

        private void OnlyVipTables_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ButVip == false) ButVip = true;
            else ButVip = false;
            InitTable(ButVip, ButFreeTable, SearchTextBox.Text);
        }


        private void OnlyFreeTables_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ButFreeTable == false) ButFreeTable = true;
            else ButFreeTable = false;
            InitTable(ButVip, ButFreeTable, SearchTextBox.Text);
        }

    }
}
