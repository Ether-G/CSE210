using System.Text.Json;

public class ScriptureLoader
{
    private class ScriptureJson
    {
        private List<Book> _books;
        public List<Book> books 
        { 
            get { return _books; }
            set { _books = value; }
        }
    }

    private class Book
    {
        private string _book;
        private List<Chapter> _chapters;
        public string book 
        { 
            get { return _book; }
            set { _book = value; }
        }
        public List<Chapter> chapters
        {
            get { return _chapters; }
            set { _chapters = value; }
        }
    }

    private class Chapter
    {
        private int _chapter;
        private string _reference;
        private List<Verse> _verses;
        public int chapter
        {
            get { return _chapter; }
            set { _chapter = value; }
        }
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        public List<Verse> verses
        {
            get { return _verses; }
            set { _verses = value; }
        }
    }

    private class Verse
    {
        private string _reference;
        private string _text;
        private int _verse;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }
        public int verse
        {
            get { return _verse; }
            set { _verse = value; }
        }
    }

    private static Random _random = new Random();
    private static Dictionary<string, ScriptureJson> _loadedScriptures = new Dictionary<string, ScriptureJson>();

    public static Scripture LoadRandomScripture(string filePath)
    {
        try
        {
            if (!_loadedScriptures.ContainsKey(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                _loadedScriptures[filePath] = JsonSerializer.Deserialize<ScriptureJson>(jsonString);
            }

            var scriptureData = _loadedScriptures[filePath];
            
            // Select a random book
            var book = scriptureData.books[_random.Next(scriptureData.books.Count)];
            
            // Select a random chapter
            var chapter = book.chapters[_random.Next(book.chapters.Count)];
            
            // Decide if we want a single verse or multiple verses (30% chance of multiple)
            bool multipleVerses = _random.NextDouble() < 0.3;
            
            if (multipleVerses && chapter.verses.Count > 1)
            {
                // Select 2-4 consecutive verses
                int verseCount = _random.Next(2, Math.Min(5, chapter.verses.Count));
                int startVerseIndex = _random.Next(0, chapter.verses.Count - verseCount + 1);
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
                var verse = chapter.verses[_random.Next(chapter.verses.Count)];
                
                return new Scripture(
                    new Reference(book.book, chapter.chapter, verse.verse),
                    verse.text
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scriptures: {ex.Message}");
            // Return a default scripture as fallback
            return new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life"
            );
        }
    }

    public static Scripture LoadRandomScriptureFromMultipleFiles(string[] filePaths)
    {
        string randomFile = filePaths[_random.Next(filePaths.Length)];
        return LoadRandomScripture(randomFile);
    }
}