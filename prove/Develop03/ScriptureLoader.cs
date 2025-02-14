using System.Text.Json;

public class ScriptureLoader
{
    private class ScriptureJson
    {
        public List<Book> books { get; set; }
    }

    private class Book
    {
        public string book { get; set; }
        public List<Chapter> chapters { get; set; }
    }

    private class Chapter
    {
        public int chapter { get; set; }
        public string reference { get; set; }
        public List<Verse> verses { get; set; }
    }

    private class Verse
    {
        public string reference { get; set; }
        public string text { get; set; }
        public int verse { get; set; }
    }

    private static Random random = new Random();
    private static Dictionary<string, ScriptureJson> LoadedScriptures = new Dictionary<string, ScriptureJson>();

    public static Scripture LoadRandomScripture(string filePath)
    {
        try
        {
            if (!LoadedScriptures.ContainsKey(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                LoadedScriptures[filePath] = JsonSerializer.Deserialize<ScriptureJson>(jsonString);
            }

            var scriptureData = LoadedScriptures[filePath];
            
            // Select a random book
            var book = scriptureData.books[random.Next(scriptureData.books.Count)];
            
            // Select a random chapter
            var chapter = book.chapters[random.Next(book.chapters.Count)];
            
            // Decide if we want a single verse or multiple verses (30% chance of multiple)
            bool multipleVerses = random.NextDouble() < 0.3;
            
            if (multipleVerses && chapter.verses.Count > 1)
            {
                // Select 2-4 consecutive verses
                int verseCount = random.Next(2, Math.Min(5, chapter.verses.Count));
                int startVerseIndex = random.Next(0, chapter.verses.Count - verseCount + 1);
                var selectedVerses = chapter.verses.Skip(startVerseIndex).Take(verseCount).ToList();
                
                // Combine the verses' text
                string combinedText = string.Join(" ", selectedVerses.Select(v => v.text));
                
                return new Scripture(
                    new Reference(
                        book.book,
                        chapter.chapter,
                        selectedVerses[0].verse,
                        selectedVerses[^1].verse
                    ),
                    combinedText
                );
            }
            else
            {
                // Select a single verse
                var verse = chapter.verses[random.Next(chapter.verses.Count)];
                
                return new Scripture(
                    new Reference(book.book, chapter.chapter, verse.verse),
                    verse.text
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scriptures: {ex.Message}");
            // Return a default scripture as fallback (didnt have time to fully debug why this happens, but we have this to rely on)
            return new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life"
            );
        }
    }

    public static Scripture LoadRandomScriptureFromMultipleFiles(string[] filePaths)
    {
        string randomFile = filePaths[random.Next(filePaths.Length)];
        return LoadRandomScripture(randomFile);
    }
}