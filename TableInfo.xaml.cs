using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourceWork
{
    /// <summary>
    /// Interaction logic for TableInfo.xaml
    /// </summary>
    public partial class TableInfo : Page
    {
        int _idTable;
        public TableInfo(int idTable)
        {
            InitializeComponent();
            _idTable = idTable;
            DatePickerControl.SelectedDate = DateTime.Now;
            var table = SerializedTables.tables.FirstOrDefault(tab => tab.Id == idTable);
            TableInfoItems.ItemsSource = new List<Table>() { table };
        }
        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = (Button)sender;
            var table = SerializedTables.tables.FirstOrDefault(tab => tab.Id == Convert.ToInt32(button.Tag));
            SerializedTables.tables.Remove(table);
            SerializedTables.IsUpdateSerializeTables();
            SerializedTables.mainWindow.GroupListBox.SelectedIndex = 0;
        }
        private List<string> Times = new List<string>()
        {
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00",
            "19:00",
            "20:00",
            "21:00",
            "22:00",
            "23:00"
        };

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime date = Convert.ToDateTime(((DatePicker)sender).SelectedDate);
                TimeBoocked.Children.Clear();
                BookedItem brockedTime = null;
                brockedTime = SerializedTables.tables.FirstOrDefault(tab => tab.Id == _idTable).BookedItems.FirstOrDefault(bdat => bdat.Date == date);
                foreach (var timeItem in Times)
                {
                    if (brockedTime != null && brockedTime.BoockedItemVisitor.Any(t => t.Time == timeItem))
                    {
                        StackPanel stackPanel = new StackPanel()
                        {
                            Orientation = Orientation.Horizontal
                        };
                        TextBlock textBlock = new TextBlock()
                        {
                            Foreground = Brushes.Black,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Text = timeItem
                        };
                        Button button = new Button()
                        {
                            Content = "Удалить бронь",
                            Margin = new Thickness(50, 0, 0, 0),
                            Background = Brushes.Red,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            Tag = timeItem
                        };
                        button.Click += UnBoockedTime;


                        var visitorInfo = brockedTime.BoockedItemVisitor.FirstOrDefault(t => t.Time == timeItem).Visitor;

                        TextBlock textBlockInfo = new TextBlock()
                        {
                            Margin = new Thickness(50, 0, 0, 0),
                            Foreground = Brushes.Black,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Text = $"Столик забронирован. Имя: {visitorInfo.VisitorName}, Контакты: {visitorInfo.Contacts}"
                        };


                        stackPanel.Children.Add(textBlock);
                        stackPanel.Children.Add(button);
                        stackPanel.Children.Add(textBlockInfo);



                        Border border = new Border()
                        {
                            Background = Brushes.White,
                            BorderBrush = Brushes.Red,
                            BorderThickness = new Thickness(2, 2, 2, 2),
                            CornerRadius = new CornerRadius(5),
                            Padding = new Thickness(5)
                        };
                        border.Child = stackPanel;
                        TimeBoocked.Children.Add(border);
                    }
                    else
                    {
                        StackPanel stackPanel = new StackPanel()
                        {
                            Orientation = Orientation.Horizontal
                        };
                        TextBlock textBlock = new TextBlock()
                        {
                            Foreground = Brushes.Black,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Text = timeItem
                        };
                        Button button = new Button()
                        {
                            Content = "Забронировать",
                            Margin = new Thickness(50, 0, 0, 0),
                            HorizontalAlignment = HorizontalAlignment.Right,
                            Tag = timeItem
                        };
                        button.Click += BoockedTime;

                        stackPanel.Children.Add(textBlock);
                        stackPanel.Children.Add(button);
                        Border border = new Border()
                        {
                            Background = Brushes.White,
                            BorderBrush = Brushes.Green,
                            BorderThickness = new Thickness(2, 2, 2, 2),
                            CornerRadius = new CornerRadius(5),
                            Padding = new Thickness(5)
                        };
                        border.Child = stackPanel;
                        TimeBoocked.Children.Add(border);
                    }
                }
            }
            catch
            {

                SerializedTables.mainWindow.GroupListBox.SelectedIndex = 0;
            }
        }

        private void UnBoockedTime(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var boocked = SerializedTables.tables.FirstOrDefault(id => id.Id == _idTable).BookedItems.FirstOrDefault(d => d.Date == Convert.ToDateTime(DatePickerControl.SelectedDate)).BoockedItemVisitor.FirstOrDefault(t => t.Time == button.Tag.ToString());
            SerializedTables.tables.FirstOrDefault(id => id.Id == _idTable).BookedItems.FirstOrDefault(d => d.Date == Convert.ToDateTime(DatePickerControl.SelectedDate)).BoockedItemVisitor.Remove(boocked);
            SerializedTables.mainWindow.WindowPage.NavigationService.Navigate(new TableInfo(_idTable));
            SerializedTables.mainWindow.GroupListBox.SelectedIndex = -1;
        }

        private void BoockedTime(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            DateTime date = Convert.ToDateTime(DatePickerControl.SelectedDate);
            SerializedTables.mainWindow.WindowPage.NavigationService.Navigate(new BoockedPage(_idTable, date, button.Tag.ToString()));
            SerializedTables.mainWindow.GroupListBox.SelectedIndex = -1;
        }
    }
}
