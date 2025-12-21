using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2025
{
    [Solves("2025/12/1")]
    internal class Day1Solver2025 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            var x = 50;
            var countOfZeroPointers = 0;
            var lineNr = 0;

            foreach (var instruction in await input.GetInputLines())
            {
                lineNr++;
                var numberOfClicks = int.Parse(instruction[1..]);
                x = instruction.StartsWith("R") ? x + numberOfClicks : x - numberOfClicks;

                while (x < 0) { x += 100; }
                while (x > 99) { x -= 100; }
                if (x == 0) { countOfZeroPointers++; }

                if (lineNr < 100) Console.WriteLine($"Line: {lineNr}, clicks: {numberOfClicks} to the {(instruction.StartsWith("R") ? "right" : "left")}, x = {x}");

            }
            return countOfZeroPointers.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var start = 50;
            var countOfZeroPointers = 0;

            foreach (var instruction in await input.GetInputLines())
            {
                var numberOfClicks = int.Parse(instruction[1..]);
                
                countOfZeroPointers += numberOfClicks/100;
                numberOfClicks = numberOfClicks % 100;
                var end = instruction.StartsWith("R") ? start + numberOfClicks : start - numberOfClicks;
                
                if ((start != 0 && end <= 0) || end >=100)
                {
                    countOfZeroPointers++;
                }
                if (end > 99) end -= 100;
                if (end < 0) end += 100;
                
                start = end;
            }
            return countOfZeroPointers.ToString();
        }

    }
}
