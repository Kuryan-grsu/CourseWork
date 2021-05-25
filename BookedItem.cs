using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceWork
{
    [Serializable]
    public class BookedItem
    {
        public BookedItem()
        {

        }

        public DateTime Date { get; set; }
        public List<BoockedItemVisitor> BoockedItemVisitor { get; set; } = new List<BoockedItemVisitor>();
    }
    [Serializable]
    public class BoockedItemVisitor
    {
        public BoockedItemVisitor()
        {

        }
        public string Time { get; set; }
        public VisitorInfoBooked Visitor { get; set; }
    }
}