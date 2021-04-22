using System;
using static System.Console;
namespace AssistantBarb
{
    public class Item
    {
        public string Name { get; private set; }
        private int LikeLevel;
        private string Art;
        private ConsoleColor Color;
        

        public Item(string name, int likeLevel, string art, ConsoleColor color)
        {
            Name = name;
            LikeLevel = likeLevel;
            Art = art;
            Color = color;

            
        }


        public void DisplayInfo()
        {
            ForegroundColor = Color;
            WriteLine(Art);
            ResetColor();
            Write("You see a ");
            BackgroundColor = Color;
            Write(Name);
            ResetColor();
            WriteLine(" in front of you.");
            WriteLine($"Nicki's like level of this item is: {LikeLevel}.");
        }

        public void GetItem()
        {
            WriteLine("Would you like this item? (yes/no)");
            string response = ReadLine().ToLower().Trim();
            if (response == "yes")
            {
                
            }
        }


    }
}
