using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public sealed class ScreenManager
    {
        private readonly List<Screen> _screens = new List<Screen>();

        public ScreenManager()
        {
        }

        public int ScreenCount
        {
            get { return _screens.Count; }
        }

        /// <summary>
        /// Display and handle Input for the top Screen.
        /// </summary>
        public void DisplayTopScreen()
        {
            Console.Clear();

            Screen? screen = GetTopScreen();

            if (screen != null)
            {
                screen.DisplayMessage(BookingApp.Language);
                screen.HandleInput();
            }
        }

        /// <summary>
        /// Get the top Screen to be displayed.
        /// </summary>
        /// <returns>The top Screen. Null if there are none.</returns>
        public Screen? GetTopScreen()
        {
            if (_screens.Count > 0)
            {
                return _screens[_screens.Count - 1];
            }

            return null;
        }

        /// <summary>
        /// Add a Screen to the top of the list and display the top Screen.
        /// </summary>
        public void Add(Screen screen)
        {
            // Prevent the screen to be added more than once.
            if (_screens.Contains(screen))
                return;

            // Add the screen onto the front of the list
            _screens.Add(screen);
            screen.Initialize(this);
        }

        /// <summary>
        /// Remove a Screen from the list and display the new top Screen if the previous top Screen was removed.
        /// </summary>
        /// <returns>True if removal was successful.</returns>
        public bool Remove(Screen screen)
        {
            int index = _screens.IndexOf(screen);

            if(index >= 0)
            {
                _screens.RemoveAt(index);

                // Check if this is the top Screen.
                if(index == _screens.Count - 1)
                {
                    // There could be a new top Screen if the list is not empty.
                    DisplayTopScreen();
                }

                // Removal was successful
                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears the list and clears the console.
        /// </summary>
        public void Clear()
        {
            _screens.Clear();
            Console.Clear();
        }
    }
}
