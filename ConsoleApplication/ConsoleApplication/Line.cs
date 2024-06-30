using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    public class Line
    {
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        private readonly List<Position> positions;

        public Line()
        {
            positions = new List<Position>(9);
        }

        public IReadOnlyList<Position> Positions => positions;
        public bool IsFull => positions.Count > 8;
        public bool IsCompleted => positions.All(p => p.IsCrossed);
        public decimal Count => positions.Count;

        public void Add(int number)
        {
            positions.Add(new Position(number));
        }

        public void Cross(int position)
        {
            if (positions.Count > position)
                positions[position].Cross();
            else
                Console.WriteLine($"Error:Missing position {position}");
        }

        public bool IsCrossed(int position)
        {
            if (positions.Count > position)
                return positions[position].IsCrossed;

            Console.WriteLine($"Error:Missing position {position}");
            return false;
        }

        public override string ToString()
        {
            StringBuilder.Clear();

            for (var i = 0; i < positions.Count; i++) StringBuilder.Append(positions[i]);

            return StringBuilder.ToString();
        }
    }
}