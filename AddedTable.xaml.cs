using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourceWork
{
    /// <summary>
    /// Interaction logic for AddedTable.xaml
    /// </summary>
    public partial class AddedTable : Page
    {
        public AddedTable()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(NameTable.Text) && !String.IsNullOrWhiteSpace(CountTable.Text))
            {
                int id = SerializedTables.tables.Count == 0 ? 1 : SerializedTables.tables.Last().Id + 1;
                Table table = new Table()
                {
                    Id = id,
                    CountPlaces = Convert.ToInt32(CountTable.Text),
                    Name = NameTable.Text,
                    StatusTable = StatusTable.Free,
                    Vip = Vip.IsChecked == true ? Visibility.Visible : Visibility.Hidden,
                    Brush = Vip.IsChecked == true ? "#FFD700" : "#00FF00",
                    BoockedButton = Visibility.Hidden

                };
                SerializedTables.tables.Add(table);
                SerializedTables.IsUpdateSerializeTables();

                NameTable.Text = string.Empty;
                CountTable.Text = string.Empty;
                Vip.IsChecked = false;
                MessageBox.Show("Стол добавлен");
            }
        }
        private void CountTable_TextInputUpdate(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
