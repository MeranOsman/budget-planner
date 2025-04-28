using System.Text;
using budget_planner;
using System.Text.Json;
using System.IO;

class Program
{
    // Die Main-Methode ist der Einstiegspunkt des Programms.
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Setzt den Titel des Konsolenfensters
        Console.Title = "Budget-Planer: Haushaltsbuch";

        // Ruft die Methode auf, die die Überschrift anzeigt
        DisplayHeader();

        // Zeigt die ersten Optionen zur Auswahl an
        DisplayOptions();

        while (true) // Startet eine Endlosschleife
        {
            // Liest die Auswahl des Benutzers
            string choice = Console.ReadLine();

            // Verarbeitet die Auswahl
            switch (choice)
            {
                case "1":
                    DisplayHeader(); // Zeigt den Header an
                    RecordIncomeAndExpenses(); // Funktion zum Aufzeichnen von Einnahmen und Ausgaben
                    break;
                case "2":
                    DisplayHeader(); // Zeigt den Header an
                    ShowIncomeAndExpenses(); // Funktion zum Anzeigen von Einnahmen und Ausgaben
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                    Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut."); // Gibt eine Fehlermeldung aus
                    Console.ResetColor(); // Setzt die Textfarbe zurück
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
            Console.WriteLine();
            Console.ResetColor();
        }

        static void RecordIncomeAndExpenses()
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine("Bitte Auswahl treffen:"); // Fordert den Benutzer auf, eine Auswahl zu treffen
            Console.WriteLine(); // Fügt eine weitere leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            Console.Write("1. Einnahmen erfassen\t"); // Option zum Erfassen von Einnahmen
            Console.WriteLine("2. Ausgaben erfassen"); // Option zum Erfassen von Ausgaben
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.ResetColor(); // Setzt die Textfarbe zurück

            while (true) // Startet eine Endlosschleife
            {
                string choice = Console.ReadLine(); // Liest die Auswahl des Benutzers

                switch (choice) // Verarbeitet die Auswahl
                {
                    case "1":
                        DisplayHeader(); // Zeigt den Header an
                        SelectCategory(1); // Funktion zum Auswählen der Kategorie für Einnahmen
                        break;
                    case "2":
                        DisplayHeader(); // Zeigt den Header an
                        SelectCategory(2); // Funktion zum Auswählen der Kategorie für Ausgaben
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                        Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut."); // Gibt eine Fehlermeldung aus
                        Console.ResetColor(); // Setzt die Textfarbe zurück
                        continue; // Geht zurück zum Anfang der Schleife
                }
                break; // Beendet die Schleife, wenn eine gültige Auswahl getroffen wurde
            }
        }

        static void ShowIncomeAndExpenses()
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine("Einnahmen und Ausgaben Übersicht"); // Überschrift für die Übersicht

            // Holt alle JSON-Dateien im aktuellen Verzeichnis
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");

            // Überprüft, ob weniger als oder genau zwei Dateien vorhanden sind
            if (files.Length <= 2)
            {
                DisplayNoDataMessage(); // Zeigt eine Nachricht an, dass keine Daten vorhanden sind
                TimerAfterNoFiles(); // Wartet eine bestimmte Zeit, nachdem keine Dateien gefunden wurden
                Main(); // Ruft die Main-Methode auf, um das Programm neu zu starten
            }

            // Listen zur Speicherung von Einnahmen und Ausgaben
            List<BudgetItem> einnahmen = new List<BudgetItem>();
            List<BudgetItem> ausgaben = new List<BudgetItem>();

            // Lädt die Budget-Items aus den Dateien in die entsprechenden Listen
            LoadBudgetItems(files, einnahmen, ausgaben);

            // Zeigt die geladenen Budget-Items an
            DisplayBudgetItems(einnahmen, ausgaben);

            // Zeigt die Gesamtsummen der Einnahmen und Ausgaben an
            DisplayTotalAmounts(einnahmen, ausgaben);

            // Zeigt weitere Optionen an
            ShowOptions();
        }

        static void DisplayNoDataMessage()
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine(); // Fügt eine weitere leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            Console.WriteLine("Keine gespeicherten Daten gefunden."); // Gibt eine Nachricht aus, dass keine Daten gefunden wurden
            Console.ResetColor(); // Setzt die Textfarbe zurück
        }

        static void LoadBudgetItems(string[] files, List<BudgetItem> einnahmen, List<BudgetItem> ausgaben)
        {
            foreach (string file in files) // Iteriert über alle Dateien
            {
                string jsonString = File.ReadAllText(file); // Liest den Inhalt der Datei als JSON-String
                BudgetItem item = JsonSerializer.Deserialize<BudgetItem>(jsonString); // Deserialisiert den JSON-String zu einem BudgetItem-Objekt

                // Überprüft den Typ des BudgetItems und fügt es der entsprechenden Liste hinzu
                if (item.Type == "Einnahme")
                {
                    einnahmen.Add(item); // Fügt das Item der Einnahmen-Liste hinzu
                }
                else if (item.Type == "Ausgabe")
                {
                    ausgaben.Add(item); // Fügt das Item der Ausgaben-Liste hinzu
                }
            }
        }

        static void DisplayBudgetItems(List<BudgetItem> einnahmen, List<BudgetItem> ausgaben)
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Cyan; // Ändert die Textfarbe zu Cyan
            Console.WriteLine("{0,-50} {1,-50}", "Einnahmen", "Ausgaben"); // Überschrift für die Einnahmen und Ausgaben
            Console.WriteLine(new string('-', 100)); // Fügt eine Trennlinie ein
            Console.ResetColor(); // Setzt die Textfarbe zurück

            // Bestimmt die maximale Anzahl der Zeilen basierend auf der größeren Liste
            int maxRows = Math.Max(einnahmen.Count, ausgaben.Count);
            for (int i = 0; i < maxRows; i++) // Iteriert über die Zeilen
            {
                // Holt den Text für die Einnahmen und Ausgaben, wenn vorhanden, sonst leer
                string einnahmeText = i < einnahmen.Count ? FormatBudgetItem(einnahmen[i]) : "";
                string ausgabeText = i < ausgaben.Count ? FormatBudgetItem(ausgaben[i]) : "";

                // Teilt die Texte in Zeilen auf
                string[] einnahmeLines = einnahmeText.Split('\n');
                string[] ausgabeLines = ausgabeText.Split('\n');

                // Iteriert über die Zeilen und zeigt sie an
                for (int j = 0; j < Math.Max(einnahmeLines.Length, ausgabeLines.Length); j++)
                {
                    string einnahmeLine = j < einnahmeLines.Length ? einnahmeLines[j] : "";
                    string ausgabeLine = j < ausgabeLines.Length ? ausgabeLines[j] : "";

                    Console.ForegroundColor = ConsoleColor.Green; // Ändert die Textfarbe zu Grün
                    Console.WriteLine("{0,-50} {1,-50}", einnahmeLine, ausgabeLine); // Zeigt die Zeilen an
                    Console.ResetColor(); // Setzt die Textfarbe zurück
                }
            }
        }

        static void DisplayTotalAmounts(List<BudgetItem> einnahmen, List<BudgetItem> ausgaben)
        {
            // Berechnet die Gesamtsummen der Einnahmen und Ausgaben
            decimal gesamtEinnahmen = einnahmen.Sum(item => item.Amount);
            decimal gesamtAusgaben = ausgaben.Sum(item => item.Amount);

            Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine("{0,-50} {1,-50}", $"Gesamt: {gesamtEinnahmen}€", $"Gesamt: {gesamtAusgaben}€"); // Zeigt die Gesamtsummen an
            Console.WriteLine(new string('-', 100)); // Fügt eine Trennlinie ein
            Console.WriteLine(new string('-', 100)); // Fügt eine weitere Trennlinie ein
            Console.ResetColor(); // Setzt die Textfarbe zurück
        }

        static void TimerAfterNoFiles()
        {
            for (int i = 5; i > 0; i--) // Countdown von 5 Sekunden
            {
                Console.WriteLine(); // Fügt eine leere Zeile ein
                Console.WriteLine(); // Fügt eine weitere leere Zeile ein
                Console.ForegroundColor = ConsoleColor.DarkGreen; // Ändert die Textfarbe zu Dunkelgrün
                Console.WriteLine($"Rückkehr zum Hauptmenü in {i} Sekunden..."); // Zeigt die verbleibende Zeit an
                Console.ResetColor(); // Setzt die Textfarbe zurück
                System.Threading.Thread.Sleep(1000); // Wartet eine Sekunde
            }
        }

        static string FormatBudgetItem(BudgetItem item)
        {
            // Formatiert die Eigenschaften eines BudgetItems als String
            return $"Kategorie: {item.Category}\nBeschreibung: {item.Description}\nBetrag: {item.Amount}€\nDatum: {item.Date.ToString("d.M.yyyy")}\n{new string('-', 50)}";
        }

        static void ShowOptions()
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine(); // Fügt eine weitere leere Zeile ein
            Console.WriteLine("Zwei Optionen zur Auswahl:"); // Zeigt die verfügbaren Optionen an
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            Console.WriteLine("1. Hauptmenü\t\t\t2. Alle Daten löschen"); // Zeigt die Optionen an
            Console.ResetColor(); // Setzt die Textfarbe zurück
            Console.WriteLine(); // Fügt eine leere Zeile ein

            string choice;
            do
            {
                choice = Console.ReadLine(); // Liest die Auswahl des Benutzers
                if (choice == "1")
                {
                    Main(); // Ruft die Main-Methode auf, um zum Hauptmenü zurückzukehren
                }
                else if (choice == "2")
                {
                    DeleteAllData(); // Ruft die Methode zum Löschen aller Daten auf
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                    Console.WriteLine("Ungültige Auswahl!"); // Gibt eine Fehlermeldung aus
                    Console.ResetColor(); // Setzt die Textfarbe zurück
                }
            } while (choice != "1" && choice != "2"); // Wiederholt die Schleife, bis eine gültige Auswahl getroffen wurde
        }

        static void DeleteAllData()
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine("Sind Sie sicher, dass Sie alle Daten löschen möchten?"); // Fragt den Benutzer, ob er alle Daten löschen möchte
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            Console.WriteLine("1. Ja\t\t\t\t2. Nein"); // Zeigt die Optionen an
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.ResetColor(); // Setzt die Textfarbe zurück

            string choice = Console.ReadLine(); // Liest die Auswahl des Benutzers
            if (choice == "1")
            {
                // Holt alle JSON-Dateien im aktuellen Verzeichnis und löscht sie
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");
                foreach (string file in files)
                {
                    File.Delete(file); // Löscht die Datei
                }

                TimerAfterDeletion(); // Wartet eine bestimmte Zeit nach dem Löschen der Dateien
                Main(); // Ruft die Main-Methode auf, um das Programm neu zu starten
            }
            else if (choice == "2")
            {
                Console.Clear(); // Löscht die Konsole
                System.Threading.Thread.Sleep(500); // Wartet eine halbe Sekunde
                ShowIncomeAndExpenses(); // Zeigt die Einnahmen- und Ausgabenübersicht erneut an
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                Console.WriteLine("Ungültige Auswahl. Bitte wählen Sie erneut."); // Gibt eine Fehlermeldung aus
                Console.ResetColor(); // Setzt die Textfarbe zurück
                DeleteAllData(); // Ruft die Methode erneut auf, um eine gültige Auswahl zu erhalten
            }
        }

        static void TimerAfterDeletion()
        {
            for (int i = 5; i > 0; i--) // Countdown von 5 Sekunden
            {
                Console.Clear(); // Löscht die Konsole
                Console.ForegroundColor = ConsoleColor.DarkRed; // Ändert die Textfarbe zu Dunkelrot
                Console.WriteLine("Alle Daten wurden gelöscht."); // Gibt eine Nachricht aus, dass alle Daten gelöscht wurden
                Console.ResetColor(); // Setzt die Textfarbe zurück
                Console.WriteLine(); // Fügt eine leere Zeile ein
                Console.WriteLine(); // Fügt eine weitere leere Zeile ein
                Console.ForegroundColor = ConsoleColor.DarkGreen; // Ändert die Textfarbe zu Dunkelgrün
                Console.WriteLine($"Rückkehr zum Hauptmenü in {i} Sekunden..."); // Zeigt die verbleibende Zeit bis zur Rückkehr zum Hauptmenü an
                Console.ResetColor(); // Setzt die Textfarbe zurück
                System.Threading.Thread.Sleep(1000); // Wartet eine Sekunde
            }
        }

        static void SelectCategory(int type)
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            if (type == 1)
            {
                Console.WriteLine("Bitte wählen Sie eine Kategorie für Einnahme:"); // Fordert den Benutzer auf, eine Kategorie für Einnahmen zu wählen
            }
            else
            {
                Console.WriteLine("Bitte wählen Sie eine Kategorie für Ausgabe:"); // Fordert den Benutzer auf, eine Kategorie für Ausgaben zu wählen
            }
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            Console.WriteLine("1. Miete"); // Option für Miete
            Console.WriteLine("2. Lebensmittel"); // Option für Lebensmittel
            Console.WriteLine("3. Freizeit"); // Option für Freizeit
            Console.WriteLine("4. Transport"); // Option für Transport
            Console.WriteLine("5. Sonstiges"); // Option für Sonstiges
            Console.ResetColor(); // Setzt die Textfarbe zurück
            Console.WriteLine(); // Fügt eine leere Zeile ein

            while (true) // Startet eine Endlosschleife
            {
                string choice = Console.ReadLine(); // Liest die Auswahl des Benutzers
                BudgetItem item = new BudgetItem
                {
                    Type = type == 1 ? "Einnahme" : "Ausgabe" // Setzt den Typ des BudgetItems basierend auf der Auswahl
                };

                // Verarbeitet die Auswahl und setzt die Kategorie des BudgetItems
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
                        Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                        Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut."); // Gibt eine Fehlermeldung aus
                        Console.ResetColor(); // Setzt die Textfarbe zurück
                        continue; // Geht zurück zum Anfang der Schleife
                }

                AddDescription(item); // Ruft die Methode zum Hinzufügen einer Beschreibung auf
                break; // Beendet die Schleife, wenn eine gültige Auswahl getroffen wurde
            }
        }

        static void AddDescription(BudgetItem item)
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine($"Bitte geben Sie eine Beschreibung für {item.Category} ein (3-15 Zeichen):"); // Fordert den Benutzer auf, eine Beschreibung einzugeben
            Console.WriteLine(); // Fügt eine weitere leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            string description = Console.ReadLine(); // Liest die Beschreibung des Benutzers
            Console.ResetColor(); // Setzt die Textfarbe zurück

            // Überprüft die Länge der Beschreibung und fordert den Benutzer zur erneuten Eingabe auf, falls die Länge nicht im gültigen Bereich liegt
            while (description.Length < 3 || description.Length > 15)
            {
                if (description.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                    Console.WriteLine("Beschreibung zu kurz. Bitte geben Sie eine Beschreibung mit mindestens 3 Zeichen ein:"); // Gibt eine Fehlermeldung aus
                    Console.ResetColor(); // Setzt die Textfarbe zurück
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                    Console.WriteLine("Beschreibung zu lang. Bitte geben Sie eine Beschreibung mit maximal 15 Zeichen ein:"); // Gibt eine Fehlermeldung aus
                    Console.ResetColor(); // Setzt die Textfarbe zurück
                }
                description = Console.ReadLine(); // Liest die neue Beschreibung des Benutzers
            }

            item.Description = description; // Setzt die Beschreibung des BudgetItems
            Console.WriteLine(); // Fügt eine leere Zeile ein
            AddAmount(item); // Ruft die Methode zum Hinzufügen eines Betrags auf
        }

        static void AddAmount(BudgetItem item)
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine($"Bitte geben Sie den Betrag ohne '€ - Zeichen' für {item.Type} ein (max 100 000):"); // Fordert den Benutzer auf, einen Betrag einzugeben
            Console.WriteLine(); // Fügt eine weitere leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            string amountInput = Console.ReadLine(); // Liest den Betrag des Benutzers
            Console.ResetColor(); // Setzt die Textfarbe zurück
            decimal amount;

            // Überprüft die Gültigkeit des Betrags und fordert den Benutzer zur erneuten Eingabe auf, falls der Betrag ungültig ist oder den maximalen Wert überschreitet
            while (!decimal.TryParse(amountInput, out amount) || amount > 100000)
            {
                Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                if (amount > 100000)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                    Console.WriteLine("Der Betrag darf 100.000 nicht überschreiten. Bitte geben Sie einen gültigen Betrag ein:"); // Gibt eine Fehlermeldung aus
                    Console.ResetColor(); // Setzt die Textfarbe zurück
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                    Console.WriteLine("Ungültiger Betrag. Bitte geben Sie eine gültige Zahl ein:"); // Gibt eine Fehlermeldung aus
                    Console.ResetColor(); // Setzt die Textfarbe zurück
                }
                Console.ResetColor(); // Setzt die Textfarbe zurück
                amountInput = Console.ReadLine(); // Liest den neuen Betrag des Benutzers
            }

            item.Amount = Math.Round(amount, 2); // Setzt den Betrag des BudgetItems und rundet ihn auf zwei Dezimalstellen
            Console.WriteLine(); // Fügt eine leere Zeile ein
            AddDate(item); // Ruft die Methode zum Hinzufügen eines Datums auf
        }

        static void AddDate(BudgetItem item)
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine($"Bitte geben Sie das Datum für {item.Type} ein (Format: Tag.Monat.Jahr):"); // Fordert den Benutzer auf, ein Datum einzugeben
            Console.WriteLine(); // Fügt eine weitere leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Blue; // Ändert die Textfarbe zu Blau
            string dateInput = Console.ReadLine(); // Liest das Datum des Benutzers
            Console.ResetColor(); // Setzt die Textfarbe zurück
            DateTime date;

            // Überprüft die Gültigkeit des Datums und fordert den Benutzer zur erneuten Eingabe auf, falls das Datum ungültig ist
            while (!DateTime.TryParseExact(dateInput, "d.M.yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                Console.ForegroundColor = ConsoleColor.Red; // Ändert die Textfarbe zu Rot
                Console.WriteLine("Ungültiges Datum. Bitte geben Sie ein gültiges Datum im Format Tag.Monat.Jahr ein:"); // Gibt eine Fehlermeldung aus
                Console.ResetColor(); // Setzt die Textfarbe zurück
                dateInput = Console.ReadLine(); // Liest das neue Datum des Benutzers
            }

            item.Date = date; // Setzt das Datum des BudgetItems
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Green; // Ändert die Textfarbe zu Grün
            Console.WriteLine($"{item.Type} für {item.Category} mit Beschreibung: {item.Description}, Betrag: {item.Amount}€ und Datum: {item.Date.ToString("d.M.yyyy")} erfasst."); // Bestätigt die Eingabe des Benutzers
            Console.ResetColor(); // Setzt die Textfarbe zurück

            SaveAsJson(item); // Ruft die Methode zum Speichern des BudgetItems als JSON auf
            PromptReturnToMainMenu(); // Ruft die Methode zur Rückkehr zum Hauptmenü auf
        }

        static void SaveAsJson(BudgetItem item)
        {
            string jsonString = JsonSerializer.Serialize(item); // Serialisiert das BudgetItem als JSON-String
            string fileName = $"{item.Type}_{item.Category}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.json"; // Generiert einen Dateinamen basierend auf dem Typ, der Kategorie und dem aktuellen Datum und Uhrzeit
            File.WriteAllText(fileName, jsonString); // Schreibt den JSON-String in eine Datei
        }

        static void PromptReturnToMainMenu()
        {
            Console.WriteLine(); // Fügt eine leere Zeile ein
            Console.WriteLine(); // Fügt eine weitere leere Zeile ein
            Console.WriteLine(); // Fügt eine dritte leere Zeile ein
            Console.ForegroundColor = ConsoleColor.Yellow; // Ändert die Textfarbe zu Gelb
            Console.WriteLine("Drücken Sie die Eingabetaste, um zum Hauptmenü zurückzukehren."); // Fordert den Benutzer auf, die Eingabetaste zu drücken, um zum Hauptmenü zurückzukehren
            Console.ResetColor(); // Setzt die Textfarbe zurück
            Console.ReadLine(); // Wartet auf die Eingabe des Benutzers
            Main(); // Ruft die Main-Methode auf, um zum Hauptmenü zurückzukehren
        }
    }
}