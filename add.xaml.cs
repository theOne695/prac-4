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
using System.Windows.Shapes;

namespace WpfApp7
{
    public partial class add : Window
    {
        private List<string> Bydget = new List<string>();

        public add()
        {
            InitializeComponent();
            Bydget = konvert.Deserialization<List<string>>("типзаписи.json");
        }

        private void addd_Click(object sender, RoutedEventArgs e)
        {
            Bydget.Add(addadd.Text);
            konvert.Serialization(Bydget, "типзаписи.json");
            this.Close();
        }
    }
}
