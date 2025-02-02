/*
Exceeded requirements by adding:
1. Mood tracking system - users can optionally record their mood with each entry
2. Journal statistics - displays total number of entries and mood frequency
3. Backwards compatibility - can still load old journal files
4. Enhanced display - shows mood information when available
5. Optional fields - mood tracking is optional, making the journal more flexible

These features help users:
- Track their emotional state over time
- See patterns in their moods
- Keep their journaling simple when desired
*/

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Journal Menu ===");
            Console.WriteLine("1. Write new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveToFile();
                    break;
                case "4":
                    journal.LoadFromFile();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    break;
            }
        }
    }
}