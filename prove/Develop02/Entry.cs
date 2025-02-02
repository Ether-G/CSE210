public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public string FormatForDisplay()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\n{Response}\n";
    }

    public string FormatForFile()
    {
        // Using ~|~ as separator
        return $"{Date}~|~{Prompt}~|~{Response}";
    }
}
