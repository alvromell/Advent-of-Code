using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2025
{
    [Solves("2025/12/08")]
    internal class Day8Solver2025 : ISolver
    {
        public Task<string> SolvePartOne(Input input)
        {
            var points = input.GetInputLines()
                            .Result.Select(l => new Point((l.Split(",").Select(n => int.Parse(n))).ToArray()))
                            .ToList();

            double shortestDist = double.MaxValue;

            foreach (var p1 in points)
            {
                foreach (var p2 in points)
                {
                    var d = Distance(p1, p2);
                    if (!p1.Equals(p2) && d < shortestDist) shortestDist = d;
                }
            }

            return Task.FromResult("Part 1 result");
        }

        public Task<string> SolvePartTwo(Input input)
        {
            return Task.FromResult("Part 2 result");
        }

        private double Distance(Point a, Point b)
        {
            return Math.Sqrt(Math.Abs((b.x - a.x)^2 + (b.y - a.y)^2 + (b.z - a.z)^2));
        }

        internal class Point
        {
            public readonly int x;
            public readonly int y;
            public readonly int z;
            public Point(int x, int y, int z)
            {
                this.x = x; this.y = y; this.z = z;
            }
            public Point(int[] c)
            {
                this.x = c[0]; this.y = c[1]; this.z = c[2];
            }
            public override string ToString()
            {
                return $"x:{x},\t y:{y},\t z:{z}";
            }
            public bool Equals(Point p)
            {
                return p.x != this.x && p.y != this.y && p.z != this.z;
            }
        }
    }
}
