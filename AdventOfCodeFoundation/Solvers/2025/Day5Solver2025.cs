using AdventOfCodeFoundation.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeFoundation.Solvers._2025
{
    [Solves("2025/12/5")]
    internal class Day5Solver2025 : ISolver
    {
        public async Task<string> SolvePartOne(Input input)
        {
            var lines = await input.GetInputLines();
            var ranges = new List<(long, long)>();
            var ids = new List<long>();

            foreach (var line in lines)
            {
                if (line.Contains('-')) ranges.Add((long.Parse(line.Split('-')[0]), long.Parse(line.Split("-")[1])));
                else if (!string.IsNullOrWhiteSpace(line)) ids.Add(long.Parse(line));
            }

            var freshIds = 0;
            foreach (var id in ids) foreach (var r in ranges) if (r.Item1 <= id && id <= r.Item2) { freshIds++; break; }
            return freshIds.ToString();
        }

        public async Task<string> SolvePartTwo(Input input)
        {
            var lines = await input.GetInputLines();
            var ranges = new RangeIndex();
            foreach (var line in lines)
            {
                if (line.Contains('-')) ranges.AddMergeRange(new Range(long.Parse(line.Split('-')[0]), long.Parse(line.Split("-")[1])));
            }

            long freshIdCount = 0;
            foreach(Range r in ranges.Ranges)
            {
                freshIdCount += (r.End - r.Start + 1);
            }
            return freshIdCount.ToString();
        }

    }
    internal class Range
    {
        public long Start;
        public long End;

        public Range(long start, long end) { Start = start; End = end; }
    }

    internal class RangeIndex
    {
        private List<Range> _ranges = new List<Range>();
        public IReadOnlyList<Range> Ranges => _ranges.OrderBy(r => r.Start).ToList();
        public RangeIndex() { }
        public void AddMergeRange(Range newRange)
        {
            var overlappingRanges = _ranges.Where(existingRange => existingRange.End >= newRange.Start && existingRange.Start <= newRange.End).ToList();
            
            if (overlappingRanges is not null && overlappingRanges.Count()>0) 
            {
                overlappingRanges.Add(newRange);
                var mergedRange = new Range(overlappingRanges.Min(r => r.Start),overlappingRanges.Max(r => r.End));
                _ranges.RemoveAll(r => overlappingRanges.Contains(r));
                _ranges.Add(mergedRange);

            }
            else
            {
                _ranges.Add(newRange);
            }
        }
    }
}
