using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        // Get numbers from user
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());
            
            if (number == 0)
                break;
                
            numbers.Add(number);
        }

        // Core Requirements
        if (numbers.Count > 0)
        {
            // Calculate sum
            int sum = numbers.Sum();
            Console.WriteLine($"The sum is: {sum}");
            
            // Calculate average
            double average = numbers.Average();
            Console.WriteLine($"The average is: {average}");
            
            // Find largest number
            int largest = numbers.Max();
            Console.WriteLine($"The largest number is: {largest}");

            // Stretch Challenge
            var positiveNumbers = numbers.Where(x => x > 0);
            if (positiveNumbers.Any())
            {
                int smallestPositive = positiveNumbers.Min();
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            }

            // Stretch Challenge
            Console.WriteLine("The sorted list is:");
            var sortedNumbers = numbers.OrderBy(x => x);
            foreach (int num in sortedNumbers)
            {
                Console.WriteLine(num);
            }
        }
    }
}