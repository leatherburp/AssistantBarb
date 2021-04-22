using System;
using static System.Console;
namespace AssistantBarb
{
    public class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;
        private string Art;

        public Menu(string prompt, string[] options, string art = null)
        {
            Prompt = prompt;
            Options = options;
            Art = art;
            SelectedIndex = 0;
        }

        private void DisplayOptions()
        {
            WriteLine(Art);
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Magenta;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;

                }


                WriteLine($"{prefix} << {currentOption} >>");
            }
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                //Update selectedIndex based on arrow
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
