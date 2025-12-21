using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2025
{
    [Solves("2025/12/3")]
    internal class Day3Solver2025 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            var totalJoltage = 0;

            foreach (var line in await input.GetInputLines()) {
                var bank = line.Select(n => int.Parse(n.ToString())).ToArray();

                var bat1 = bank[..(bank.Length-1)].Select((n, i) => (n, i)).Aggregate((max,x)=>x.n > max.n ? x : max);
                var bat2 = bank[(bat1.i+1)..].Select((n,i) => (n,i)).Max();

                totalJoltage += int.Parse(string.Concat(bat1.n,bat2.n));
            }

            return totalJoltage.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            BigInteger totalJoltage = 0;
            var joltages = new List<BigInteger>();
            foreach (var line in await input.GetInputLines())
            {
                var bank = line.Select(n => int.Parse(n.ToString())).ToArray();
                var maxVoltageForBank = GetMaxJoltageForBank(bank, 12);
                joltages.Add(maxVoltageForBank);
                totalJoltage += maxVoltageForBank;
            }

            return totalJoltage.ToString();
        }

        private BigInteger GetMaxJoltageForBank(int[] bank, int numberOfBatteries)
        {
            if(numberOfBatteries == 1) return bank.Select((n, i) => (n, i)).Max().n;
            var potentials = bank[..(bank.Length - numberOfBatteries + 1)];
            var maxBattery = potentials.Select((n, i) => (n, i)).Aggregate((max, x) => x.n > max.n ? x : max);
            return BigInteger.Parse(string.Concat(maxBattery.n, GetMaxJoltageForBank(bank[(maxBattery.i + 1)..], numberOfBatteries - 1)));
        }
    }
}
