using Bokningssystem.Logic.RoomClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.HelperMethods
{
    public static class InputHelper
    {
        // -------------------------------------------------------------------------
        //  Metod för att ändra string till int. (Används för menyer) Gjord av Sara.
        // -------------------------------------------------------------------------
        public static int ParseInt(string userPrompt, int min, int max)
        {
            while (true) 
            {
                Console.Write(userPrompt);
                string? input = Console.ReadLine()?.Trim();
               
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("\nDu måste skriva in en siffra! Försök igen.");
                    continue; 
                }
                // Kontrollerar ifall användarens input är en siffra inom givet intervall.
                if (!int.TryParse(input, out int val) || val < min || val > max)
                {
                    Console.WriteLine($"\nDu måste skriva in en siffra mellan {min}-{max}! Vänligen försök igen.");
                    continue;
                }
                return val;

            }
        }
        // ----------------------------------------------------------------
        //     Metod för att läsa in datum på bokningar. Gjord av Sara.
        // ----------------------------------------------------------------
        public static DateTime ParseDateTime(string userPrompt, CultureInfo culture)
        {
            // Exempel på format för datum utefter kultur.
            string exampleDate = DateTime.Now.ToString(culture.DateTimeFormat.ShortDatePattern);
            while (true) 
            {
                Console.WriteLine($"{userPrompt} (Format ({exampleDate}): ");
                string? input = Console.ReadLine().Trim().Replace(" ", "");
               
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("\nDu måste skriva in ett datum! Vänligen försök igen.");
                    continue; 
                }
                // Kontrollerar ifall användarens input är ett giltigt datum utefter kultur.
                if (!DateTime.TryParse(input, culture, DateTimeStyles.None, out DateTime date))
                {
                    Console.WriteLine($"\nFelaktigt datumformat! (Korrekt format: ({exampleDate}).");
                    continue;
                }
                // Kontrollerar så att datumet ej passerat.
                if (date.Date < DateTime.Today)
                {
                    Console.WriteLine("\nDu kan inte boka ett datum som passerat! Vänligen försök igen.");
                    continue;
                }
                // Kontrollerar så att datumet inte är på en helgdag.
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine("\nDu kan inte göra en bokning på en helgdag! Vänligen försök igen.");
                    continue;
                }
                return date.Date; // Returnerar användarens val i ett DateTime objekt med endast datum.
            }
            
        }
        // ----------------------------------------------------------------------
        //  Metod för att läsa in start- och sluttid på bokningar. Gjord av Sara.
        // ----------------------------------------------------------------------
        public static (TimeSpan StartTime, TimeSpan EndTime) ReadTimeSpan(string startPrompt, string endPrompt, DateTime bookingDate, int maxBookingHours, CultureInfo culture)
        {
            TimeSpan bookingStartTime, bookingEndTime;
            // Exempel på format för tid utefter kultur i kort tidsformat.
            string exampleTime = DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture);
            // Tidigaste och senaste tider som kan bokas.
            TimeSpan earliestStartTime = TimeSpan.FromHours(8);
            TimeSpan latestStartTime = TimeSpan.FromHours(16.5);
            TimeSpan latestEndTime = TimeSpan.FromHours(17);
            // Formatterad till string som skriver ut tiden utefter kultur i kort tidsformat.
            string earliestStartTimeFormatted = DateTime.Today.Add(earliestStartTime).ToString("t", culture);
            string latestStartTimeFormatted = DateTime.Today.Add(latestStartTime).ToString("t", culture);
            string latestEndTimeFormatted = DateTime.Today.Add(latestEndTime).ToString("t", culture);

            // ----------------------------------------------------------------
            //                          Starttid
            // ----------------------------------------------------------------
            Console.WriteLine($"Rum kan bokas mellan {earliestStartTimeFormatted} - {latestStartTimeFormatted}");
            while (true)
            {
                Console.Write($"\n{startPrompt} (Format: {exampleTime}): ");
                string startTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");

                if (string.IsNullOrWhiteSpace(startTimeInput))
                {
                    Console.WriteLine("\nDu måste skriva in en tid! Vänligen försök igen.\n");
                    continue;
                }
                // Kontrollerar ifall användarens input är en giltig tid utefter kultur.
                if (!DateTime.TryParse(startTimeInput, culture, DateTimeStyles.None, out DateTime parsedStartTime))
                {
                    Console.WriteLine($"Felaktigt tidsformat! (Korrekt format: {exampleTime})");
                    continue;
                }
                // Ändrar DateTime objekt till TimeSpan.
                bookingStartTime = parsedStartTime.TimeOfDay;
                // Skapar nytt DateTime objekt med datum och starttid för jämförelse.
                DateTime startDateTime = bookingDate + bookingStartTime;
                // Kontrollerar så att starttiden inte passerat eller är före/efter öppettider.
                if (startDateTime < DateTime.Now)
                {
                    Console.WriteLine("\nDu kan inte boka en tid som passerat!");
                    continue;
                }
                if (bookingStartTime < earliestStartTime)
                {
                    Console.WriteLine($"\nRum kan inte bokas innan {earliestStartTimeFormatted}");
                    continue;
                }
                if (bookingStartTime > latestStartTime)
                {
                    Console.WriteLine($"\nRum kan inte bokas efter {latestStartTimeFormatted}");
                    continue;
                }
                break;
            }

            // ----------------------------------------------------------------
            //                            Sluttid
            // ----------------------------------------------------------------
            MenuHelper.NewBookingMenu();
            Console.WriteLine($"Rum kan bokas mellan {earliestStartTimeFormatted} - {latestEndTimeFormatted}");
            while (true)
            {
                Console.Write($"\n{endPrompt} (Format {exampleTime}): ");
                string endTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");
                if (string.IsNullOrWhiteSpace(endTimeInput))
                {
                    Console.WriteLine("\nDu måste skriva in en tid! Vänligen försök igen.\n");
                    continue;
                }
                // Kontrollerar ifall användarens input är en giltig tid utefter kultur.
                if (!DateTime.TryParse(endTimeInput, culture, DateTimeStyles.None, out DateTime parsedEndTime))
                {
                    Console.WriteLine($"Felaktigt tidsformat! (Korrekt format {exampleTime}).");
                    continue;
                }
                bookingEndTime = parsedEndTime.TimeOfDay;
                // Kontrollerar så att sluttiden inte är före starttiden eller efter stängning.
                if (bookingEndTime <= bookingStartTime)
                {
                    Console.WriteLine("\nSluttiden måste vara efter starttiden!");
                    continue;
                }
                if (bookingEndTime > latestEndTime)
                {
                    Console.WriteLine($"\nRum kan inte bokas efter {latestEndTimeFormatted}");
                    continue;
                }
                // Jämför bokningens längd med maxtid för bokning (6 timmar för grupprum, 8 timmar för klassrum).
                TimeSpan bookingDuration = bookingEndTime - bookingStartTime;
                if (bookingDuration.TotalHours > maxBookingHours)
                {
                    Console.WriteLine($"\nDu kan inte boka rummet mer än {maxBookingHours} timmar!");
                    continue;
                }
                // Kontrollerar så att bokningen är över 30 min (minsta bokningstid).
                if (bookingDuration.TotalMinutes < 30)
                {
                    Console.WriteLine($"\nBokningen måste vara minst 30 minuter!");
                    continue;
                }

                break;
            }
            return (bookingStartTime, bookingEndTime); // Returnerar start- och sluttid.
        }

        // ----------------------------------------------------------------
        //            Metod för att läsa in namn. Gjord av Sara
        // ----------------------------------------------------------------
        public static string ReadName(string userPrompt)
        {
            string bookerNameInput;
            while (true)
            {
                Console.Write($"\n{userPrompt}");
                bookerNameInput = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(bookerNameInput))
                {
                    Console.WriteLine("\nDu måste ange ett namn för att boka ett rum! Vänligen försök igen!");
                    continue;
                }
                // Kontrollerar så att namnet inte innehåller siffror eller specialtecken.
                if (!IsLetter(bookerNameInput))
                {
                    Console.WriteLine("\nNamnet får endast innehålla bokstäver!");
                    continue;
                }
                break;
            }
            // Formaterar namn så första bokstaven är stor och resten små.
            string bookerName = char.ToUpper(bookerNameInput[0]) + bookerNameInput.Substring(1).ToLower();
            return bookerName;
        }
        // ----------------------------------------------------------------
        //    Metod för att låta användaren bekräfta val. Gjord av Sara.
        // ----------------------------------------------------------------
        public static bool ConfirmAction(string userPrompt, string actionCanceled)
        {
            bool confirmLoop = true;
            while (confirmLoop)
            {
                Console.WriteLine($"Är du säker på att du vill {userPrompt} (Ja/Nej)");
                string confirmAction = Console.ReadLine().Trim().ToLower();

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
                    Console.WriteLine($"\nDu måste bekräfta Ja eller Nej!");
                    MenuHelper.GoBack("vidare...");
                    continue; 
                }
            }
            return false;
        }
        // -------------------------------------------------------------------
        //  Metod för att kolla om string innehåller bokstäver. Gjord av Sara.
        // -------------------------------------------------------------------
        public static bool IsLetter(string userInput)
        {
            foreach (char c in userInput)
            {
                int code = (int)c;

                bool isAsciiLetter = (code >= 65 && code <= 90) || (code >= 97 && code <= 122);

                bool isSwedishLetter = c == 'å' || c == 'ä' || c == 'ö' || c == 'Å' || c == 'Ä' || c == 'Ö';
                if (!isAsciiLetter && !isSwedishLetter)
                {
                    return false;
                }
            }
            return true;
        }

    }
}