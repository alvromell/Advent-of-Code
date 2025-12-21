using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2015
{
    [Solves("2025/12/2")]
    internal class Day2Solver2025 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            var ranges = (await input.GetRawInput()).Split(',');
            BigInteger total = 0;
            foreach (var range in ranges)
            {
                (var start, var end) = (range.Split("-")[0], range.Split("-")[1]);

                if (start.Length % 2 != 0 && end.Length == start.Length) continue;

                for (BigInteger i = BigInteger.Parse(start); i <= BigInteger.Parse(end); i++)
                {
                    string currentNr = i.ToString();
                    if (currentNr[..(currentNr.Length / 2)] == currentNr[(currentNr.Length / 2)..])
                    {
                        total += i;
                    }
                }
            }
            return total.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var ranges = (await input.GetRawInput()).Split(',');
            BigInteger total = 0;
            foreach (var range in ranges)
            {
                (var start, var end) = (range.Split("-")[0], range.Split("-")[1]);

                for (BigInteger i = BigInteger.Parse(start); i <= BigInteger.Parse(end); i++)
                {
                    string currentNr = i.ToString();
                    if (Regex.IsMatch(currentNr, @"^(.+?)\1+$"))
                    {
                        total += i;
                    }
                }
            }
            return total.ToString();
        }
    }
}
