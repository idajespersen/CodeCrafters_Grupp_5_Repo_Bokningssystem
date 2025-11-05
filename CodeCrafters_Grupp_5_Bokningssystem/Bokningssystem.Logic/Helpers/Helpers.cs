
using Bokningssystem.Logic.Rooms;
using Bokningssystem.Logic.BookingClass;
using Bokningssystem.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Helpers
{
    public static class Helper
    {
        public static int ParseInt(string userPrompt, int min, int max)
        {
            while (true) // Loop för att säkerställa giltigt val.
            {
                Console.WriteLine(userPrompt);
                string? input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Du måste skriva in en siffra! Försök igen.");
                    continue;
                }
                // Läser in användarens val och försöker konvertera till int.
                // Kontrollerar så att användaren väljer ett giltigt nummer
                if (int.TryParse(input, out int val) && val >= min && val <= max)
                {
                    return val;
                }
                else
                {
                    // Ifall det inte är giltigt skickas ett felmeddelande.
                    Console.WriteLine($"Du måste skriva in en siffra mellan {min}-{max}! Försök igen.");
                }
            }
        }

        public static DateTime ParseDateTime(string userPrompt, CultureInfo culture)
        {
            while (true)
            {
                Console.WriteLine(userPrompt);
                string? input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Du måste skriva in ett datum! Försök igen.");
                    continue;
                }

                if (DateTime.TryParse(input, culture, DateTimeStyles.None, out DateTime date))
                {
                    if (date.Date < DateTime.Today)
                    {
                        Console.WriteLine("Du kan inte boka ett datum som passerat! Försök igen.");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                    return date.Date;
                }
                else
                {
                    Console.WriteLine($"Felaktigt datumformat! Försök igen (Korrekt format: {DateTime.Now.ToString(culture.DateTimeFormat.ShortDatePattern)}).");
                    continue;
                }
            }
        }

        public static (TimeSpan StartTime, TimeSpan EndTime) ReadTimeSpan(DateTime bookingDate, int maxBookingHours)
        {
            TimeSpan bookingStartTime, bookingEndTime;

            while (true)
            {
                Console.Write($"\nAnge bokningens starttid (HH:MM): ");
                string startTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");
                if (string.IsNullOrWhiteSpace(startTimeInput))
                {
                    Console.WriteLine("Du måste skriva in en tid! Försök igen.");
                    continue;
                }

                if (!TimeSpan.TryParseExact(startTimeInput, [@"h\:mm", @"hh\:mm"], CultureInfo.InvariantCulture, out bookingStartTime))
                {
                    Console.WriteLine("Felaktigt tidsformat! Försök igen (Korrekt format: 09:30).");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
                //// Skapar DateTime objekt med datum och starttid för jämförelse.
                DateTime startDateTime = bookingDate + bookingStartTime;
                // Kontrollerar så att starttiden inte passerat.
                if (startDateTime < DateTime.Now)
                {
                    Console.WriteLine("Du kan inte boka en tid som passerat!");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
                TimeSpan earliestBookingTime = TimeSpan.FromHours(8);
                if (bookingStartTime < earliestBookingTime)
                {
                    Console.WriteLine($"Du kan inte boka rum innan kl 08:00");
                    continue;
                }
                break; // Lämnar loopen då vi har en giltig tid.
            }
            while (true) // Loop för att säkerställa giltig sluttid.
            {
                // Användaren skriver in bokningens sluttid.
                Console.Write("\nAnge sluttid (format: HH:MM): ");
                string endTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");
                // Kontrollerar om användaren skrivit in rätt format på tid. Enligt svensk standard.
                // Skickar ut bookingEndTime.
                if (!TimeSpan.TryParseExact(endTimeInput, [@"h\:mm", @"hh\:mm"], CultureInfo.InvariantCulture, out bookingEndTime))
                {
                    Console.WriteLine("Felaktigt tidsformat! Försök igen (Korrekt format: 09:30).");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
                // Kontrollerar så att sluttiden inte är före starttiden.
                if (bookingEndTime <= bookingStartTime)
                {
                    Console.WriteLine("Sluttiden måste vara efter starttiden!");
                    continue; // Skickar tillbaka användaren till början av loopen.
                }
                TimeSpan latestBookingTime = TimeSpan.FromHours(17);
                if (bookingEndTime > latestBookingTime)
                {
                    Console.WriteLine($"Du kan inte boka rum efter kl 17:00");
                    continue;
                }

                TimeSpan bookingDuration = bookingEndTime - bookingStartTime;
                if (bookingDuration.TotalHours > maxBookingHours)
                {
                    Console.WriteLine($"Du kan inte boka rummet mer än {maxBookingHours} timmar!");
                    continue;
                }

                break; // Lämnar loopen då vi har en giltig tid.
            }
            return (bookingStartTime, bookingEndTime);
        }

        public static void ShowAvailableRooms<T>(string type, List<T> typeOfRoom) where T : Room
        {
            Console.Clear();
            Console.WriteLine($"Tillgängliga {type}:");
            for (int i = 0; i < typeOfRoom.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {typeOfRoom[i].RoomName} Kapacitet: {typeOfRoom[i].RoomCapacity}");
            }
        }
        public static void BackToMenu()
        {
            Thread.Sleep(1000);
            Console.WriteLine("\nTryck ENTER för att återgå till menyn");
            Console.ReadKey();
            Console.Clear();
        }
    }
}