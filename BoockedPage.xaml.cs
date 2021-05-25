using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CourceWork
{
    /// <summary>
    /// Interaction logic for BoockedPage.xaml
    /// </summary>
    public partial class BoockedPage : Page
    {
        int _id;
        DateTime _date;
        string _time;
        Table t;

        public BoockedPage(int idTable, DateTime date, string time)
        {
            InitializeComponent();
            _id = idTable;
            _date = date;
            _time = time;
            t = SerializedTables.tables.FirstOrDefault(id => id.Id == idTable);
            Table.Text = t.Name;
            Time.Text = "   На дату: " + date.ToShortDateString() + " " + time;
        }
        private void endfunc()
        {
            SerializedTables.mainWindow.WindowPage.NavigationService.Navigate(new TableInfo(_id));
            SerializedTables.mainWindow.GroupListBox.SelectedIndex = -1;
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NameInput.Text) || String.IsNullOrWhiteSpace(ContactsInput.Text))
                return;
            if (t.BookedItems.Any(b => b.Date == _date))
            {
                BoockedItemVisitor boockedItemVisitor = new BoockedItemVisitor()
                {
                    Time = _time,
                    Visitor = new VisitorInfoBooked()
                    {
                        VisitorName = NameInput.Text,
                        Contacts = ContactsInput.Text
                    }
                };
                t.BookedItems.FirstOrDefault(b => b.Date == _date).BoockedItemVisitor.Add(boockedItemVisitor);
                SerializedTables.IsUpdateSerializeTables();
                endfunc();
            }
            else
            {
                BookedItem bookedItem = new BookedItem()
                {
                    Date = _date,
                    BoockedItemVisitor = new List<BoockedItemVisitor>()
                    {
                        new BoockedItemVisitor()
                        {
                            Time = _time,
                            Visitor = new VisitorInfoBooked()
                            {
                                VisitorName = NameInput.Text,
                                Contacts = ContactsInput.Text
                            }
                        }
                    }
                };
                t.BookedItems.Add(bookedItem);
                SerializedTables.IsUpdateSerializeTables();
                endfunc();
            }
        }
    }
}
