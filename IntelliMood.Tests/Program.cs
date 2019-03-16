using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SVDPP
{

    class Program
    {


        static void Main(string[] args)
        {
            var test = new List<List<double>>()
            {
                new List<double>(){ 0, 3, 4, 5, 2 },
                new List<double>(){ 3, 5, 2, 2, 5 },
                new List<double>(){ 5, 3, 0, 4, 3 },
                new List<double>(){ 5, 5, 5, 0, 5 },
                new List<double>(){ 2, 3, 0, 2, 2 }
            };

            var predictor = new Predictor();

            var populated = predictor.GetPopulatedEmptySpots(test);

            foreach (var row in populated)
            {
                Console.WriteLine(string.Join(" ", row.Select(n => n.ToString("F2"))));
            }

        }
    }
}
