using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace AssistantBarb
{
    class ConsoleUtils
    {


        public static void Continue()
        {
            WriteLine("\nPress any key to continue...");
            ReadKey(true);
        }

        public static void Exit()
        {
            WriteLine("\nPress any key to exit...");
            ReadKey(true);
        }

        public static void SetWinSize(int width, int height)
        {
            int desiredWidth = width;
            int desiredHeight = height;
            try
            {
                WindowHeight = desiredHeight;
                WindowWidth = desiredWidth;
            }
            catch (PlatformNotSupportedException error)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("-- Warning --");
                WriteLine("You are on an operating system that doesnt support certain cosmetic lines of code like the game's desired width and height.");
                WriteLine("Things may not look quite right, unless you adjust the console window yourself.");
                ForegroundColor = ConsoleColor.White;
                ConsoleUtils.Continue();
                Clear();
            }
            catch (System.ArgumentOutOfRangeException error)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("-- Warning --");
                WriteLine("Your screen isn't big enough to match the game's desired width and height.");
                WriteLine("Things may not look quite right, unless you adjust the text size in your console window.");
                ForegroundColor = ConsoleColor.White;
                ConsoleUtils.Continue();
                Clear();
            }
        }
    }
}
