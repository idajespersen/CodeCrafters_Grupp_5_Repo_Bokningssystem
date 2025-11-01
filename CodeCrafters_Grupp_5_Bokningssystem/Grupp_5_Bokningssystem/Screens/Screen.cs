using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public abstract class Screen
    {
        protected Screen(Screen? parent)
        {
            Parent = parent;
            DisplayMessage(DisplayLanguage.Selected);
        }

        public Screen? Parent
        {
            get;
        }

        public void HandleInput()
        {
            string? str = Console.ReadLine();

            if (!string.IsNullOrEmpty(str))
                HandleInput(str);
        }

        public abstract void DisplayMessage(Language language);

        protected abstract void HandleInput(string inputString);
    }
}
