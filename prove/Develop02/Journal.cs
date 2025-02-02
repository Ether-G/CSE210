public class Journal
{
    private List<Entry> _entries;
    private List<string> _prompts;
    private Random _random;

    public Journal()
    {
        _entries = new List<Entry>();
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What was something new I learned today?",
            "What was the most challenging thing I faced today?",
            "What am I most grateful for today?"
        };
        _random = new Random();
    }

    public void AddEntry()
    {
        string prompt = GetRandomPrompt();
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("> ");
        string response = Console.ReadLine();

        Console.Write("How are you feeling? (Optional - press Enter to skip): ");
        string mood = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();

        Entry entry = new Entry(date, prompt, response, mood);
        _entries.Add(entry);
        
        Console.WriteLine("\nEntry added successfully!");
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nNo entries to display.");
            return;
        }

        Console.WriteLine("\n=== Journal Entries ===");
        foreach (Entry entry in _entries)
        {
            Console.WriteLine(entry.FormatForDisplay());
        }

        // Display statistics
        Console.WriteLine("\n=== Journal Statistics ===");
        Console.WriteLine($"Total Entries: {_entries.Count}");
        
        var moodEntries = _entries.Where(e => !string.IsNullOrEmpty(e.Mood));
        if (moodEntries.Any())
        {
            Console.WriteLine("\nRecorded Moods:");
            var moodGroups = moodEntries.GroupBy(e => e.Mood);
            foreach (var group in moodGroups)
            {
                Console.WriteLine($"- {group.Key}: {group.Count()} times");
            }
        }
    }

    public void SaveToFile()
    {
        Console.Write("\nEnter filename to save: ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in _entries)
                {
                    writer.WriteLine(entry.FormatForFile());
                }
            }
            Console.WriteLine("Journal saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        Console.Write("\nEnter filename to load: ");
        string filename = Console.ReadLine();

        try
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            
            foreach (string line in lines)
            {
                string[] parts = line.Split("~|~");
                if (parts.Length >= 3)
                {
                    string mood = parts.Length > 3 ? parts[3] : "";
                    Entry entry = new Entry(parts[0], parts[1], parts[2], mood);
                    _entries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    private string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}