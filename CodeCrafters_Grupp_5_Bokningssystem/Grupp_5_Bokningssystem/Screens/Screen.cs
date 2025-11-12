using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public abstract class Screen
    {
        /// <summary>
        /// Manager to handle this Screen class.
        /// Helper methods Display(Screen) and Close() calls the ScreenManager methods.
        /// </summary>
        private ScreenManager _screenManager = null!;

        /// <summary>
        /// Requests the user to input something in the console.
        /// </summary>
        public void HandleInput()
        {
            string? str = Console.ReadLine();

            if (str == null)
                return;

            HandleInput(str);
        }

        /// <summary>
        /// Initializes the Screen
        /// </summary>
        /// <exception cref="Exception">Throws Exception if already initialized.</exception>
        public void Initialize(ScreenManager manager)
        {
            if (_screenManager != null)
                throw new Exception("Screen has already been initialized.");

            _screenManager = manager;

            OnInitialized();
        }

        /// <summary>
        /// Display a Screen.
        /// </summary>
        /// <exception cref="NullReferenceException">Throws Exception if ScreenManager is null.</exception>
        public void DisplayScreen(Screen screen)
        {
            if (_screenManager == null)
                throw new NullReferenceException(nameof(ScreenManager));

            _screenManager.Add(screen);
        }

        /// <summary>
        /// Close the Screen and remove it from the ScreenManager.
        /// </summary>
        /// <returns>True if the removal was successful.</returns>
        /// <exception cref="NullReferenceException">Throws Exception if ScreenManager is null.</exception>
        public bool Close()
        {
            if (_screenManager == null)
                throw new NullReferenceException(nameof(ScreenManager));

            if(_screenManager.Remove(this))
            {
                // Call OnClosed() if the removal was successful.
                OnClosed();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Message to display when showing the Screen.
        /// </summary>
        /// <param name="language">Parameter to control the current language.</param>
        public abstract void DisplayMessage(Language language);

        /// <summary>
        /// Handle a string input from the user.
        /// </summary>
        /// <param name="inputString">The string that the user input.</param>
        protected abstract void HandleInput(string inputString);

        /// <summary>
        /// An underlying method that is called from Initialized(...).
        /// </summary>
        protected virtual void OnInitialized()
        { 
        }

        /// <summary>
        /// An underlying method that is called from Close().
        /// </summary>
        protected virtual void OnClosed()
        {
        }
    }
}
