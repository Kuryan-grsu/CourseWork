using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CourceWork
{
    [Serializable]
    public class Table
    {
        public Table()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusTable StatusTable { get; set; }
        public Visibility BoockedButton { get; set; }

        public List<BookedItem> BookedItems { get; set; } = new List<BookedItem>();
        public int CountPlaces { get; set; }
        public Visibility Vip { get; set; }

        public string Brush { get; set; }
    }

}
