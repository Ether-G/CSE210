public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;
    private string _originalText;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _originalText = text;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
        _random = new Random();
    }

    public void HideRandomWords(int numberToHide)
    {
        var visibleWords = _words.Where(w => !w.IsHidden()).ToList();
        numberToHide = Math.Min(numberToHide, visibleWords.Count);
        
        for (int i = 0; i < numberToHide; i++)
        {
            if (visibleWords.Count == 0) break;
            
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public string GetDisplayText()
    {
        string wordsDisplay = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} {wordsDisplay}";
    }

    public string GetFullText()
    {
        return $"{_reference.GetDisplayText()} {_originalText}";
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    public bool CheckRecitation(string attempt)
    {
        string normalizedAttempt = string.Join(" ", attempt.Split(new[] { ' ' }, 
            StringSplitOptions.RemoveEmptyEntries));
        string normalizedOriginal = string.Join(" ", _originalText.Split(new[] { ' ' }, 
            StringSplitOptions.RemoveEmptyEntries));
        
        return string.Equals(normalizedAttempt, normalizedOriginal, 
            StringComparison.CurrentCultureIgnoreCase);
    }
}