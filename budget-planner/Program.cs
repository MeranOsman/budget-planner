using System.Text;
using budget_planner;

class Program
{

    // Die Main-Methode ist der Einstiegspunkt des Programms.
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Setzt den Titel des Konsolenfensters
        Console.Title = "Budget-Planer - Haushaltsbuch";

        // Ruft die Methode auf, die die Überschrift anzeigt
        DisplayHeader();

        // Zeigt die ersten Optionen zur Auswahl an
        DisplayOptions();

        while (true)
        {
            // Liest die Auswahl des Benutzers
            string choice = Console.ReadLine();

            // Verarbeitet die Auswahl
            switch (choice)
            {
                case "1":
                    DisplayHeader();
                    ErfasseEinnahmenUndAusgaben();
                    break;
                case "2":
                    DisplayHeader();
                    ZeigeEinnahmenUndAusgaben();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
                    Console.ResetColor();
                    continue; // Geht zurück zum Anfang der Schleife
            }

            break; // Beendet die Schleife, wenn eine gültige Auswahl getroffen wurde
        }


        Console.ReadLine(); // Damit die Konsole geöffnet bleibt

        static void DisplayHeader()
        {
            // Löscht den Bildschirm
            Console.Clear();

            // Zeigt die Überschrift im Konsolenfenster an
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=====================================");
            Console.WriteLine("=   Budget-Planer: Haushaltsbuch    =");
            Console.WriteLine("=====================================");
            Console.ResetColor();
        }


        static void DisplayOptions()
        {
            // Optionen anzeigen in der Konsole
            Console.WriteLine();
            Console.WriteLine("Bitte wählen Sie eine Option:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. Einnahmen und Ausgaben erfassen\t");
            Console.WriteLine("2. Einnahmen und Ausgaben Übersicht");
            Console.ResetColor();
        }

        static void ErfasseEinnahmenUndAusgaben()
        {
            Console.WriteLine();
            Console.WriteLine("Bitte Auswahl treffen:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. Einnahmen erfassen\t");
            Console.WriteLine("2. Ausgaben erfassen");
            Console.ResetColor();

            while (true)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayHeader();
                        SelectCategory(1);
                        break;
                    case "2":
                        DisplayHeader();
                        SelectCategory(2);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
                        Console.ResetColor();
                        continue;
                }
                break;
            }
        }


        static void ZeigeEinnahmenUndAusgaben()
        {
            Console.WriteLine("Einnahmen und Ausgaben anzeigen...");
        }


        static void SelectCategory(int type)
        {
            Console.WriteLine();
            if (type == 1)
            {
                Console.WriteLine("Bitte wählen Sie eine Kategorie für Einnahme:");
            }
            else
            {
                Console.WriteLine("Bitte wählen Sie eine Kategorie für Ausgabe:");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Miete");
            Console.WriteLine("2. Lebensmittel");
            Console.WriteLine("3. Freizeit");
            Console.WriteLine("4. Transport");
            Console.WriteLine("5. Sonstiges");
            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
                string choice = Console.ReadLine();
                BudgetItem item = new BudgetItem
                {
                    Type = type == 1 ? "Einnahme" : "Ausgabe"
                };

                switch (choice)
                {
                    case "1":
                        item.Category = "Miete";
                        break;
                    case "2":
                        item.Category = "Lebensmittel";
                        break;
                    case "3":
                        item.Category = "Freizeit";
                        break;
                    case "4":
                        item.Category = "Transport";
                        break;
                    case "5":
                        item.Category = "Sonstiges";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
                        Console.ResetColor();
                        continue;
                }

                AddDescription(item);
                break;
            }
        }

        static void AddDescription(BudgetItem item)
        {
            Console.WriteLine();
            Console.WriteLine($"Bitte geben Sie eine Beschreibung für {item.Category} ein (3-10 Zeichen):");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            string description = Console.ReadLine();
            Console.ResetColor();

            while (description.Length < 3 || description.Length > 10)
            {
                if (description.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Beschreibung zu kurz. Bitte geben Sie eine Beschreibung mit mindestens 3 Zeichen ein:");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Beschreibung zu lang. Bitte geben Sie eine Beschreibung mit maximal 10 Zeichen ein:");
                    Console.ResetColor();
                }
                description = Console.ReadLine();
            }

            item.Description = description;
            Console.WriteLine();
            AddAmount(item);
        }


        static void AddAmount(BudgetItem item)
        {
            Console.WriteLine();
            Console.WriteLine($"Bitte geben Sie den Betrag ohne '€ - Zeichen' für {item.Type} ein:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            string amountInput = Console.ReadLine();
            Console.ResetColor();
            decimal amount;


            while (!decimal.TryParse(amountInput, out amount))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ungültiger Betrag. Bitte geben Sie eine gültige Zahl ein:");
                Console.ResetColor();
                amountInput = Console.ReadLine();
            }

            item.Amount = amount;
            Console.WriteLine();
            AddDate(item);
        }


        static void AddDate(BudgetItem item)
        {
            Console.WriteLine();
            Console.WriteLine($"Bitte geben Sie das Datum für {item.Type} ein (Format: Tag.Monat.Jahr):");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            string dateInput = Console.ReadLine();
            Console.ResetColor();
            DateTime date;

            while (!DateTime.TryParseExact(dateInput, "d.M.yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ungültiges Datum. Bitte geben Sie ein gültiges Datum im Format Tag.Monat.Jahr ein:");
                Console.ResetColor();
                dateInput = Console.ReadLine();
            }

            item.Date = date;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{item.Type} für {item.Category} mit Beschreibung: {item.Description}, Betrag: {item.Amount}€ und Datum: {item.Date.ToString("d.M.yyyy")} erfasst.");
            Console.ResetColor();
        }
    }
}
