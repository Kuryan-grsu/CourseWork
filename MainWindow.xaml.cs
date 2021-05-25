using System.Windows;
using System.Windows.Controls;

namespace CourceWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SerializedTables.mainWindow = this;
            GroupListBox.SelectedIndex = 0;
            if (!SerializedTables.IsDesirializeTables())
            {
                MessageBox.Show("Не удалось загрузить список столов!");
            }

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GroupListBox.SelectedIndex)
            {
                case 0:
                    WindowPage.NavigationService.Navigate(new HomePage());
                    break;
                case 1:
                    WindowPage.NavigationService.Navigate(new AddedTable());
                    break;
            }
        }
    }
}
