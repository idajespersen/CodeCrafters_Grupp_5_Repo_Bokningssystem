using global::Grupp_5_Bokningssystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Screens
{
        public sealed class AboutInfoScreen : Screen
        {
            // Hardcoded developer names, to be replaced with file persistency
            private readonly string[] members =
            [
                "Daniel Skalk",
            "Ida Hägglund",
            "Sara Sundqvist",
            "Hajdar",
            "Tove Rosén"
            ];

            public override void DisplayMessage(Language language)
            {
                if (DisplayLanguage.Selected == Language.Swedish)
                {
                    Console.WriteLine("Det här programmet skapades av:");
                    Console.WriteLine();
                    Console.WriteLine(" - CodeCrafters-Teamet - ");

                    PrintMembers();

                    Console.WriteLine();
                    Console.Write("Tryck [ENTER] för att återgå till menyn.");
                }
                else
                {
                    Console.WriteLine("This program was created by:");
                    Console.WriteLine();
                    Console.WriteLine(" - CodeCrafters-Team - ");

                    PrintMembers();

                    Console.WriteLine();
                    Console.Write("Press [ENTER] to return to the menu.");
                }
            }

            protected override void HandleInput(string inputString)
            {
                ScreenManager.Instance.Pop();
            }

            private void PrintMembers()
            {
                for (int i = 0; i < members.Length; i++)
                {
                    Console.WriteLine(members[i]);
                }
            }
        }
    }