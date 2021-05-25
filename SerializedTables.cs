using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CourceWork
{
    public static class SerializedTables
    {
        public static XmlSerializer tablesSerializer = new XmlSerializer(typeof(List<Table>));
        public static MainWindow mainWindow;
        public static List<Table> tables = new List<Table>();
        static string path = System.IO.Path.GetFullPath("Tables.xml");


        public static bool IsUpdateSerializeTables()
        {
            try
            {
                if (File.Exists(path))
                    File.WriteAllText(path, string.Empty);
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    tablesSerializer.Serialize(fileStream, tables);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDesirializeTables()
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    tables = (List<Table>)tablesSerializer.Deserialize(fileStream);
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
