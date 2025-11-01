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
        }

        public Screen? Parent
        {
            get;
        }

        public abstract void DisplayMessage(Language language);
    }
}
