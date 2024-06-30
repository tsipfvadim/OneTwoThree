using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication
{
    public class FieldOperations : IEquatable<FieldOperations>
    {
        private readonly List<Operation> operations;
        private readonly List<int> positions;

        public IReadOnlyList<Operation> Operations => operations;

        public FieldOperations()
        {
            operations = new List<Operation>();
            positions = new List<int>();
        }

        public FieldOperations(FieldOperations source, Operation operation)
        {
            operations = new List<Operation>(source.Operations) { operation };
            positions = new List<int>(source.positions);

            if (!operation.Refill)
            {
                positions.Add(operation.Start);
                positions.Add(operation.End);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(operations[0].ToString());

            for (var i = 1; i < operations.Count; i++)
                sb.Append($" -> {operations[i].ToString()}");

            return sb.ToString();
        }

        public bool Equals(FieldOperations other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (operations.Count != other.operations.Count)
                return false;

            for (int i = operations.Count - 1; i >= 0; i--)
            {
                var operation = operations[i];

                if (operation.Refill)
                {
                    if (!other.operations[i].Refill)
                        return false;
                }

                if (!other.positions.Contains(operation.Start) || !other.positions.Contains(operation.End))
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((FieldOperations)obj);
        }

        public override int GetHashCode()
        {
            return (operations != null ? operations.GetHashCode() : 0);
        }
    }
}