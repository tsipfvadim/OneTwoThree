using System;

namespace ConsoleApplication
{
    public struct Operation : IEquatable<Operation>
    {
        public readonly int Start;
        public readonly int End;

        public bool Refill => Start < 0;

        public Operation(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return Refill ? "ReFill" : $"({Start},{End})";
        }

        public bool Equals(Operation other)
        {
            return Start == other.Start && End == other.End;
        }

        public override bool Equals(object obj)
        {
            return obj is Operation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Start * 397) ^ End;
        }
    }
}