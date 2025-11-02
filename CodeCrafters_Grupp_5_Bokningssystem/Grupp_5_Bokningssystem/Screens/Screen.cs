using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public abstract class Screen
    {
        public void HandleInput()
        {
            string? str = Console.ReadLine();

            HandleInput(str!);
        }

        public abstract void DisplayMessage(Language language);

        protected abstract void HandleInput(string inputString);
    }
}
