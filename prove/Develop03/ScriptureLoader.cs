using System.Text.Json;

public class ScriptureLoader
{
    private class ScriptureJson
    {
        private List<Book> _books;
        public List<Book> Books 
        { 
            get => _books;
            set => _books = value;
        }
    }

    private class Book
    {
        private string _bookName;
        private List<Chapter> _chapters;
        
        public string BookName 
        { 
            get => _bookName;
            set => _bookName = value;
        }
        
        public List<Chapter> Chapters
        {
            get => _chapters;
            set => _chapters = value;
        }
    }

    private class Chapter
    {
        private int _chapterNumber;
        private string _reference;
        private List<Verse> _verses;
        
        public int ChapterNumber
        {
            get => _chapterNumber;
            set => _chapterNumber = value;
        }
        
        public string Reference
        {
            get => _reference;
            set => _reference = value;
        }
        
        public List<Verse> Verses
        {
            get => _verses;
            set => _verses = value;
        }
    }

    private class Verse
    {
        private string _reference;
        private string _text;
        private int _verseNumber;
        
        public string Reference
        {
            get => _reference;
            set => _reference = value;
        }
        
        public string Text
        {
            get => _text;
            set => _text = value;
        }
        
        public int VerseNumber
        {
            get => _verseNumber;
            set => _verseNumber = value;
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
            var book = scriptureData.Books[_random.Next(scriptureData.Books.Count)];
            
            // Select a random chapter
            var chapter = book.Chapters[_random.Next(book.Chapters.Count)];
            
            // Decide if we want a single verse or multiple verses (30% chance of multiple)
            bool multipleVerses = _random.NextDouble() < 0.3;
            
            if (multipleVerses && chapter.Verses.Count > 1)
            {
                // Select 2-4 consecutive verses
                int verseCount = _random.Next(2, Math.Min(5, chapter.Verses.Count));
                int startVerseIndex = _random.Next(0, chapter.Verses.Count - verseCount + 1);
                var selectedVerses = chapter.Verses.Skip(startVerseIndex).Take(verseCount).ToList();
                
                // Combine the verses' text
                string combinedText = string.Join(" ", selectedVerses.Select(v => v.Text));
                
                return new Scripture(
                    new Reference(
                        book.BookName,
                        chapter.ChapterNumber,
                        selectedVerses[0].VerseNumber,
                        selectedVerses[^1].VerseNumber
                    ),
                    combinedText
                );
            }
            else
            {
                // Select a single verse
                var verse = chapter.Verses[_random.Next(chapter.Verses.Count)];
                
                return new Scripture(
                    new Reference(book.BookName, chapter.ChapterNumber, verse.VerseNumber),
                    verse.Text
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