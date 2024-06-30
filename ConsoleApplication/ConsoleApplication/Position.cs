using System;

namespace ConsoleApplication
{
    public class Position
    {
        public readonly int Number;

        public Position(int number)
        {
            Number = number;
            IsCrossed = false;
        }

        public bool IsCrossed { get; private set; }

        public void Cross()
        {
            if (IsCrossed)
                Console.WriteLine($"Error:{Number} Already Crossed");

            IsCrossed = true;
        }

        public override string ToString()
        {
            return IsCrossed ? "X" : Number.ToString();
        }

        public bool CanCross(Position position)
        {
            return Number == position.Number || Number + position.Number == 10;
        }
    }
}