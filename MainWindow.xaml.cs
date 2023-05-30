using Newtonsoft.Json;
using System;
using System.Collections;
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

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        private List<BudgetEntry> budgetEntries = new List<BudgetEntry>();
        private List<string> Typeeee = new List<string>();
        private string currentDate;

        public MainWindow()
        {
            InitializeComponent();
            currentDate = DatePicker.Text;
            DatePicker.SelectedDate = DateTime.Now.Date;
            LoadData();
            UpdateDataGrid();
            LoadTL();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string nameValue = TextBox1.Text;
            string tipValue = combobox.Text;
            int moneyValue = int.Parse(TextBox2.Text);
            bool actionValue = moneyValue < 0;
            moneyValue = Math.Abs(moneyValue);

            var zam = new BudgetEntry(nameValue, tipValue, currentDate, moneyValue, actionValue);
            budgetEntries.Add(zam);
            SaveData();
            UpdateDataGrid();

            TextBox1.Text = "";
            TextBox2.Text = "";
            combobox.Text = "";
        }

        private void updateButton_click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is BudgetEntry selectedZametka)
            {
                string nameValue = TextBox1.Text;
                int moneyValue = Convert.ToInt32(TextBox2.Text);
                string type = combobox.Text;
                int selectedIndex = budgetEntries.FindIndex(z => z == selectedZametka);
                selectedZametka.Name = nameValue;
                selectedZametka.Money = moneyValue;
                selectedZametka.TypeName = type;

                if (moneyValue < 0)
                {
                    selectedZametka.Step = true;
                    selectedZametka.Money = Math.Abs(moneyValue);
                }
                else
                {
                    selectedZametka.Step = false;
                }

                budgetEntries[selectedIndex] = selectedZametka;
                SaveData();
                UpdateDataGrid();

                TextBox1.Text = "";
                TextBox2.Text = "";
                combobox.Text = "";
                DataGrid.SelectedIndex = -1;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is BudgetEntry selectedZametka)
            {
                budgetEntries.Remove(selectedZametka);
                SaveData();
                UpdateDataGrid();

                TextBox1.Text = "";
                TextBox2.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            add ADD = new add();
            ADD.Closed += CloseD;
            ADD.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem is BudgetEntry selectedZametka)
            {
                TextBox1.Text = selectedZametka.Name;
                if (selectedZametka.Step)
                {
                    TextBox2.Text = "-" + selectedZametka.Money.ToString();
                }
                else
                {
                    TextBox2.Text = selectedZametka.Money.ToString();
                }
                combobox.Text = selectedZametka.TypeName;
            }
            else
            {
                TextBox1.Text = "";
                TextBox2.Text = "";
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDate = DatePicker.Text;
            UpdateDataGrid();
        }

        private void LoadData()
        {
            budgetEntries = konvert.Deserialization<List<BudgetEntry>>("pract4.json");
        }

        private void UpdateDataGrid()
        {
            var filteredZametki = budgetEntries.Where(z => z.Data == currentDate).ToList();
            DataGrid.ItemsSource = filteredZametki;
            Calculate();
        }

        private void LoadTL()
        {
            Typeeee = konvert.Deserialization<List<string>>("TypeName.json");
            combobox.ItemsSource = Typeeee;
        }

        private void Calculate()
        {
            int total = 0;
            foreach (BudgetEntry zametka in budgetEntries)
            {
                if (zametka.Step == false)
                {
                    total += zametka.Money;
                }
                if (zametka.Step == true)
                {
                    total -= zametka.Money;
                }
            }
            txtBlock.Text = "Итог: " + total.ToString();
        }

        private void CloseD(object sender, EventArgs e)
        {
            LoadTL();
        }

        private void SaveData()
        {
            konvert.Serialization(budgetEntries, "pract4.json");
        }
    }
}