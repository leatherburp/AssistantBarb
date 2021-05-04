using System;
using static System.Console;
using System.IO;
using System.Threading;
namespace AssistantBarb
{
    public class Game
    {
        Item RedSlurpee = new Item("Red Slurpee", 1, ArtAssets.SlurpeeArt, ConsoleColor.Red);
        Item PinkDrink = new Item("Pink Drink", 2, ArtAssets.PinkDrinkArt, ConsoleColor.Magenta);
        Item MonsterEnergy = new Item("Monster Energy Drink", 0, ArtAssets.MonsterEnergyArt, ConsoleColor.Green);
        Item IcedCoffee = new Item("Iced Coffee", 1, ArtAssets.IcedCoffeeArt, ConsoleColor.DarkYellow);
        Item PinkWig = new Item("Pink Wig", 2, ArtAssets.BobArt, ConsoleColor.Magenta);
        Item BlackWig = new Item("Black Wig", 1, ArtAssets.BlackWigArt, ConsoleColor.Gray);
        Item BaldCap = new Item("Baldcap", 0, ArtAssets.BaldCapArt, ConsoleColor.DarkYellow);
        Player CurrentPlayer = new Player(24, 4);
        Location Starbucks = new Location("Starbucks");
        Location ConStore = new Location("convienence store");
        private World MyWorld;
        private static string GameTitleArt = @"
                                                                                                                             
 @@@@@@    @@@@@@    @@@@@@   @@@   @@@@@@   @@@@@@@   @@@@@@   @@@  @@@  @@@@@@@     @@@@@@@    @@@@@@   @@@@@@@   @@@@@@@   
@@@@@@@@  @@@@@@@   @@@@@@@   @@@  @@@@@@@   @@@@@@@  @@@@@@@@  @@@@ @@@  @@@@@@@     @@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@  
@@!  @@@  !@@       !@@       @@!  !@@         @@!    @@!  @@@  @@!@!@@@    @@!       @@!  @@@  @@!  @@@  @@!  @@@  @@!  @@@  
!@!  @!@  !@!       !@!       !@!  !@!         !@!    !@!  @!@  !@!!@!@!    !@!       !@   @!@  !@!  @!@  !@!  @!@  !@   @!@  
@!@!@!@!  !!@@!!    !!@@!!    !!@  !!@@!!      @!!    @!@!@!@!  @!@ !!@!    @!!       @!@!@!@   @!@!@!@!  @!@!!@!   @!@!@!@   
!!!@!!!!   !!@!!!    !!@!!!   !!!   !!@!!!     !!!    !!!@!!!!  !@!  !!!    !!!       !!!@!!!!  !!!@!!!!  !!@!@!    !!!@!!!!  
!!:  !!!       !:!       !:!  !!:       !:!    !!:    !!:  !!!  !!:  !!!    !!:       !!:  !!!  !!:  !!!  !!: :!!   !!:  !!!  
:!:  !:!      !:!       !:!   :!:      !:!     :!:    :!:  !:!  :!:  !:!    :!:       :!:  !:!  :!:  !:!  :!:  !:!  :!:  !:!  
::   :::  :::: ::   :::: ::    ::  :::: ::      ::    ::   :::   ::   ::     ::        :: ::::  ::   :::  ::   :::   :: ::::  
 :   : :  :: : :    :: : :    :    :: : :       :      :   : :  ::    :      :        :: : ::    :   : :   :   : :  :: : ::   
                                                                                                                              
";
        private static string WinArt = @"
                                                              
@@@ @@@   @@@@@@   @@@  @@@     @@@  @@@  @@@  @@@  @@@  @@@  
@@@ @@@  @@@@@@@@  @@@  @@@     @@@  @@@  @@@  @@@  @@@@ @@@  
@@! !@@  @@!  @@@  @@!  @@@     @@!  @@!  @@!  @@!  @@!@!@@@  
!@! @!!  !@!  @!@  !@!  @!@     !@!  !@!  !@!  !@!  !@!!@!@!  
 !@!@!   @!@  !@!  @!@  !@!     @!!  !!@  @!@  !!@  @!@ !!@!  
  @!!!   !@!  !!!  !@!  !!!     !@!  !!!  !@!  !!!  !@!  !!!  
  !!:    !!:  !!!  !!:  !!!     !!:  !!:  !!:  !!:  !!:  !!!  
  :!:    :!:  !:!  :!:  !:!     :!:  :!:  :!:  :!:  :!:  !:!  
   ::    ::::: ::  ::::: ::      :::: :: :::    ::   ::   ::  
   :      : :  :    : :  :        :: :  : :    :    ::    :   
                                                              
";
        private static string LoseArt = @"
                                                                                       
 @@@@@@@@   @@@@@@   @@@@@@@@@@   @@@@@@@@      @@@@@@   @@@  @@@  @@@@@@@@  @@@@@@@   
@@@@@@@@@  @@@@@@@@  @@@@@@@@@@@  @@@@@@@@     @@@@@@@@  @@@  @@@  @@@@@@@@  @@@@@@@@  
!@@        @@!  @@@  @@! @@! @@!  @@!          @@!  @@@  @@!  @@@  @@!       @@!  @@@  
!@!        !@!  @!@  !@! !@! !@!  !@!          !@!  @!@  !@!  @!@  !@!       !@!  @!@  
!@! @!@!@  @!@!@!@!  @!! !!@ @!@  @!!!:!       @!@  !@!  @!@  !@!  @!!!:!    @!@!!@!   
!!! !!@!!  !!!@!!!!  !@!   ! !@!  !!!!!:       !@!  !!!  !@!  !!!  !!!!!:    !!@!@!    
:!!   !!:  !!:  !!!  !!:     !!:  !!:          !!:  !!!  :!:  !!:  !!:       !!: :!!   
:!:   !::  :!:  !:!  :!:     :!:  :!:          :!:  !:!   ::!!:!   :!:       :!:  !:!  
 ::: ::::  ::   :::  :::     ::    :: ::::     ::::: ::    ::::     :: ::::  ::   :::  
 :: :: :    :   : :   :      :    : :: ::       : :  :      :      : :: ::    :   : :  
                                                                                       
";
        private string IntroText = @"Location: Nebraska
Time: April 5, 2012, 3:34 PM

You're Jules. A devoted fan of Nicki Minaj, most commonly known as a Barb. You'd love to work for her as a personal assistant, so you too a risk and emailed her management team.";
        private string EmailFile = "email.txt";
        private string DenyEmail = @"Dear Mr. Azoff,

I am thankful for your response. Working for Nicki is a huge dream of mine. Although this is true, I have this gut feeling that I shouldn't go. I can't explain it, but I feel like someone told me deny the position. I thank you for your consideration, but I cannot accept this position.

Best,
Jules";
        private string AcceptEmail = @"Dear Mr. Azoff,

I am thankful for your response. Working for Nicki is a huge dream of mine. I can't believe that it could be a reality! Of course I accept this position. I am ready to go as soon as possible, my bags are already packed. I am prepared for what Nicki throws at me.

Thank you again,
Jules";


        public void Run()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            Clear();
            ConsoleUtils.SetWinSize(156, 49);

            ForegroundColor = ConsoleColor.Magenta;
            WriteLine(GameTitleArt);
            ForegroundColor = ConsoleColor.White;
            RunMainMenu();
        }

        private void Fail(string warning, string lose)
        {
            if (CurrentPlayer.Score == 0)
            {
                ForegroundColor = ConsoleColor.Red;
                TextAnimationUtils.AnimateTyping(lose);
                SetCursorPosition(15, 15);
                WriteLine(LoseArt);
                WriteLine("\n\nPress ENTER to go back to Nebraska...");
                ReadKey(true);
                Clear();
                ForegroundColor = ConsoleColor.White;
                LosePrompt();
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                TextAnimationUtils.AnimateTyping(warning);
                CurrentPlayer.Score--;
                ForegroundColor = ConsoleColor.White;
                WriteLine("\n\nPress ENTER to keep working...");
                ReadKey(true);
            }
        }

        private void Succeed(string encouragement)
        {
            ForegroundColor = ConsoleColor.Magenta;
            TextAnimationUtils.AnimateTyping(encouragement);
            CurrentPlayer.Score++;
            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\nPress ENTER to keep working...");
            ReadKey();
        }

        private void RunMainMenu()
        {
            string prompt = $@"
Welcome to the game, Barbz! Where would you like to go?
(Use the arrow keys to cycle through options and press enter to select an option)";
            string[] options = { "Play", "Credits", "Exit" };
            Menu mainMenu = new Menu(prompt, options, GameTitleArt);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Play();
                    break;
                case 1:
                    CreditsPage();
                    break;
                case 2:
                    ExitGame();
                    break;
            }
        }

        private void Play()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            TextAnimationUtils.AnimateTyping(IntroText);
            WriteLine();
            ConsoleUtils.Continue();
            Clear();

            ForegroundColor = ConsoleColor.Red;
            TextAnimationUtils.Blink($"{ArtAssets.EnvelopeArt}\n        *** 1 NEW EMAIL ***", 3);
            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\nPress ENTER to open...");
            ReadKey(true);
            Clear();

            EmailPrompt();

            TextAnimationUtils.AnimateTyping("You can't believe it! You're going to be working for the queen! You pack the remainder of your bags and hop on the plane for Los Angeles.");
            Thread.Sleep(250);
            ForegroundColor = ConsoleColor.Magenta;
            TextAnimationUtils.AnimateTyping("\n\n\nLike Nicki's verse in 'I Am Your Leader', you're flying one letter and one number.");
            ForegroundColor = ConsoleColor.White;
            ReadKey(true);

            TextAnimationUtils.PlaneAnimation(6);
            Clear();

            TextAnimationUtils.AnimateTyping("You land at the airport and are quickly driven to Nicki's mansion.");
            WriteLine("\n\nPress ENTER to meet Nicki...");
            ReadKey(true);
            Clear();

            ForegroundColor = ConsoleColor.Magenta;
            TextAnimationUtils.AnimateTyping("\"Hi Jules! Nice to meet you,\" Nicki says, \"Let's get to work. Can you go out and get me a drink somewhere? I'll give you the keys to my pink lamborghini.\"");
            ForegroundColor = ConsoleColor.White;
            LamboPrompt();

            Clear();
            LocationPrompt();

            TextAnimationUtils.CarAnimation(6);
            Clear();

            TextAnimationUtils.AnimateTyping($"You arrive at the {CurrentPlayer.Destination.Name}.");
            WriteLine("\n\nPress ENTER to park the car in the parking lot...");
            ReadKey(true);

            StartParkingLot();

            if (CurrentPlayer.Destination == Starbucks)
            {
                StarbucksPrompt();
            }
            else if (CurrentPlayer.Destination == ConStore)
            {
                ConStorePrompt();
            }

            Clear();
            ForegroundColor = ConsoleColor.Green;
            TextAnimationUtils.Blink($"{ArtAssets.TextArt}\n   *** 1 NEW TEXT MESSAGE ***", 3);
            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\nPress ENTER to open...");
            ReadKey(true);
            Clear();

            ForegroundColor = ConsoleColor.Magenta;
            TextAnimationUtils.AnimateTyping("\"Hi Jules, it's Nicki. I know you're out right now, so could you run over to the wig store and get a good wig for the concert tonight? Thank you so much!\"");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\nPress ENTER to go to the wig store...");
            ReadKey(true);
            Clear();

            Clear();
            TextAnimationUtils.CarAnimation(3, 750);
            Clear();

            TextAnimationUtils.AnimateTyping("There's lots of traffic on the highway. You see that ther's a carpool lane to your left that is moving quickly. You don't want to keep Nicki waiting.");
            HovLanePrompt();

            Clear();
            TextAnimationUtils.AnimateTyping($"You arrive at the wig store.");

            WigStorePrompt();

            Clear();
            TextAnimationUtils.AnimateTyping("You get back to Nicki's Mansion.");
            WriteLine("\n\nPress ENTER to go to Nicki...");
            ReadKey(true);
            Clear();

            if (CurrentPlayer.TookHovLane)
            {
                Succeed("\"Wow you got here really quick. That shows dedication. Nice job!\"");
                Clear();
            }
            else
            {
                ForegroundColor = ConsoleColor.Magenta;
                TextAnimationUtils.AnimateTyping("\"You're back? Great. Thank you. Let's see what you got for me.\"");
                ForegroundColor = ConsoleColor.White;
                ReadKey(true);
                Clear();
            }

            TextAnimationUtils.AnimateTyping("You give Nicki her drink...");
            ReadKey(true);

            if(CurrentPlayer.Slot1 == RedSlurpee)
            {
                ForegroundColor = ConsoleColor.Magenta;
                TextAnimationUtils.AnimateTyping("\n\n\"Ooh a Slurpee? A safe choice. Thank you.\"");
                ForegroundColor = ConsoleColor.White;
                WriteLine("\n\nPress ENTER to keep working...");
                ReadKey(true);
            }
            else if (CurrentPlayer.Slot1 == MonsterEnergy)
            {
                Fail("\n\n\"You got me a Monster? Do you know who I am? Don't ever do that again!\"", "\n\n\"You got me a Monster? Absolutley not. Do you know who I am? You're fired. Get out of here!\"");
            }
            else if(CurrentPlayer.Slot1 == PinkDrink)
            {
                Succeed("\n\n\"Wow. You really know me! Thanks love! This is delicious!!\"");
            }
            else if (CurrentPlayer.Slot1 == IcedCoffee)
            {
                ForegroundColor = ConsoleColor.Magenta;
                TextAnimationUtils.AnimateTyping("\n\n\"An iced coffee? A safe choice. Thank you.\"");
                ForegroundColor = ConsoleColor.White;
                WriteLine("\n\nPress ENTER to keep working...");
                ReadKey(true);
            }

            Clear();
            TextAnimationUtils.AnimateTyping("You give Nicki her wig...");
            ReadKey(true);

            if (CurrentPlayer.Slot2 == PinkWig)
            {
                Succeed("\n\n\"A pink wig? This is so cute!! You really know me! Nice job!\"");
            }
            else if (CurrentPlayer.Slot2 == BaldCap)
            {
                Fail("\n\n\"What is this? This better be a joke. Don't ever do that again.\"", "\n\n\"What the hell? THis is a bald cap! Get out of here! You're fired!\"");
            }
            else if (CurrentPlayer.Slot2 == BlackWig)
            {
                ForegroundColor = ConsoleColor.Magenta;
                TextAnimationUtils.AnimateTyping("\n\n\"This is cute... Not my color, but I like it. Thank you.\"");
                ForegroundColor = ConsoleColor.White;
                WriteLine("\n\nPress ENTER to keep working...");
                ReadKey(true);
            }

            Clear();
            ForegroundColor = ConsoleColor.Magenta;
            TextAnimationUtils.AnimateTyping("\"Thank you for the items. There's one last thing I need from you today. Can you get on my Twitter and promote the concert for me? We're so close to selling out!\"");
            ForegroundColor = ConsoleColor.White;
            PromotionPrompt();

            Clear();
            ForegroundColor = ConsoleColor.Magenta;
            TextAnimationUtils.AnimateTyping("\"Thank you for going out for me. It's about time for a sound check. Lets go to the concert!\"");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\nPress ENTER to go to the venue with Nicki...");
            ReadKey(true);

            if (CurrentPlayer.Score == 5)
            {
                Clear();
                ForegroundColor = ConsoleColor.Magenta;
                TextAnimationUtils.AnimateTyping("\"You were such a help today. Thank you so much! We really got along. How about you hang out backstage and stay working for me as my full-time assistant?\"");
                SetCursorPosition(15, 15);
                Write(WinArt);
                WriteLine("\n\nJules is living her dream working for Nicki now! Press ENTER to go back to the main menu...");
                ForegroundColor = ConsoleColor.White;
                ReadKey(true);
                Run();
            }
            else if (CurrentPlayer.Score < 5 && CurrentPlayer.Score > 1)
            {
                Clear();
                ForegroundColor = ConsoleColor.Magenta;
                TextAnimationUtils.AnimateTyping("\"Thank you for your assistance today. I'll be sure to call you again soon! You can work with the rest of my assistants.");
                SetCursorPosition(15, 15);
                Write(WinArt);
                WriteLine("\n\nJules is living her dream working for Nicki, but there's always room for improvement. Press ENTER to retry...");
                ForegroundColor = ConsoleColor.White;
                ReadKey(true);
                Clear();
                LosePrompt();
            }
            else
            {
                Clear();
                ForegroundColor = ConsoleColor.Magenta;
                TextAnimationUtils.AnimateTyping("\"Thanks for all your help today, but I just didn't vibe with you that much. I'm sorry but I won't be calling you again.\"");
                ForegroundColor = ConsoleColor.Red;
                SetCursorPosition(15, 15);
                WriteLine(LoseArt);
                WriteLine("\n\nJules tried, but she still didn't succeed... maybe give it another shot?");
                ReadKey(true);
                Clear();
                ForegroundColor = ConsoleColor.White;
                LosePrompt();
            }

        }

        private void CreditsPage()
        {
            Clear();
            WriteLine("A Game by Nathaniel Smith!");
            WriteLine("ASCII Art From:");
            WriteLine("\tPr59 - https://ascii.co.uk/art/envelope");
            WriteLine("\tJoan Stark - https://www.asciiart.eu/vehicles/airplanes");
            ConsoleUtils.Continue();
            RunMainMenu();
        }

        private void ExitGame()
        {
            ConsoleUtils.Exit();
            Environment.Exit(0);
        }

        private void PromotionPrompt()
        {
            string prompt = "\nHow are you going to promote the concert?";
            string[] options = { "Start beef with a hater", "Make a series of funny Tweets", "Start a giveaway for signed CD copies", "Don't do anything" };
            Menu promotionPrompt = new Menu(prompt, options, "\"Thank you for the items. There's one last thing I need from you today. Can you get on my Twitter and promote the concert for me? We're so close to selling out!\"");
            int selectedIndex = promotionPrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    TextAnimationUtils.AnimateTyping("You start beef with a hater...");
                    WriteLine("\n\nPress any keys to start writing.");
                    ForegroundColor = ConsoleColor.Cyan;
                    TextAnimationUtils.UserTypingAnimation("i cant stand u haters in my mentions any more... u miserable ppl refuse to move on w/ your lives. come to my concert if ur so obsessed w/ me.");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to send tweet...");
                    ReadKey(true);
                    Clear();
                    TextAnimationUtils.AnimateTyping("Well... that didnt go to plan... #NickiMinajIsCanceled is the #1 trending hashtag on Twitter. Nicki doesn't seem too happy...");
                    WriteLine("\n\nPress ENTER to talk to her...");
                    ReadKey(true);
                    Fail("You seriously got THAT trending? You better be lucky my fans are dedicated and aren't canceling their tickets. Don't mess up like that again!", "You seriously got THAT trending???? Get out of my house right now! You're fired!");
                    break;
                case 1:
                    Clear();
                    TextAnimationUtils.AnimateTyping("You make a funny tweet...");
                    WriteLine("\n\nPress any keys to start writing.");
                    ForegroundColor = ConsoleColor.Cyan;
                    TextAnimationUtils.UserTypingAnimation("cawling all barbs, cawling all barbs. report to the conference room right now... come to my concert! its tonight.");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to send tweet...");
                    ReadKey(true);
                    Clear();
                    TextAnimationUtils.AnimateTyping("Well... that worked well... the concert sold out!");
                    WriteLine("\n\nPress ENTER to talk to Nicki...");
                    ReadKey(true);
                    Succeed("The concert really sold out that fast? Wow you're good. Great job!");
                    break;
                case 2:
                    Clear();
                    TextAnimationUtils.AnimateTyping("You start a giveaway...");
                    WriteLine("\n\nPress any keys to start writing.");
                    ForegroundColor = ConsoleColor.Cyan;
                    TextAnimationUtils.UserTypingAnimation("i'm gonna be giving a few signed cd's! make sure you follow and attend the concert tonight to be entered!");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to send tweet...");
                    ReadKey(true);
                    Clear();
                    TextAnimationUtils.AnimateTyping("Well... it looks like sales didn't change that much... at least you gave it a try.");
                    WriteLine("\n\nPress any key to keep working...");
                    ReadKey(true);
                    break;
                case 3:
                    Clear();
                    TextAnimationUtils.AnimateTyping("You don't do anything...");
                    WriteLine("\n\nPress ENTER to talk to Nicki...");
                    ReadKey(true);
                    Fail("You're not going to do anything? You gotta get up and do something! Get back to work!", "You're not going to do anything? That's the last strike. Get out of here! You're fired!");
                    break;
            }
           
        }

        private void EmailPrompt()
        {
            string email = File.ReadAllText(EmailFile);
            string prompt = "\nHow are you going to respond?";
            string[] options = { "Accept", "Deny" };
            Menu emailPrompt = new Menu(prompt, options, email);
            int selectedIndex = emailPrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    WriteLine("You begin to write:\n\n");
                    TextAnimationUtils.UserTypingAnimation(AcceptEmail);
                    WriteLine("\n\nPress ENTER to send...");
                    ReadKey(true);
                    Clear();
                    break;
                case 1:
                    Clear();
                    WriteLine("You begin to write:\n\n");
                    TextAnimationUtils.UserTypingAnimation(DenyEmail);
                    WriteLine("\n\nPress ENTER to send...");
                    ReadKey();
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    TextAnimationUtils.AnimateTyping("And just like that you career is over before it began. Nicki doesn't even know you exist! That was a stupid decision.");
                    SetCursorPosition(15, 15);
                    WriteLine(LoseArt);
                    ReadKey(true);
                    Clear();
                    ForegroundColor = ConsoleColor.White;
                    LosePrompt();
                    break;
            }
        }

        private void WigStorePrompt()
        {
            string prompt = "Which wig do you buy?";
            string[] options = { "Black Wig", "Pink Bob Wig", "Baldcap" };
            Menu wigStorePrompt = new Menu(prompt, options, ArtAssets.WigOptionsArt);
            int selectedIndex = wigStorePrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    TextAnimationUtils.AnimateTyping("You get the black wig and start to head back to Nicki.");
                    CurrentPlayer.PickUpWig(BlackWig);
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to leave the store...");
                    ReadKey(true);
                    break;
                case 1:
                    Clear();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    TextAnimationUtils.AnimateTyping("You get the pink wig and start to head back to Nicki.");
                    CurrentPlayer.PickUpWig(PinkWig);
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to leave the store...");
                    ReadKey(true);
                    break;
                case 2:
                    Clear();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    TextAnimationUtils.AnimateTyping("You get the baldcap and start to head back to Nicki.");
                    CurrentPlayer.PickUpWig(BaldCap);
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to leave the store...");
                    ReadKey(true);
                    break;
            }

        }

        private void HovLanePrompt()
        {
            string art = "There's lots of traffic on the highway. You see that ther's a carpool lane to your left that is moving quickly. You don't want to keep Nicki waiting.";
            string prompt = "\n\nDo you illegally get in the express lane? (There is a chance you could get pulled over)";
            string[] options = { "Wait in traffic", "Get in the HOV lane" };
            Menu hovLanePrompt = new Menu(prompt, options, art);
            int selectedIndex = hovLanePrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    TextAnimationUtils.AnimateTyping("You decide to wait in traffic.");
                    Clear();
                    TextAnimationUtils.CarAnimation(3, 750);
                    break;
                case 1:
                    RunHovLane();
                    break;
            }
        }

        private void RunHovLane()
        {
            Clear();
            Random police = new Random();
            int pullOver = police.Next(1, 101);

            TextAnimationUtils.CarAnimation(3);
            if (pullOver < 50)
            {
                CurrentPlayer.TookHovLane = true;
            }
            else if (pullOver >= 50)
            {
                for (int i = 0; i < 3; i++)
                {
                    Clear();
                    BackgroundColor = ConsoleColor.Blue;
                    Clear();
                    WriteLine(" ");
                    Thread.Sleep(500);
                    BackgroundColor = ConsoleColor.Red;
                    Clear();
                    WriteLine(" ");
                    Thread.Sleep(500);
                    BackgroundColor = ConsoleColor.White;
                    Clear();
                    WriteLine(" ");
                    Thread.Sleep(250);
                }
                BackgroundColor = ConsoleColor.Black;
                Clear();
                ForegroundColor = ConsoleColor.Red;
                TextAnimationUtils.AnimateTyping("You just got pulled over! Nicki is calling you...");
                ForegroundColor = ConsoleColor.White;
                WriteLine("\n\nPress ENTER to answer...");
                ReadKey(true);
                Fail("\"You got pulled over!? Well at least you were trying to go places quickly. Don't you ever do this again!\"", "\"You got pulled over?! Hell no... Get back now! You're fired!\"");
            }
            TextAnimationUtils.CarAnimation(3);
        }

        private void LamboPrompt()
        {
            string art = "\"Hi Jules! Nice to meet you,\" Nicki says, \"Let's get to work. Can you go out and get me a drink somewhere? I'll give you the keys to my pink lamborghini.\"";
            string prompt = "\n\nHow do you respond?";
            string[] options =  { "Thank Nicki and take keys", "\"Pink lamborghini? I'm about to race with Chyna!\"" };
            Menu lamboPrompt = new Menu(prompt, options, art);
            int selectedIndex = lamboPrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    break;
                case 1:
                    Succeed("\n\"Brought the Wraith to Chyna just to race in China... I could seriously use that for a bar in the future. Nice job!\"");
                    break;
            }
        }

        private void LocationPrompt()
        {
            string prompt = "Where are you going to go?";
            string[] options = { "Starbucks", "Convience Store" };
            Menu locationPrompt = new Menu(prompt, options);
            int selectedIndex = locationPrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    CurrentPlayer.SetLocation(Starbucks);
                    break;
                case 1:
                    CurrentPlayer.SetLocation(ConStore);
                    break;

            }
        }

        private void StarbucksPrompt()
        {
            string prompt = "What are you going to order?";
            string[] options = { "Pink Drink", "Iced Coffee" };
            Menu starbucksPrompt = new Menu(prompt, options, ArtAssets.StarbucksArt);
            int selectedIndex = starbucksPrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    TextAnimationUtils.AnimateTyping("You order a pink drink and start to head back to Nicki.");
                    CurrentPlayer.PickUpDrink(PinkDrink);
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to leave Starbucks...");
                    ReadKey(true);
                    return;
                case 1:
                    Clear();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    TextAnimationUtils.AnimateTyping("You order an iced coffee and start to head back to Nicki.");
                    CurrentPlayer.PickUpDrink(IcedCoffee);
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to leave Starbucks...");
                    ReadKey(true);
                    return;
            }
        }

        private void ConStorePrompt()
        {
            string prompt = "What are you going to order?";
            string[] options = { "Red Slurpee", "Monster Energy Drink" };
            Menu conStorePrompt = new Menu(prompt, options, ArtAssets.ConStoreArt);
            int selectedIndex = conStorePrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    TextAnimationUtils.AnimateTyping("You get a red Slurpee and start to head back to Nicki.");
                    CurrentPlayer.PickUpDrink(RedSlurpee);
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to leave the store...");
                    ReadKey(true);
                    break;
                case 1:
                    Clear();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    TextAnimationUtils.AnimateTyping("You pick a Monster Energy Drink and start to head back to Nicki.");
                    CurrentPlayer.PickUpDrink(MonsterEnergy);
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n\nPress ENTER to leave the store...");
                    ReadKey(true);
                    break;
            }
        }

        private void LosePrompt()
        {
            string prompt = "\nWhat do you want to do now?";
            string[] options = { "Retry", "Exit" };
            Menu losePrompt = new Menu(prompt, options);
            int selectedIndex = losePrompt.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    Run();
                    break;
                case 1:
                    Clear();
                    ExitGame();
                    break;
            }
        }

        public void StartParkingLot()
        {

            CursorVisible = false;

            string[,] parkingLot =
            {
                {"█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█" },
                {"█", "░", "│", "█", "│", "░", "│", "░", "│", "█", "│", "░", "│", "█", "│", "█", "│", "░", "│", "░", "│", "█", "│", "░", "█" },
                {"█", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "█" },
                {"█", "░", "│", "░", "│", "░", "│", "░", "│", "█", "│", "█", "│", "░", "│", "░", "│", "░", "│", "█", "│", " ", " ", " ", "█" },
                {"█", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", " ", " ", " ", " " },
                {"█", "░", "│", "█", "│", "░", "│", "█", "│", "░", "│", "░", "│", "░", "│", "█", "│", "░", "│", "░", "│", " ", " ", " ", "█" },
                {"█", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "█" },
                {"█", "░", "│", "░", "│", "█", "│", "░", "│", "░", "│", "░", "│", "█", "│", "█", "│", "░", "│", "░", "│", " ", " ", " ", "█" },
                {"█", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", "─", "┼", " ", " ", " ", "█" },
                {"█", "░", "│", "░", "│", "░", "│", "░", "│", "█", "│", "░", "│", "░", "│", "█", "│", "░", "│", "█", "│", " ", " ", " ", "█" },
                {"█", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "█" },
                {"█", "░", "│", "░", "│", "█", "│", "░", "│", "░", "│", "░", "│", "░", "│", "░", "│", "█", "│", "░", "│", "░", "│", "░", "█" },
                {"█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█" },
            };

          MyWorld = new World(parkingLot);

            RunGameLoop();
        }

        private void HandlePlayerInput()
        {
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y - 1))
                    {
                        CurrentPlayer.Y -= 1;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y + 1))
                    {
                        CurrentPlayer.Y += 1;
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X - 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X -= 1;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X + 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X += 1;
                    }

                    break;
                default:
                    break;
            }
        }

        private void DrawFrame()
        {
            Clear();
            MyWorld.Draw();
            CurrentPlayer.Draw();
        }

        private void RunGameLoop()
        {

            while (true)
            {
                DrawFrame();
                HandlePlayerInput();
                string elementAtPlayerPos = MyWorld.GetElementAt(CurrentPlayer.X, CurrentPlayer.Y);

                if (elementAtPlayerPos == "█")
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    TextAnimationUtils.AnimateTyping("You crashed the car in the parking lot! Nicki is calling you.....");
                    ForegroundColor = ConsoleColor.White;   
                    WriteLine("\n\nPress ENTER to answer...");
                    ReadKey(true);
                    Fail("\n\n\"You scratched my car? Be more careful! Don't do that again!\"", "\n\n\"You scratched my car? That's it. Get back now. You're fired!\"");
                    break;
                }
                else if (elementAtPlayerPos == "░")
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Green;
                    TextAnimationUtils.AnimateTyping("You successfully parked the car!");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine($"\n\nPress ENTER to walk into {CurrentPlayer.Destination.Name}...");
                    ReadKey(true);
                    break;
                }
                    System.Threading.Thread.Sleep(20);
            }
        }
    }
}
