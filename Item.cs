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
    }
}
