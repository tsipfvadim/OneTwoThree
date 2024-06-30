using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication
{
    public class GameOperations
    {
        private readonly List<int> allowedRefills = new List<int> { 6, 12 };
        private readonly List<FieldOperations> operations;
        private int iterations;

        public GameOperations()
        {
            operations = new List<FieldOperations>
            {
                new FieldOperations()
            };
        }

        public int Count => operations.Count;

        public void Iterate()
        {
            var watch = new Stopwatch();
            watch.Start();
            var sources = new List<FieldOperations>(operations);
            operations.Clear();

            for (var i = 0; i < sources.Count; i++)
                operations.AddRange(Iterate(sources[i]));

            iterations++;
            var removed = RemoveDuplicates();
            var refilled = RemoveRefills();
            watch.Stop();
            Console.WriteLine(
                $"Iteration {iterations} Count = {Count} Time = {watch.ElapsedMilliseconds} Removed {removed} RefillRemoved {refilled}");
        }

        private IEnumerable<FieldOperations> Iterate(FieldOperations source)
        {
            var field = new Field();
            ApplyOperations(field, source);

            if (field.IsCompleted())
                Console.WriteLine($"IsCompleted {iterations} {source}");

            var variants = GetAvailableVariants(field);
            return CombineWithSource(source, variants);
        }

        private void ApplyOperations(Field field, FieldOperations fieldOperations)
        {
            for (var i = 0; i < fieldOperations.Operations.Count; i++)
            {
                var operation = fieldOperations.Operations[i];

                if (operation.Refill)
                    field.Refill();
                else
                    field.Cross(operation.Start, operation.End);
            }
        }

        private List<Operation> GetAvailableVariants(Field field)
        {
            var variants = new List<Operation>();

            for (var i = 0; i < field.Count; i++)
            {
                if (field.CanCrossHorizontal(i, out var horEnd))
                    variants.Add(new Operation(i, horEnd));

                if (field.CanCrossVertical(i, out var verEnd))
                    variants.Add(new Operation(i, verEnd));
            }

            return variants;
        }

        private IEnumerable<FieldOperations> CombineWithSource(FieldOperations source, List<Operation> variants)
        {
            var result = new List<FieldOperations>();

            if (variants.Count == 0)
            {
                result.Add(new FieldOperations(source, new Operation(-1, 0)));
                return result;
            }

            for (var i = 0; i < variants.Count; i++) result.Add(new FieldOperations(source, variants[i]));

            return result;
        }

        private int RemoveDuplicates()
        {
            var removed = 0;
            var index = 0;

            while (index < operations.Count)
            {
                var fieldOperations = operations[index];

                for (var i = index + 1; i < operations.Count; i++)
                {
                    var otherOperations = operations[i];
                    if (!fieldOperations.Equals(otherOperations))
                        continue;

                    fieldOperations.Equals(otherOperations);
                    operations.RemoveAt(i);
                    i--;
                    removed++;
                }

                index++;
            }

            return removed;
        }

        private int RemoveRefills()
        {
            var removed = 0;
            var index = 0;

            while (index < operations.Count)
            {
                var fieldOperations = operations[index];

                if (fieldOperations.Operations.Last().Refill)
                    if (!allowedRefills.Contains(iterations))
                    {
                        operations.RemoveAt(index);
                        index--;
                        removed++;
                    }

                index++;
            }

            return removed;
        }
    }
}