using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Screens
{
    public sealed class RoomMenuScreen : NumberSelectionScreen
    {
        public RoomMenuScreen()
            : base(2)
        {
        }

        public override void DisplayMessage(Language language)
        {
            if (language == Language.Swedish)
            {
                Console.WriteLine(" - Rumshantering - ");
                Console.WriteLine();
                Console.WriteLine(" [1] - Sök efter rum");
                Console.WriteLine(" [2] - Skapa ett nytt rum");
                Console.WriteLine();
                Console.WriteLine(" [0] - Återgå till menyn");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(" - Rumshantering - ");
                Console.WriteLine();
                Console.WriteLine(" [1] - Sök efter rum");
                Console.WriteLine(" [2] - Skapa ett nytt rum");
                Console.WriteLine();
                Console.WriteLine(" [0] - Återgå till menyn");
                Console.WriteLine();
            }
        }

        public override void HandleChoice(int choice)
        {
            switch (choice)
            {
                case 0:
                    ScreenManager.Instance.Pop();
                    return;
                case 1:
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en SearchRoom finnas!");
                    Console.ReadKey();
                    //SearchRoom();
                    break;
                case 2:
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en NewRoom finnas!");
                    Console.ReadKey();
                    //NewRoom();
                    break;
            }
        }
    }
}