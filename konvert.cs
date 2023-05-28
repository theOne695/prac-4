using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7
{
    internal class konvert
    {
        public static void Serialization<nn>(nn budgetEntries, string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = JsonConvert.SerializeObject(budgetEntries);
            File.WriteAllText(path + "\\" + filename, json);
        }

        public static nn Deserialization<nn>(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = File.ReadAllText(path + "\\" + filename);
            nn allzam = JsonConvert.DeserializeObject<nn>(json);
            return allzam;
        }
    }
}
