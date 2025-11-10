using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Screens
{

        public class ScreenManager
        {
            private static ScreenManager _instance = null!;

            public static ScreenManager Instance
            {
                get
                {
                    _instance ??= new ScreenManager();

                    return _instance;
                }
            }

            private readonly Stack<Screen> _screens;

            private ScreenManager()
            {
                _screens = new Stack<Screen>();
            }

            public int ScreenCount
            {
                get { return _screens.Count; }
            }

            public void ReuseTopScreen()
            {
                DisplayCurrentScreen();
            }

            public void Push(Screen screen)
            {
                // Push the screen onto the top of the stack
                _screens.Push(screen);
                DisplayCurrentScreen();
            }

            public Screen? Pop()
            {
                Screen? screen = null;

                // Pop if there are any items in the stack
                if (_screens.Count > 0)
                {
                    screen = _screens.Pop();
                    DisplayCurrentScreen();
                }

                return screen;
            }

            public void Clear()
            {
                _screens.Clear();
                Console.Clear();
            }

            public void DisplayCurrentScreen()
            {
                Console.Clear();

                if (_screens.Count > 0)
                {
                    Screen screen = _screens.Peek();

                    screen.DisplayMessage(DisplayLanguage.Selected);
                    screen.HandleInput();
                }
            }
        }
    }