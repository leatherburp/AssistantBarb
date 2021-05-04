using System;
using static System.Console;
namespace AssistantBarb
{
    public class Player
    {
        public int Score = 0;
        public bool TookHovLane = false;
        public Location Destination;
        public Item Slot1;
        public Item Slot2;
        public Item Slot3;
        public int X { get; set; }
        public int Y { get; set; }
        private string PlayerMarker;
        private ConsoleColor PlayerColor;

        public Player(int initialX, int initialY)
        {
            X = initialX;
            Y = initialY;
            PlayerMarker = "█";
            PlayerColor = ConsoleColor.Magenta;
        }

        public void Draw()
        {
            ForegroundColor = PlayerColor;
            SetCursorPosition(X, Y);
            Write(PlayerMarker);
            ForegroundColor = ConsoleColor.White;
        }

        public void DisplayPlayerInfo()
        {
            WriteLine($"The player's current score is: {Score}.");
            WriteLine($"Did the player take the HOV Lane? {TookHovLane}");

            if (Slot1 != null && Slot2 != null) { WriteLine($"The player currently has: {Slot1.Name} and {Slot2.Name} in their inventory."); }
        }

        public void PickUpDrink(Item drink)
        {
            Slot1 = drink;
        }

        public void PickUpWig(Item wig)
        {
            Slot2 = wig;
        }

        public void Promote(Item promotion)
        {
            Slot3 = promotion;
        }

       public void SetLocation(Location place)
        {
            Destination = place;
        }


    }
}
