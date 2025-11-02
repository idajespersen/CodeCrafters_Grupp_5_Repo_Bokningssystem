using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp_5_Bokningssystem.Screens;

namespace Grupp_5_Bokningssystem
{
    public sealed class BookingApp
    {
        public static Language Language
        {
            get;
            set;
        } = Language.Swedish;


        private bool _isRunning = false;
        private readonly ScreenManager _screenManager;

        public BookingApp(ScreenManager screenManager)
        {
            _screenManager = screenManager;
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception">Throws an exception if the app is currently running.</exception>
        public void Run()
        {
            if (_isRunning)
                throw new Exception("App is already running.");

            _isRunning = true;

            while(_isRunning && _screenManager.ScreenCount > 0)
            {
                _screenManager.DisplayTopScreen();
            }

            OnStop();
        }

        /// <exception cref="Exception">Throws an exception if the app is not currently running.</exception>
        public void Stop()
        {
            if (!_isRunning)
                throw new ArgumentException("App is not currently running.");

            OnStop();
        }

        private void OnStop()
        {
            if (!_isRunning)
                return;

            Save();

            _isRunning = false;
        }

        /// <summary>
        /// Save changes to the program
        /// </summary>
        public void Save()
        {

        }
    }
}
