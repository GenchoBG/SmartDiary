using System;
using IntelliMood.Services;
using IntelliMood.Services.Implementations;

namespace IntelliMood.Tests
{
    public class Program
    {
        public static void Main()
        {
            var emotionsGuesser = new MyEmotionGetter();
        }
    }
}
