using budget_planner;

class Program
{

    // Die Main-Methode ist der Einstiegspunkt des Programms.
    static void Main()
    {

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
                    Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
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
            Console.WriteLine("=====================================");
            Console.WriteLine("=   Budget-Planer: Haushaltsbuch    =");
            Console.WriteLine("=====================================");
        }


        static void DisplayOptions()
        {
            // Optionen anzeigen in der Konsole
            Console.WriteLine();
            Console.WriteLine("Bitte wählen Sie eine Option:");
            Console.WriteLine();
            Console.Write("1. Einnahmen und Ausgaben erfassen\t");
            Console.WriteLine("2. Einnahmen und Ausgaben Übersicht");
        }

        static void ErfasseEinnahmenUndAusgaben()
        {
            Console.WriteLine();
            Console.WriteLine("Bitte Auswahl treffen:");
            Console.WriteLine();
            Console.Write("1. Einnahmen erfassen\t");
            Console.WriteLine("2. Ausgaben erfassen");


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
                        Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
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
            Console.WriteLine("1. Miete");
            Console.WriteLine("2. Lebensmittel");
            Console.WriteLine("3. Freizeit");
            Console.WriteLine("4. Transport");
            Console.WriteLine("5. Sonstiges");
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
                        Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
                        continue;
                }

                AddDescription(item);
                break;
            }
        }

        static void AddDescription(BudgetItem item)
        {
            Console.WriteLine();
            Console.WriteLine($"Bitte geben Sie eine Beschreibung für {item.Category} ein (max. 10 Zeichen):");
            Console.WriteLine();
            string description = Console.ReadLine();

            while (description.Length > 10)
            {
                Console.WriteLine("Beschreibung zu lang. Bitte geben Sie eine Beschreibung mit maximal 10 Zeichen ein:");
                description = Console.ReadLine();
            }

            item.Description = description;
            Console.WriteLine();
            Console.WriteLine($"{item.Type} für {item.Category} mit Beschreibung '{item.Description}' erfasst.");
        }
    }
}
