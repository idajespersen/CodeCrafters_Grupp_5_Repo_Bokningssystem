using Bokningssystem.Logic.BookingClass;
using Bokningssystem.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningssystem.Logic.HelperMethods;

namespace Bokningssystem.Logic.Rooms
{
    // Basklass som implementerar IBookable interface.
    public class Room : IBookable
    {
        // Parent class accepts name, capacity and availability as parameters
        public List<Booking> bookings = new List<Booking>();
        public string RoomName { get; }
        public int RoomCapacity { get; }
        bool RoomIsAvailable { get; }
        public virtual int MaxBookingHours => 8;
        // Skapar ett cultureinfo objekt för att läsa av lokal kultur för datum.
        CultureInfo currentCulture = CultureInfo.CurrentCulture;
        public Room(string roomName, int roomCapacity, bool RoomIsAvailable)
        {
            RoomName = roomName;
            RoomCapacity = roomCapacity;
            this.RoomIsAvailable = RoomIsAvailable;
            // Registrerar rummet i den globala listan över rum.
            RoomRegistry.RegisterRoom(this);
        }
        // Metod för att skapa bokning. Gjord av Sara.
        public void NewBooking()
        {   // Använder ReadName metod för att läsa in användarens namn.
            string bookerName = Helper.ReadName("Ange namn på den som bokar: ");
            // Använder ParseDateTime metod för att läsa in bokningens datum.
            DateTime bookingDate = Helper.ParseDateTime($"\nAnge datum för bokning", currentCulture);
            // Använder ReadTimeSpan metod för att läsa in bokningens start- och sluttid.
            var (bookingStartTime, bookingEndTime) = Helper.ReadTimeSpan("Ange starttid", "Ange sluttid", bookingDate, MaxBookingHours, currentCulture);
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
                return; // Avbryter bokningen. Skickar tillbaka användaren till föregående meny.
            }
            // Om bokningen inte överlappar med någon annan läggs den till i bookings listan.
            bookings.Add(newBooking);
            Console.Clear();
            Console.WriteLine($"Bokning skapad:\n{bookerName} har bokat {RoomName} - {startTime.Day} {startTime:MMMM} {startTime.Year} kl {startTime:HH\\:mm}-{endTime:HH\\:mm}.");
        }
        // Metod för att ta bort bokning. Gjord av Sara.
        public void CancelBooking()
        {
            Console.Clear();
            // Felmeddelande om det inte gjorts några bokningar på det valda rummet.
            if (bookings.Count == 0)
            {
                Console.WriteLine($"Det finns inga bokningar för {RoomName}");
                return; // Skickar tillbaka användaren till föregående meny.
            }
            // Visar alla bokningar på det valda rummet.
            Console.WriteLine($"Bokningar för {RoomName}:");
            for (int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {bookings[i].BookerName} - {bookings[i].StartTime.Day} {bookings[i].StartTime:MMMM} {bookings[i].StartTime.Year} kl {bookings[i].StartTime:HH\\:mm} - {bookings[i].EndTime:HH\\:mm}");
            }
            // Använder metod ParseInt för att läsa in användarens val av bokning att ta bort.
            int removeInput = Helper.ParseInt("Ange vilken bokning du vill ta bort: ", 1, bookings.Count);
            // Variabel för att ta bort bokning. - 1 för att nå rätt index.
            Booking bookingToRemove = bookings[removeInput - 1];
            // Ber användaren bekräfta borttagning av bokning. Om de svarar ja så tas bokning bort.
            if (Helper.ConfirmAction($"ta bort bokningen för {bookingToRemove.BookerName} - {bookingToRemove.StartTime.Day} {bookingToRemove.StartTime:MMMM} {bookingToRemove.StartTime.Year} kl {bookingToRemove.StartTime:HH\\:mm}-{bookingToRemove.EndTime:HH\\:mm}", "Borttagning har avbrutits"))
            {
                bookings.Remove(bookingToRemove);
                Console.WriteLine($"Bokningen har tagits bort");
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
            bool updateMenuActive = true; // Variabel som styr loop för menyn.
            while (updateMenuActive)
            {
                Console.Clear();
                // Felmeddelande om det inte gjorts några bokningar på det valda rummet.
                if (bookings.Count == 0)
                {
                    Console.WriteLine($"Det finns inga bokningar för {RoomName}");
                    return; // Skickar tillbaka användaren till föregående meny.
                }

                Console.WriteLine($"Välj bokningen du vill uppdatera:");
                // Visar alla bokningar på det valda rummet.
                Console.WriteLine($"Bokningar för {RoomName}:");
                for (int i = 0; i < bookings.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {bookings[i].BookerName} - {bookings[i].StartTime.Day} {bookings[i].StartTime:MMMM} {bookings[i].StartTime.Year} kl {bookings[i].StartTime:HH\\:mm} - {bookings[i].EndTime:HH\\:mm}");
                }
                // Använder metod ParseInt för att läsa in användarens val av bokning att uppdatera.
                int updateInput = Helper.ParseInt("Ange vilken bokning du vill uppdatera: ", 1, bookings.Count);
                // Variabel för att uppdatera bokning. - 1 för att nå rätt index.
                Booking bookingToUpdate = bookings[updateInput - 1];

                Console.WriteLine("Vad vill du ändra?");
                Console.WriteLine("[1] Ändra namn på bokningen");
                Console.WriteLine("[2] Ändra datum/tid på bokningen");
                Console.WriteLine("[0] Återgå till bokningsmenyn");
                // Använder metod ParseInt för att läsa in användarens val av vad som ska ändras.
                int updateMenuChoice = Helper.ParseInt("", 0, 2);
                switch (updateMenuChoice)
                {
                    case 1: // Om användaren väljer att ändra namn.
                        Console.Clear();
                        // Använder metod ReadName för att läsa in nytt namn.
                        string newBookerName = Helper.ReadName("Ange nytt namn på den som bokar: ");
                        // Ber användaren bekräfta uppdatering av bokning. Om de svarar ja så uppdateras namnet.
                        if (Helper.ConfirmAction($"ändra namnet för bokning {bookingToUpdate.BookerName} - {bookingToUpdate.StartTime.Day} {bookingToUpdate.StartTime:MMMM} {bookingToUpdate.StartTime.Year}  kl {bookingToUpdate.StartTime:HH\\:mm}  {bookingToUpdate.EndTime:HH\\:mm} till {newBookerName}", "Uppdatering av namn har avbrutits"))
                        {
                            bookingToUpdate.BookerName = newBookerName;
                            Console.WriteLine("Namnet på bokningen har uppdaterats!");
                        }
                        break;
                    case 2: // Om användaren väljer att ändra tid/datum.
                        // Skapar nya DateTime objekt med samma värde som bokningen som ska uppdateras.
                        DateTime newStartTime = bookingToUpdate.StartTime;
                        DateTime newEndTime = bookingToUpdate.EndTime;
                        Console.Clear();
                        Console.WriteLine("Vad vill du ändra?");
                        Console.WriteLine("[1] Ändra datum på bokningen");
                        Console.WriteLine("[2] Ändra tid på bokningen");
                        Console.WriteLine("[0] Återgå till bokningsmenyn");
                        // Använder metod ParseInt för att läsa in användarens val av vad som ska ändras.
                        int updateMenuChoice2 = Helper.ParseInt("", 0, 2);
                        switch (updateMenuChoice2)
                        {
                            case 1: // Om användaren väljer att ändra datum.
                                Console.Clear();
                                // Använder ParseDateTime metod för att läsa in nytt datum.
                                DateTime newBookingDate = Helper.ParseDateTime($"\nAnge nytt datum för bokning (Format: {currentCulture.DateTimeFormat.ShortDatePattern}): ", currentCulture);
                                // Kombinerar datum och tider till ett nytt DateTime objekt för bokningen.
                                newStartTime = newBookingDate + newStartTime.TimeOfDay;
                                newEndTime = newBookingDate + newEndTime.TimeOfDay;
                                // Skapar temporärt Booking objekt för att kolla om ny bokning överlappar med befintliga bokningar.
                                Booking tempBooking = new Booking(bookingToUpdate.BookerName, RoomName, newStartTime, newEndTime);
                                bool overlap = bookings.Any(b => b != bookingToUpdate && b.BookingsOverlap(tempBooking));
                                if (overlap)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                                    Helper.BackToMenu();
                                }
                                // Ber användaren bekräfta uppdatering av bokning. Om de svarar ja så uppdateras datumet.
                                if (Helper.ConfirmAction($"ändra datum för bokning {bookingToUpdate.BookerName} - {bookingToUpdate.StartTime.Day} {bookingToUpdate.StartTime:MMMM} {bookingToUpdate.StartTime.Year} kl {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm} till {newStartTime.Day} {newStartTime:MMMM} {newStartTime.Year}", "Uppdatering av datum har avbrutits"))
                                {
                                    bookingToUpdate.StartTime = newStartTime;
                                    bookingToUpdate.EndTime = newEndTime;
                                    Console.WriteLine("Datumet på bokningen har uppdaterats!");
                                }
                                break;
                            case 2: // Om användaren väljer att ändra tid.
                                Console.Clear();
                                // Använder ReadTimeSpan metod för att läsa in ny start- och sluttid.
                                var (newbookingStartTime, newBookingEndTime) = Helper.ReadTimeSpan("Ange ny starttid", "Ange ny sluttid", bookingToUpdate.StartTime.Date, MaxBookingHours, currentCulture);
                                // Kombinerar datum och tider till ett nytt DateTime objekt för bokningen.
                                newStartTime = newStartTime.Date + newbookingStartTime;
                                newEndTime = newEndTime.Date + newBookingEndTime;
                                // Skapar temporärt Booking objekt för att kolla om ny bokning överlappar med befintliga bokningar.
                                Booking tempBooking2 = new Booking(bookingToUpdate.BookerName, RoomName, newStartTime, newEndTime);
                                overlap = bookings.Any(b => b != bookingToUpdate && b.BookingsOverlap(tempBooking2));
                                if (overlap)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                                    Helper.BackToMenu();
                                }
                                // Ber användaren bekräfta uppdatering av bokning. Om de svarar ja så uppdateras tiden.
                                if (Helper.ConfirmAction($"ändra tiden för bokning {bookingToUpdate.BookerName} - {bookingToUpdate.StartTime.Day} {bookingToUpdate.StartTime:MMMM} {bookingToUpdate.StartTime.Year} kl {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm} till  {newStartTime} - {newEndTime}", "Uppdatering av tid har avbrutits"))
                                {
                                    bookingToUpdate.StartTime = newStartTime;
                                    bookingToUpdate.EndTime = newEndTime;

                                    Console.WriteLine("Tiden på bokningen har uppdaterats!");
                                }
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                updateMenuActive = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.Clear();
                                Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                                Helper.BackToMenu();
                                break;
                        }
                        break;
                    case 0: // Om användaren vill återgå till huvudmenyn
                        updateMenuActive = false; // While loopen avslutas.
                        break;
                    default: // Om användaren skriver in något annat än 1, 2 eller 0.
                        Console.Clear();
                        Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                        Helper.BackToMenu();
                        break;
                }
                break;
            }
        }
    }
}



