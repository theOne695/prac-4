using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7
{
    public class BudgetEntry
    {
        public string Name { get; set; }
        public string TypeName { get; set; }

        public string Data;
        public int Money { get; set; }
        public bool Step { get; set; }

        public BudgetEntry(string name, string type, string data, int money, bool step)
        {
            Name = name;
            TypeName = type;
            Data = data;
            Money = Math.Abs(money);
            Step = step;
        }
    }
}
