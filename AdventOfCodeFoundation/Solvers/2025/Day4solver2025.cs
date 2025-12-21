using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2025
{
    [Solves("2025/12/4")]
    internal class Day4solver2025 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            var lines = await input.GetInputLines();
            var size = lines.First().Length;
            char[][] map = CreatePaddedMap(size, lines);

            var accecibleRolls = 0;
            for (int i=1; i<=size; i++)
            {
                for (int j=1; j<=size; j++)
                {
                    if (map[j][i] != '@') continue;
                    var adjacentRolls = CountAdjacent(map, i, j);
                    if(adjacentRolls<4) accecibleRolls++;
                }
            }
            return accecibleRolls.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var lines = await input.GetInputLines();
            var size = lines.First().Length;
            char[][] map = CreatePaddedMap(size, lines);

            var accecibleRolls = 0;
            var removedRolls = true;
            while (removedRolls) 
            {
                removedRolls = false;
                for (int i = 1; i <= size; i++)
                {
                    for (int j = 1; j <= size; j++)
                    {
                        if (map[j][i] != '@') continue;
                        var removed = CountAndRemoveAdjacent(ref map, i, j);
                        if (removed)
                        {
                            accecibleRolls++;
                            removedRolls = true;
                        }
                    }
                }
            }  

            return accecibleRolls.ToString();
        }
        private char[][] CreatePaddedMap(int size, IEnumerable<string> lines)
        {
            char[][] map = new char[size + 2][];
            map[0] = Enumerable.Repeat('.', size + 2).ToArray();
            var lineNr = 1;
            foreach (var line in lines)
            {
                map[lineNr] = new char[1] { '.' }.Concat(line.ToCharArray()).Concat(new char[1] { '.' }).ToArray();
                lineNr++;
            }
            map[size + 1] = Enumerable.Repeat('.', size + 2).ToArray();
            return map;
        }
        private int CountAdjacent(char[][] map, int i, int j) {
            var count = 0;
            List<(int, int)> offsets = new List<(int, int)>
            {
                (-1,-1),(-1,0),(-1,1),(0,-1),(0,1),(1,-1),(1,0),(1,1),
            };

            foreach (var offset in offsets)
            {
                if (map[j + offset.Item1][i+offset.Item2]=='@') count++;
            }
            return count; 
        }
        private bool CountAndRemoveAdjacent(ref char[][] map, int i, int j) {
            var removedRoll = false;
            var surroundingRolls = 0;
            List<(int, int)> offsets = new List<(int, int)>
            {
                (-1,-1),(-1,0),(-1,1),(0,-1),(0,1),(1,-1),(1,0),(1,1),
            };

            foreach (var offset in offsets)
            {
                if (map[j + offset.Item1][i + offset.Item2] == '@')
                {
                    surroundingRolls++;
                }
            }
            if (surroundingRolls < 4)
            {
                map[j][i] = '.';
                removedRoll = true;
            }
            return removedRoll; 
        }
    }
}
