using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2025
{
    [Solves("2025/12/6")]
    internal class Day6Solver2025 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            var lines = await input.GetInputLines();
            var homework = new List<string[]> { };
            foreach (var line in lines.Where(l => !l.Contains("+")))
            {
                homework.Add(Regex.Split(line.Trim(), "\\s+"));
            }
            var operations = Regex.Split(lines.Last().Trim(), "\\s+");

            long total = 0;
            for (int i = 0; i < homework[0].Length; i++)
            {
                total += operations[i]=="+" 
                    ? homework.Aggregate(0L, (acc, val) => acc += long.Parse(val[i])) 
                    : homework.Aggregate(1L, (acc, val) => acc *= long.Parse(val[i]));
            }
            return total.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var lines = (await input.GetInputLines()).ToArray();
            
            var homework = lines.Where(l => !l.Contains("+")).Select(l => l.ToCharArray()).ToList();
            
            var operations = lines.Last().ToCharArray();

            long total = 0;
            var currentNumbers = new List<long> { };
            for (int i = homework.First().Length-1; i >= 0; i--)
            {
                var number = new string(homework.Select(l => l[i]).ToList().Where(c => !Char.IsWhiteSpace(c) && Char.IsDigit(c)).ToArray());
                currentNumbers.Add(long.Parse(number));
                if (!Char.IsWhiteSpace(operations[i]))
                {
                    total += operations[i] == '+' ? currentNumbers.Sum() : currentNumbers.Aggregate(1L, (acc, val) => acc *= val);
                    currentNumbers = new List<long> { };
                    i--;
                    continue;
                }
            }
            return total.ToString();
        }
    }
}
