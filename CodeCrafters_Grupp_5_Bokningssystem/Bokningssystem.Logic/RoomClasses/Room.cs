using Bokningssystem.Logic.Helpers;
using Bokningssystem.Logic.BookingClass;
using Bokningssystem.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Rooms
{
    // Basklass som implementerar IBookable interface.
    public class Room : IBookable
    {
        public List<Booking> bookings = new List<Booking>();
        // Rummets namn.
        public string RoomName { get; }
        // Rummets kapacitet.
        public int RoomCapacity { get; }
        // Rummets tillgänglighet.
        bool RoomIsAvailable { get; }
        // Parent class accepts name, capacity and availability as parameters
        // Methods for IBookable interface implemented with NotImplementedException
        // Konstruktor för att skapa rum.
        public virtual int MaxBookingHours => 8;
        public Room(string roomName, int roomCapacity, bool RoomIsAvailable)
        {
            RoomName = roomName;
            RoomCapacity = roomCapacity;
            this.RoomIsAvailable = RoomIsAvailable;
            // Registrerar rummet i den globala listan över rum.
            RoomRegistry.RegisterRoom(this);

        }
        // Metod för att skapa bokning. Gjord av Sara.
        public virtual void NewBooking()
        {
            // Skapar ett cultureinfo objekt för att läsa av lokal kultur för datum.
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // Användaren skriver in sitt namn
            Console.Write("Ange namn på den som bokar: ");
            string? bookerNameInput = Console.ReadLine().Trim();
            // Kontrollerar om namnet är tomt/whitespace. 
            if (string.IsNullOrWhiteSpace(bookerNameInput))
            {
                // Skriver ut felmeddelande och användaren skickas tillbaka till bokningsmenyn.
                Console.WriteLine("Du måste ange ett namn för att boka ett rum!");
                return;
            }
            // Formaterar namn så första bokstaven är stor och resten små.
            string bookerName = char.ToUpper(bookerNameInput[0]) + bookerNameInput.Substring(1).ToLower();
            DateTime bookingDate = Helper.ParseDateTime($"\nAnge datum för bokning ({currentCulture.DateTimeFormat.ShortDatePattern}): ", currentCulture);
            // Använder ReadTimeSpan metod för att läsa in bokningens start- och sluttid.
            var (bookingStartTime, bookingEndTime) = Helper.ReadTimeSpan(bookingDate, MaxBookingHours);


            // Kombinerar datum och tider till DateTime objekt för bokningen.
            DateTime startTime = bookingDate + bookingStartTime;
            DateTime endTime = bookingDate + bookingEndTime;

            // Skapar nytt Booking objekt.
            Booking newBooking = new Booking(bookerName, RoomName, startTime, endTime);
            // Kontrollerar så att den nya bokningen inte överlappar med befintliga bokningar.
            bool overlap = bookings.Any(b => b.BookingsOverlap(newBooking));
            if (overlap)
            {
                Console.Clear();
                Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                Helper.BackToMenu();
                return; // Avbryter bokningen. Returnerar tillbaka till bokningsmenyn.
            }
            // Om bokningen inte överlappar med någon annan läggs den till i bookings listan.
            bookings.Add(newBooking);
            Console.Clear();
            Console.WriteLine($"\nBokning skapad: {bookerName} har bokat {RoomName} {startTime.ToString("g", currentCulture)}–{endTime.ToString("t", currentCulture)}");
        }
        // Metod för att ta bort bokning. Gjord av Sara.
        public void CancelBooking()
        {
            // Felmeddelande om det inte gjorts några bokningar på det valda rummet.
            if (bookings.Count == 0)
            {
                Console.WriteLine($"Det finns inga bokningar för {RoomName}");
                return; // Skickar tillbaka användaren till föregående meny.
            }
            // Visar bokningar på det valda rummet.
            Console.WriteLine($"Bokningar för {RoomName}:");
            for (int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {bookings[i].BookerName} - {bookings[i].StartTime} - {bookings[i].EndTime}");
            }
            // Använder metod ParseInt för att be användaren välja en bokning att ta bort.
            int removeBookingChoice = Helper.ParseInt("Ange vilken bokning du vill ta bort: ", 1, bookings.Count);
            // Variabel för att ta bort bokning. - 1 för att nå rätt index.
            Booking bookingToRemove = bookings[removeBookingChoice - 1];
            //Ber användaren bekräfta att bokningen ska tas bort.
            Console.WriteLine($"Är du säker på att du vill ta bort bokningen för {bookingToRemove.BookerName} - {bookingToRemove.StartTime} - {bookingToRemove.EndTime}? (Ja/Nej)");
            string confirmRemove = Console.ReadLine().Trim().ToLower();
            // Om användaren skriver ja eller j tas bokningen bort.
            // Annars avbryts det.
            if (confirmRemove == "j" || confirmRemove == "ja")
            {
                bookings.Remove(bookingToRemove);
                Console.WriteLine($"Bokningen har tagits bort");
            }
            else
            {
                Console.WriteLine($"Borttagning har avbrutits");
            }
        }

        public void ListBookings()
        {
            throw new NotImplementedException();
        }

        public void ListBookingsByYear()
        {
            throw new NotImplementedException();
        }


        public void NewRoom()
        {
            throw new NotImplementedException();
        }

        public void SearchRoom()
        {
            throw new NotImplementedException();
        }

        public void UpdateBooking()
        {
            throw new NotImplementedException();
        }
    }

}
