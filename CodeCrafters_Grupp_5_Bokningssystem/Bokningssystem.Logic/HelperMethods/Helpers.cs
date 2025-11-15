using Bokningssystem.Logic.RoomClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.HelperMethods
{
    public static class Helper
    {
        // Metod för att ändra string till int. (Används för menyer) Gjord av Sara.
        public static int ParseInt(string userPrompt, int min, int max)
        {
            while (true) // Loop för att säkerställa att vi får ut ett giltigt val.
            {
                Console.Write(userPrompt);
                string? input = Console.ReadLine()?.Trim();
                // Kontrollerar så att användaren skriver in något.
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Du måste skriva in en siffra! Försök igen.");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
                // Läser in användarens val och försöker konvertera till int.
                // Kontrollerar så att användaren väljer ett giltigt nummer
                if (int.TryParse(input, out int val) && val >= min && val <= max)
                {
                    return val; // Returnerar användarens val i en int.
                }
                else
                {
                    // Ifall det inte är giltigt skickas ett felmeddelande.
                    Console.WriteLine($"Du måste skriva in en siffra mellan {min}-{max}! Vänligen försök igen.");
                    continue;
                }

            }
        }
        // Metod för att läsa in datum på bokningar. Gjord av Sara.
        public static DateTime ParseDateTime(string userPrompt, CultureInfo culture)
        {
            Console.Clear();
            while (true) // Loop för att säkerställa att vi får ut ett giltigt datum.
            {
                Console.WriteLine($"{userPrompt} (Format: {culture.DateTimeFormat.ShortDatePattern}): ");
                string? input = Console.ReadLine().Trim().Replace(" ", "");
                // Kontrollerar så att användaren skriver in något.
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Du måste skriva in ett datum! Försök igen.");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
                // Läser in användarens val och försöker konvertera till DateTime objekt.
                // Läser in DateTime utefter kulturellt format.
                if (DateTime.TryParse(input, culture, DateTimeStyles.None, out DateTime date))
                {
                    // Kontrollerar så att datumet ej passerat.
                    if (date.Date < DateTime.Today)
                    {
                        Console.WriteLine("Du kan inte boka ett datum som passerat! Försök igen.");
                        continue;
                    }
                    // Kontrollerar så att datumet är på en helgdag.
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        Console.WriteLine("Du kan inte göra en bokning på en helgdag! Försök igen.");
                        continue;
                    }
                    return date.Date; // Returnerar användarens val i ett DateTime objekt med endast datum.
                }
                else
                {
                    // Ifall användarens input är ogiltigt/fel format skickas ett felmeddelande.
                    // Visar exempel på rätt format utefter kultur.
                    Console.WriteLine($"Felaktigt datumformat! Försök igen (Korrekt format: {DateTime.Now.ToString(culture.DateTimeFormat.ShortDatePattern)}).");
                    continue;
                }
            }
        }
        // Metod för att läsa in start- och sluttid på bokningar. Gjord av Sara.
        public static (TimeSpan StartTime, TimeSpan EndTime) ReadTimeSpan(string userPrompt, string userPrompt2, DateTime bookingDate, int maxBookingHours, CultureInfo culture)
        {
            Console.Clear();
            TimeSpan bookingStartTime, bookingEndTime;

            while (true) // Loop för att säkerställa att vi får ut en giltig starttid.
            {   // Användaren skriver in starttid. Visar exempelformat beroende på kultur.
                Console.Write($"\n{userPrompt} (Format: ({DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)}): ");
                string startTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");
                // Kontrollerar så att användaren skriver in något.
                if (string.IsNullOrWhiteSpace(startTimeInput))
                {
                    Console.WriteLine("Du måste skriva in en tid! Försök igen.");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
                // Läser in användarens val och försöker konvertera till ett DateTime objekt.
                // Läser in DateTime utefter kulturellt format.
                if (!DateTime.TryParse(startTimeInput, culture, DateTimeStyles.None, out DateTime startTimeDateTime))
                {
                    Console.WriteLine($"Felaktigt tidsformat! Försök igen (Korrekt format: ({DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)})");
                    continue;
                }
                // Ändrar DateTime objekt till TimeSpan.
                bookingStartTime = startTimeDateTime.TimeOfDay;
                // Skapar nytt DateTime objekt med datum och starttid för jämförelse.
                DateTime startDateTime = bookingDate + bookingStartTime;
                // Kontrollerar så att starttiden inte passerat.
                if (startDateTime < DateTime.Now)
                {
                    Console.WriteLine("Du kan inte boka en tid som passerat!");
                    continue;
                }
                // Tidigast tid rum kan bokas (kl 8). String som skriver ut tiden utefter kultur.
                TimeSpan earliestBookingTime = TimeSpan.FromHours(8);
                string earliestBookingTimeCurrentCulture = DateTime.Today.Add(earliestBookingTime).ToString("t", culture);
                if (bookingStartTime < earliestBookingTime)
                {
                    Console.WriteLine($"Du kan inte boka rum innan kl {earliestBookingTimeCurrentCulture}");
                    continue;
                }
                break; // Lämnar loopen då vi har en giltig tid.
            }
            while (true) // Loop för att säkerställa giltig sluttid.
            {
                Console.Write($"\n{userPrompt2} (Format:{DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)}: ");
                string endTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");

                if (!DateTime.TryParse(endTimeInput, culture, DateTimeStyles.None, out DateTime endTimeDateTime))
                {
                    Console.WriteLine($"Felaktigt tidsformat! Försök igen (Korrekt format: {DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)}).");
                    continue;
                }
                bookingEndTime = endTimeDateTime.TimeOfDay;
                // Kontrollerar så att sluttiden inte är före starttiden.
                if (bookingEndTime <= bookingStartTime)
                {
                    Console.WriteLine("Sluttiden måste vara efter starttiden!");
                    continue;
                }
                // Senast tid rum kan bokas (kl 17). String som skriver ut tiden utefter kultur.
                TimeSpan latestBookingTime = TimeSpan.FromHours(17);
                string latestBookingTimeCurrentCulture = DateTime.Today.Add(latestBookingTime).ToString("t", culture);
                if (bookingEndTime > latestBookingTime)
                {
                    Console.WriteLine($"Du kan inte boka rum efter kl {latestBookingTimeCurrentCulture}");
                    continue;
                }
                // Jämför bokningens längd med maxtid för bokning (6 timmar för grupprum, 8 timmar för klassrum).
                TimeSpan bookingDuration = bookingEndTime - bookingStartTime;
                if (bookingDuration.TotalHours > maxBookingHours)
                {
                    Console.WriteLine($"Du kan inte boka rummet mer än {maxBookingHours} timmar!");
                    continue;
                }

                break; // Lämnar loopen då vi har en giltig tid.
            }
            return (bookingStartTime, bookingEndTime); // Returnerar start- och sluttid.
        }
    
        // Metod för att läsa in namn. Gjord av Sara.
        public static string ReadName(string userPrompt)
        {
            Console.Clear();
            string bookerNameInput;
            while (true)
            {
                // Användaren skriver in sitt namn
                Console.WriteLine($"{userPrompt}");
                bookerNameInput = Console.ReadLine().Trim();
                // Kontrollerar så att namnet inte är tomt/whitespace. 
                if (string.IsNullOrWhiteSpace(bookerNameInput))
                {
                    // Skriver ut felmeddelande och användaren skickas tillbaka till bokningsmenyn.
                    Console.WriteLine("Du måste ange ett namn för att boka ett rum!");
                    continue;
                }
                // Kontrollerar så att namnet inte innehåller siffror eller specialtecken (förutom t.ex &, / eller +).
                if (bookerNameInput.Contains('1') || bookerNameInput.Contains('2') || bookerNameInput.Contains('3')
                    || bookerNameInput.Contains('4') || bookerNameInput.Contains('5') || bookerNameInput.Contains('6')
                    || bookerNameInput.Contains('7') || bookerNameInput.Contains('8') || bookerNameInput.Contains('9')
                    || bookerNameInput.Contains('!') || bookerNameInput.Contains('@') || bookerNameInput.Contains('£')
                    || bookerNameInput.Contains('%') || bookerNameInput.Contains('#') || bookerNameInput.Contains('¤')
                    || bookerNameInput.Contains('=') || bookerNameInput.Contains('?') || bookerNameInput.Contains('^')
                    || bookerNameInput.Contains('*') || bookerNameInput.Contains('<') || bookerNameInput.Contains('>')
                    || bookerNameInput.Contains(':') || bookerNameInput.Contains(';') || bookerNameInput.Contains('|')
                    || bookerNameInput.Contains('{') || bookerNameInput.Contains('}') || bookerNameInput.Contains('[')
                    || bookerNameInput.Contains(']') || bookerNameInput.Contains('½') || bookerNameInput.Contains('/')
                    || bookerNameInput.Contains('(') || bookerNameInput.Contains(')') || bookerNameInput.Contains('\\'))
                {
                    Console.WriteLine("Namnet får endast innehålla bokstäver!");
                    continue;
                }
                break;
            }
            // Formaterar namn så första bokstaven är stor och resten små.
            string bookerName = char.ToUpper(bookerNameInput[0]) + bookerNameInput.Substring(1).ToLower();
            return bookerName;
        }
        // Metod för att låta användaren bekräfta val. Gjord av Sara.
        public static bool ConfirmAction(string userPrompt, string actionCanceled)
        {
            bool confirmLoop = true;
            while (confirmLoop) // Loop för att få ut ett giltigt svar.
            {
                Console.Clear();
                Console.WriteLine($"Är du säker på att du vill {userPrompt}? (Ja/Nej)");
                string confirmAction = Console.ReadLine().Trim().ToLower();
                // Om användaren skriver ja eller j tas bokningen bort.
                // Skriver de nej eller n avbryts det.
                if (confirmAction == "j" || confirmAction == "ja")
                {
                    return true;
                }
                else if (confirmAction == "n" || confirmAction == "nej")
                {
                    Console.WriteLine($"{actionCanceled}");

                    return false;
                }
                else
                {
                    Console.WriteLine($"Du måste bekräfta Ja eller Nej!");
                    BackToMenu("vidare...");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
            }
            return false;
        }

        public static void TypeOfRoomMenu()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║        Välj typ av rum         ║");
            Console.WriteLine("╠════════════════════════════════╣");
            Console.WriteLine("║                                ║");
            Console.WriteLine("║   [1] Klassrum                 ║");
            Console.WriteLine("║   [2] Grupprum                 ║");
            Console.WriteLine("║   [0] Återgå till huvudmenyn   ║");
            Console.WriteLine("║                                ║");
            Console.WriteLine("╚════════════════════════════════╝");
        }
        // Metod för att visa alla klassrum/grupprum. Gjord av Sara.
        public static void ShowAvailableRooms<T>(string type, List<T> typeOfRoom) where T : Room
        {
            Console.Clear();
            // Visar varje rum med namn, kapacitet & status.
            // Beroende på vilken rumslista man skickar in (grupprum/klassrum).
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║                      Tillgängliga {type}                       ║");
            Console.WriteLine($"╚══════════════════════════════════════════════════════════════════╝\n");
            for (int i = 0; i < typeOfRoom.Count; i++)
            {
                var currentBooking = typeOfRoom[i].bookings
                .FirstOrDefault(b => DateTime.Now >= b.StartTime && DateTime.Now < b.EndTime);
                Console.Write($"[{i + 1}] {typeOfRoom[i].RoomName} Kapacitet: {typeOfRoom[i].RoomCapacity} ");
                if (typeOfRoom[i] is ClassRoom classRoom)
                { Console.Write($" Projektor: {(classRoom.HasProjector ? "Ja " : "Nej")} "); }
                if (typeOfRoom[i] is GroupRoom groupRoom)
                { Console.Write($" Smartboard: {(groupRoom.HasSmartBoard ? "Ja " : "Nej")} "); }
                Console.ForegroundColor = typeOfRoom[i].IsCurrentlyAvailable ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(typeOfRoom[i].IsCurrentlyAvailable ? "Tillgängligt just nu."
                    : currentBooking != null ? $"Upptaget just nu. Kan bokas efter kl {currentBooking.EndTime:HH:mm}."
                    : "Upptaget just nu.");
                Console.ResetColor();
            }
        }
        public static void BackToMenu(string message)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"\nTryck ENTER för att gå {message}");
            Console.ReadKey();
            Console.Clear();
        }
        public static void DisplayMessage(int min, int max)
        {
            Console.Clear();
            Console.WriteLine($"Du måste skriva in en siffra mellan {min}-{max}! Vänligen försök igen.");

        }
    }
}
