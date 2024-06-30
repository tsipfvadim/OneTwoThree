using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    public class Field
    {
        private readonly List<Line> lines;

        public Field()
        {
            lines = new List<Line>();
            lines.Add(new Line());

            for (var i = 1; i < 20; i++)
                if (i % 10 != 0)
                    AddNumber(i);
        }

        public int Count => (int)lines.Sum(l => l.Count);

        public void Refill()
        {
            var positions = new List<int>();

            for (var i = 0; i < lines.Count; i++)
                positions.AddRange(
                    lines[i].Positions
                        .Where(p => !p.IsCrossed)
                        .Select(p => p.Number));

            for (var i = 0; i < positions.Count; i++) AddNumber(positions[i]);
        }

        public void Cross(int start, int end)
        {
            GetPosition(start).Cross();
            GetPosition(end).Cross();
        }

        public bool TryCrossHorizontal(int pos)
        {
            var position = GetPosition(pos);

            if (position.IsCrossed)
                return false;

            var count = Count;

            for (var i = pos + 1; i < count; i++)
            {
                var nextPosition = GetPosition(i);

                if (nextPosition.IsCrossed)
                    continue;

                if (position.CanCross(nextPosition))
                {
                    position.Cross();
                    nextPosition.Cross();
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool TryCrossVertical(int pos)
        {
            var position = GetPosition(pos);
            var count = Count;

            if (position.IsCrossed)
                return false;

            for (var i = pos + 9; i < count; i += 9)
            {
                var nextPosition = GetPosition(i);

                if (nextPosition.IsCrossed)
                    continue;

                if (position.CanCross(nextPosition))
                {
                    position.Cross();
                    nextPosition.Cross();
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool CanCrossHorizontal(int pos, out int end)
        {
            end = 0;
            var position = GetPosition(pos);

            if (position.IsCrossed)
                return false;

            var count = Count;

            for (var i = pos + 1; i < count; i++)
            {
                var nextPosition = GetPosition(i);

                if (nextPosition.IsCrossed)
                    continue;

                if (position.CanCross(nextPosition))
                {
                    end = i;
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool CanCrossVertical(int pos, out int end)
        {
            end = 0;
            var position = GetPosition(pos);
            var count = Count;

            if (position.IsCrossed)
                return false;

            for (var i = pos + 9; i < count; i += 9)
            {
                var nextPosition = GetPosition(i);

                if (nextPosition.IsCrossed)
                    continue;

                if (position.CanCross(nextPosition))
                {
                    end = i;
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool IsCompleted()
        {
            for (var i = 0; i < lines.Count; i++)
                if (!lines[i].IsCompleted)
                    return false;

            return true;
        }

        public void Print()
        {
            for (var i = 0; i < lines.Count; i++) Console.WriteLine(lines[i].ToString());
        }

        private void AddNumber(int number)
        {
            if (lines.Last().IsFull)
                lines.Add(new Line());

            if (number < 10)
            {
                lines.Last().Add(number);
            }
            else
            {
                lines.Last().Add(number / 10);
                AddNumber(number % 10);
            }
        }

        private Position GetPosition(int pos)
        {
            var linePosition = pos / 9;
            var inLinePosition = pos % 9;
            return lines[linePosition].Positions[inLinePosition];
        }
    }
}