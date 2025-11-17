using Bokningssystem.Logic.BookingClass;
using Bokningssystem.Logic.HelperMethods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.RoomClasses
{
    // ----------------------------------------------------------------
    //             Parent / Base class Room. Made by Ida.
    // ----------------------------------------------------------------
    // Parent Class: Room inherits from interface IBookable
    public class Room : IBookable
    {
        // Unique empty list for each created room
        //Tove: get/set to be able to Deserialize using json
        public List<Booking> Bookings
        {
            get;
            set;
        } = new List<Booking>();
        // Variable to handle unique ID per room
        public string RoomId { get; }
        // Variable for room name from user input
        public string Name { get; }
        // Variable for room capacity
        public int RoomCapacity { get; }
        // Beräknar ifall rum är tillgängligt
        // (Då aktuell tid inte är mellan pågående boknings start- och sluttid)
        public bool IsCurrentlyAvailable
        {
            get
            {
                DateTime now = DateTime.Now;
                return !Bookings.Any(b => now >= b.StartTime && now < b.EndTime);
            }
        }
        // Standardvärde för max antal timmar ett rum kan bokas.
        // Använd override för att ändra individuellt för olika typer rum/subklasser.
        public virtual int MaxBookingHours => 8;

        // Room Constructor
        // Parent class accepts id, name, and capacity as parameters
        public Room(string roomId, string name, int roomCapacity)
        {
            RoomId = roomId;
            Name = name;
            RoomCapacity = roomCapacity;
        }
        // ----------------------------------------------------------------
        //           Metod för att skapa bokning. Gjord av Sara.
        // ----------------------------------------------------------------
        public void NewBooking()
        {
            
            // Läser in användarens namn.
            string bookerName = InputHelper.ReadName("Ange namn: ");
            // Läser in bokningens datum utefter lokal kultur.
            DateTime bookingDate = InputHelper.ParseDateTime($"Ange bokningsdatum", CultureInfo.CurrentCulture);
            // Läser in bokningens start- och sluttid.
            var (bookingStartTime, bookingEndTime) = InputHelper.ReadTimeSpan("Ange starttid", "Ange sluttid", bookingDate, MaxBookingHours, CultureInfo.CurrentCulture);
            // Kombinerar datum och tider till DateTime objekt för bokningen.
            DateTime startTime = bookingDate + bookingStartTime;
            DateTime endTime = bookingDate + bookingEndTime;

            Booking newBooking = new Booking(bookerName, Name, startTime, endTime);
            // Kontrollerar så att den nya bokningen inte överlappar med befintliga bokningar.
            bool overlap = Bookings.Any(b => b.BookingsOverlap(newBooking));
            if (overlap)
            {
                Console.Clear();
                Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                return; 
            }
            // Lägger till bokning.
            Bookings.Add(newBooking);
            RoomRegistry.SaveRoom(this);
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║          Bokning skapad        ║");
            Console.WriteLine("╚════════════════════════════════╝\n");
            Console.WriteLine($"Namn: {bookerName} ");
            Console.WriteLine($"Rum: {Name} ");
            Console.WriteLine($"Datum: {startTime:dd MMMM yyyy} ");
            Console.WriteLine($"Tid: {startTime:HH\\:mm}-{endTime:HH\\:mm}");
            if (newBooking.Duration.Minutes == 0)
            {
                Console.WriteLine($"Bokningslängd: {newBooking.Duration.Hours} timmar ");
            }
            else if (newBooking.Duration.Hours == 0)
            {
                Console.WriteLine($"Bokningslängd: {newBooking.Duration.Minutes} minuter ");
            }
            else
            {
                Console.WriteLine($"Bokningslängd: {newBooking.Duration.Hours} timmar {newBooking.Duration.Minutes} minuter ");
            }
        }

        // ----------------------------------------------------------------
        //           Metod för att ta bort bokning. Gjord av Sara.
        // ----------------------------------------------------------------
        public void CancelBooking()
        {
            MenuHelper.CancelBookingMenu();
            
            if (Bookings.Count == 0)
            {
                Console.WriteLine($"Det finns inga bokningar för {Name}");
                return; 
            }
            
            Console.WriteLine($"Bokningar för {Name}:");
            MenuHelper.ShowBookingsForRoom(this);

            // Läser in användarens val av bokning som ska tas bort.
            int removeInput = InputHelper.ParseInt("Ange bokningen du vill ta bort: ", 1, Bookings.Count);
            // Variabel för att ta bort bokning. - 1 för att nå rätt index.
            Booking bookingToRemove = Bookings[removeInput - 1];
            // Användaren får bekräfta borttagning av bokning.
            MenuHelper.CancelBookingMenu();
            if (InputHelper.ConfirmAction($"ta bort bokningen för {bookingToRemove.BookerName} - {bookingToRemove.StartTime.Day} {bookingToRemove.StartTime:MMMM} {bookingToRemove.StartTime.Year} kl {bookingToRemove.StartTime:HH\\:mm}-{bookingToRemove.EndTime:HH\\:mm}", "Borttagning har avbrutits"))
            {
                Bookings.Remove(bookingToRemove);
                Console.WriteLine($"\nBokningen har tagits bort");
                RoomRegistry.SaveRoom(this);
            }
        }
        // ----------------------------------------------------------------
        //           Metod för att uppdatera bokning. Gjord av Sara.
        // ----------------------------------------------------------------
        public void UpdateBooking()
        {
            bool updateMenuActive = true; 
            while (updateMenuActive)
            {
                MenuHelper.UpdateBookingMenu();
                if (Bookings.Count == 0)
                {
                    Console.WriteLine($"Det finns inga bokningar för {Name}");
                    return; 
                }

                Console.WriteLine($"Bokningar för {Name}:");
                MenuHelper.ShowBookingsForRoom(this);
                // Läser in användarens val av bokning som ska uppdateras.
                int updateInput = InputHelper.ParseInt("\nAnge bokningen du vill uppdatera: ", 1, Bookings.Count);
                // Variabel för att uppdatera bokning. - 1 för att nå rätt index.
                Booking bookingToUpdate = Bookings[updateInput - 1];

                MenuHelper.UpdateBookingMenuChoices();

                // Läser in användarens val av vad som ska ändras.
                int updateMenuChoice = InputHelper.ParseInt("", 0, 2);
                switch (updateMenuChoice)
                {
                    case 1:
                        // --------------------
                        //   Uppdatera namn
                        // --------------------
                        MenuHelper.UpdateBookingMenu();
                        // Läser in nytt namn.
                        string newBookerName = InputHelper.ReadName("Ange nytt namn: ");
                        string confirmMessageName = $"ändra bokningen: " +
                           $"\n\nFrån: " +
                           $"\nNamn: {bookingToUpdate.BookerName} " +
                           $"\nDatum: {bookingToUpdate.StartTime:dd MMMM yyyy} " +
                           $"\nTid {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm}" +
                           $"\n\nTill: " +
                           $"\nNamn: {newBookerName} " +
                           $"\nDatum: {bookingToUpdate.StartTime:dd MMMM yyyy} " +
                           $"\nTid {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm} " +
                           $"\n\nSvara ";
                        MenuHelper.UpdateBookingMenu();
                        // Användaren får bekräfta ändring av namn.
                        if (InputHelper.ConfirmAction(confirmMessageName, "\nUppdatering av namn har avbrutits."))
                        {
                            bookingToUpdate.BookerName = newBookerName;
                            Console.WriteLine("Namnet på bokningen har uppdaterats!");
                            RoomRegistry.SaveRoom(this);
                        }
                        break;
                    case 2: 
                        
                        DateTime newStartTime = bookingToUpdate.StartTime;
                        DateTime newEndTime = bookingToUpdate.EndTime;
                        MenuHelper.UpdateBookingMenuChoicesDateAndTime();
                        // Läser in användarens val av vad som ska ändras.
                        int updateMenuChoice2 = InputHelper.ParseInt("", 0, 2);
                        switch (updateMenuChoice2)
                        {
                                // --------------------
                                //   Uppdatera datum
                                // --------------------
                            case 1:
                                MenuHelper.UpdateBookingMenu();
                                // Läser in nytt datum.
                                DateTime newBookingDate = InputHelper.ParseDateTime($"Ange nytt datum för bokning: ", CultureInfo.CurrentCulture);
                                // Kombinerar datum och tider till ett nytt DateTime objekt för bokningen.
                                newStartTime = newBookingDate + newStartTime.TimeOfDay;
                                newEndTime = newBookingDate + newEndTime.TimeOfDay;
                                // Skapar temporärt Booking objekt för att kolla om ny bokning överlappar med befintliga bokningar.
                                Booking tempBooking = new Booking(bookingToUpdate.BookerName, Name, newStartTime, newEndTime);
                                bool overlap = Bookings.Any(b => b != bookingToUpdate && b.BookingsOverlap(tempBooking));
                                if (overlap)
                                {
                                    Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                                    MenuHelper.BackToMenu("till menyn...");
                                }
                                string confirmMessageDate = $"ändra bokningen: " +
                             $"\n\nFrån: " +
                             $"\nNamn: {bookingToUpdate.BookerName} " +
                             $"\nDatum: {bookingToUpdate.StartTime:dd MMMM yyyy} " +
                             $"\nTid {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm}" +
                             $"\n\nTill: " +
                             $"\nNamn: {bookingToUpdate.BookerName} " +
                             $"\nDatum: {newStartTime:dd MMMM yyyy} " +
                             $"\nTid {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm} " +
                             $"\n\nSvara ";
                                MenuHelper.UpdateBookingMenu();
                                // Användaren får bekräfta ändring av datum.
                                if (InputHelper.ConfirmAction(confirmMessageDate, "\nUppdatering av datum har avbrutits."))
                                {
                                    bookingToUpdate.StartTime = newStartTime;
                                    bookingToUpdate.EndTime = newEndTime;
                                    Console.WriteLine("\nDatumet på bokningen har uppdaterats!");
                                    RoomRegistry.SaveRoom(this);
                                }
                                break;
                            // --------------------
                            //   Uppdatera tider
                            // --------------------
                            case 2:
                                MenuHelper.UpdateBookingMenu();
                                // Läser in ny start- och sluttid.
                                var (newbookingStartTime, newBookingEndTime) = InputHelper.ReadTimeSpan("Ange ny starttid", "Ange ny sluttid", bookingToUpdate.StartTime.Date, MaxBookingHours, CultureInfo.CurrentCulture);
                                // Kombinerar datum och tider till ett nytt DateTime objekt för bokningen.
                                newStartTime = newStartTime.Date + newbookingStartTime;
                                newEndTime = newEndTime.Date + newBookingEndTime;
                                // Skapar temporärt Booking objekt för att kolla om ny bokning överlappar med befintliga bokningar.
                                Booking tempBooking2 = new Booking(bookingToUpdate.BookerName, Name, newStartTime, newEndTime);
                                overlap = Bookings.Any(b => b != bookingToUpdate && b.BookingsOverlap(tempBooking2));
                                if (overlap)
                                {
                                    Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                                    MenuHelper.BackToMenu("till menyn...");
                                }
                                string confirmMessageTime = $"ändra bokningen: " +
                          $"\n\nFrån: " +
                          $"\nNamn: {bookingToUpdate.BookerName} " +
                          $"\nDatum: {bookingToUpdate.StartTime:dd MMMM yyyy} " +
                          $"\nTid {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm}" +
                          $"\n\nTill: " +
                          $"\nNamn: {bookingToUpdate.BookerName} " +
                          $"\nDatum: {bookingToUpdate.StartTime:dd MMMM yyyy} " +
                          $"\nTid {newStartTime:HH\\:mm}-{newEndTime:HH\\:mm} " +
                          $"\n\nSvara ";
                                // Användaren får bekräfta ändring av tider.
                                if (InputHelper.ConfirmAction(confirmMessageTime, "\nUppdatering av tid har avbrutits."))
                                {
                                    bookingToUpdate.StartTime = newStartTime;
                                    bookingToUpdate.EndTime = newEndTime;
                                    RoomRegistry.SaveRoom(this);
                                    Console.WriteLine("\nTiden på bokningen har uppdaterats!");
                                }
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                updateMenuActive = false; 
                                break;
                            default: 
                                MenuHelper.DisplayMessage(0,2);
                                MenuHelper.BackToMenu("till menyn...");
                                break;
                        }
                        break;
                    case 0: // Om användaren vill återgå till huvudmenyn
                        updateMenuActive = false;
                        break;
                    default: 
                        MenuHelper.DisplayMessage(0, 2);
                        MenuHelper.BackToMenu("till menyn...");
                        break;
                }
                break;
            }
        }

        // - List all bookings
        public void ListBookings()
        {
            throw new NotImplementedException();
        }
        // - List bookings from a specific year
        // Will be inside the ListBookings in menu
        public void ListBookingsByYear()
        {
            throw new NotImplementedException();
        }
    }
}

