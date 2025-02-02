public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mood { get; set; }

    public Entry(string date, string prompt, string response, string mood = "")
    {
        Date = date;
        Prompt = prompt;
        Response = response;
        Mood = mood;
    }

    public string FormatForDisplay()
    {
        string moodDisplay = !string.IsNullOrEmpty(Mood) ? $"\nMood: {Mood}" : "";
        return $"Date: {Date}\nPrompt: {Prompt}\n{Response}{moodDisplay}\n";
    }

    public string FormatForFile()
    {
        return $"{Date}~|~{Prompt}~|~{Response}~|~{Mood}";
    }
}