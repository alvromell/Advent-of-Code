using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2025
{
    [Solves("2025/12/7")]
    internal class Day7Solver2025 : ISolver
    { 
        private Dictionary<(int, int), long> _memo = new Dictionary<(int, int), long>();
        
        public Task<string> SolvePartOne(Input input)
        {
            var lines = input.GetInputLines().Result;

            var beamIndex = new HashSet<int> { lines.First().IndexOf("S") };
            var splitCount = 0L;

            foreach (var line in lines) {
                if (!line.Contains("^")) continue;
                var splitterIndex = new HashSet<int> { };

                for (int i=0; i<line.Length; i++) if(line[i]=='^') splitterIndex.Add(i);
                var hits = beamIndex.Intersect(splitterIndex).ToHashSet();
                foreach (var hit in hits) { 
                    splitCount++; 
                    beamIndex.Remove(hit); 
                    beamIndex.Add(hit - 1); 
                    beamIndex.Add(hit + 1); 
                }
            }
            return Task.FromResult(splitCount.ToString());
        }

        public Task<string> SolvePartTwo(Input input)
        {
            var lines = input.GetInputLines().Result.ToList();
            var map = lines.Select(l => l.ToCharArray()).ToArray();

            var startIndex = lines.First().IndexOf("S");
            var beamCount = CountBeam(ref map, 0, startIndex);
            

            return Task.FromResult(beamCount.ToString());
        }

        private long CountBeam(ref char[][] map, int lineNr, int columnIndex)
        {
            if (lineNr == map.Length-1) return 1;
            if (_memo.ContainsKey((lineNr, columnIndex)))
            {
                return _memo[(lineNr, columnIndex)];
            }

            long result;
            if (map[lineNr][columnIndex] == '^') result = CountBeam(ref map, lineNr, columnIndex - 1) + CountBeam(ref map, lineNr, columnIndex + 1);
            else result = CountBeam(ref map, lineNr + 1, columnIndex);

            _memo[(lineNr, columnIndex)] = result;

            return result;
        }
    }
}
