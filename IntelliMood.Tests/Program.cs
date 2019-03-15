using System;
using IntelliMood.Services;

namespace IntelliMood.Tests
{
    public class Program
    {
        public static void Main()
        {
            string text = Console.ReadLine();

            EmotionGetter get = new EmotionGetter();

            while (true)
            {
                string mood = get.GetEmotionFromText(text);

                Console.WriteLine(mood);

                text = Console.ReadLine();
            }

        }
    }
}
