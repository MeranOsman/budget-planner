using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budget_planner
{



    public class BudgetItem
    {
        public string Category { get; set; }
        public string Type { get; set; } // Einnahme oder Ausgabe
        public string Description { get; set; } // Beschreibung der Kategorie
    }

}
