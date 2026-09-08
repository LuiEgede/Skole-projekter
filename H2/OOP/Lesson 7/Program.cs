﻿namespace H2.OOP.Lesson_7;

class Program
{
    class Collection<T>
    {
        private List<T> items;

        public Collection()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }

        public T Find(Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        public void ForEach(Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }

    class LargestValue<T> where T : IComparable<T>
    {
        private List<T> items;

        public LargestValue()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public T GetLargestValue()
        {
            if (items.Count == 0)
            {
                throw new ArgumentException("Collection is empty");
            }

            return items.Max();
        }

        //Metoden GetSmallestValue og GetLargestValue kunne godt være en metode som begge ville udskrive en værdi baseret på args
        public T GetSmallestValue()
        {
            if (items.Count == 0)
            {
                throw new ArgumentException("Collection is empty");
            }

            return items.Min();
        }
    }


    static void Main(string[] args)
    {
        Collection<int> collection = new Collection<int>();
        Console.WriteLine("Collection: ");
        collection.Add(1);
        collection.Add(2);
        Console.WriteLine("Collection: ");
        collection.ForEach(Console.WriteLine);
        Console.WriteLine("Find: " + collection.Find(x => x == 2));
        Console.WriteLine("Remove: ");
        collection.Remove(3);
        Console.WriteLine("Collection: ");
        collection.ForEach(Console.WriteLine);

        LargestValue<int> largestValue = new LargestValue<int>();
        largestValue.Add(1);
        largestValue.Add(2);
        largestValue.Add(3);
        Console.WriteLine("Largest Value: " + largestValue.GetLargestValue());
        Console.WriteLine("Smallest Value: " + largestValue.GetSmallestValue());
    }
}