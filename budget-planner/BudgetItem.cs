using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace budget_planner
{
    public class BudgetItem
    {
        // Typ des BudgetItems (z.B. "Einnahme" oder "Ausgabe")
        public string Type { get; set; }

        // Kategorie des BudgetItems (z.B. "Miete", "Lebensmittel", etc.)
        public string Category { get; set; }

        // Beschreibung des BudgetItems
        public string Description { get; set; }

        // Betrag des BudgetItems
        public decimal Amount { get; set; }

        // Datum des BudgetItems
        public DateTime Date { get; set; }
    }
}
