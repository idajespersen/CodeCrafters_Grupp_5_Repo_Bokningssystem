=========================================
              DOKUMENTATION
=========================================

                KOM IGÅNG
=========================================

Öppna projektet i Visual Studio eller annan C#-IDE som stödjer .NET.

Starta programmet genom att köra Program.cs (Klicka F5 eller Start knappen).

Vid uppstart laddar programmet automatiskt in sparade rum och bokningar via RoomRegistry.LoadRooms().


               ANVÄNDNING
=========================================
Då programmet körs visas huvudmenyn: 

[1] Bokningshantering
Här kan användaren:

- Skapa ny bokning

- Ta bort en bokning

- Uppdatera en befintlig bokning

- Visa alla bokningar 

- Lista bokningar per år

[2] Rumshantering
Här kan användaren:

- Skapa nytt rum (klassrum eller grupprum)

- Visa alla registrerade rum

[3] Om programmet

- Visar credits för programmet.

[0] Avsluta programmet

- Stänger applikationen.

Flödet för bokningar:

1. Välj ett rum.

2. Ange namn på den som bokar.

3. Ange datum.

4. Ange start- och sluttid.

5. Programmet kontrollerar så att tiden är ledig.

6. Bokningen sparas som JSON och kopplas till det valda rummet.

           KÄNDA BEGRÄNSNINGAR
=========================================

Ingen hantering av att två personer försöker boka samtidigt (ifall programmet skull användas av flera personer).

Programmet körs enbart i konsolmiljö, ingen GUI-version finns.


             ANSVARSOMRÅDEN
=========================================
