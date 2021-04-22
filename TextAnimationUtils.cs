using System;
using static System.Console;
using System.IO;
using System.Threading;

namespace AssistantBarb
{
    static class TextAnimationUtils
    {
        public static void Blink(string text, int blinkCount = 5, int onTime = 500, int offTime = 200)
        {
            CursorVisible = false;

            for (int i = 0; i < blinkCount; i++)
            {
                WriteLine(text);
                Thread.Sleep(onTime);
                Clear();
                Thread.Sleep(offTime);
            }
            WriteLine(text);
            CursorVisible = true;
        }

        public static void AnimateTyping(string text, int delay = 25)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Write(text[i]);
                Thread.Sleep(delay);

                // skip animation if enter key is pressed
                if (KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Write(text.Substring(i + 1));
                        break;
                    }
                }
            }
        }

        public static void UserTypingAnimation(string text, int delay = 25)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Write(text[i]);
                Thread.Sleep(delay);
                if (i % 10 == 0) { ReadKey(true); }

            }
        }

        public static void PlaneAnimation(int cycles, int duration = 250)
        {
            for (int i = 0; i < cycles; i++)
            {
                CursorVisible = false;
                Clear();
                WriteLine(ArtAssets.PlaneAnim1);
                Thread.Sleep(duration);
                Clear();
                WriteLine(ArtAssets.PlaneAnim2);
                Thread.Sleep(duration);
                Clear();
                WriteLine(ArtAssets.PlaneAnim3);
                Thread.Sleep(duration);
                Clear();
                WriteLine(ArtAssets.PlaneAnim4);
                Thread.Sleep(duration);
                Clear();
                WriteLine(ArtAssets.PlaneAnim5);
                Thread.Sleep(duration);
                CursorVisible = true;
            }
        }

        public static void CarAnimation(int cycles, int duration = 250)
        {
            for (int i = 0; i < cycles; i++)
            {
                ForegroundColor = ConsoleColor.Magenta;
                CursorVisible = false;
                Clear();
                WriteLine(ArtAssets.CarAnim1);
                Thread.Sleep(duration);
                Clear();
                WriteLine(ArtAssets.CarAnim2);
                Thread.Sleep(duration);
                CursorVisible = true;
            }
            ForegroundColor = ConsoleColor.White;
        }
    }
}
