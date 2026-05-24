namespace ClaimTheSquareConsole;

public class Board
{
    public const int Width = 8;
    public const int Height = 8;
    public const int SquareCount = Width * Height;

    private readonly Square[] _squares;

    public Board()
    {
        _squares = new Square[SquareCount];

        for (var index = 0; index < _squares.Length; index++)
        {
            _squares[index] = new Square(index);
        }
    }

    public void Show()
    {
        for (var row = 0; row < Height; row++)
        {
            for (var col = 0; col < Width; col++)
            {
                var index = row * Width + col;
                _squares[index].Show();
            }

            Console.WriteLine();
        }
    }

    public ClaimResult ClaimSquare(
        int index,
        string text,
        GameColor foreColor,
        GameColor backColor)
    {
        if (index < 0 || index >= _squares.Length)
        {
            return new ClaimResult(false, "Ugyldig rute.");
        }

        return _squares[index].Claim(text, foreColor, backColor);
    }

    public List<TextObjectDto> ToDtos()
    {
        var textObjects = new List<TextObjectDto>();

        foreach (var square in _squares)
        {
            if (!square.IsEmpty())
            {
                textObjects.Add(square.ToDto());
            }
        }

        return textObjects;
    }

    public void LoadFrom(List<TextObjectDto> textObjects)
    {
        var hasMessage = false;

        foreach (var textObject in textObjects)
        {
            if (textObject.Index < 0 || textObject.Index >= _squares.Length)
            {
                Console.WriteLine($"Hopper over rute {textObject.Index}: ugyldig indeks.");
                hasMessage = true;
                continue;
            }

            var errorMessage = _squares[textObject.Index].LoadFromDto(textObject);
            if (errorMessage != null)
            {
                Console.WriteLine($"Hopper over rute {textObject.Index}: {errorMessage}");
                hasMessage = true;
            }
        }

        if (hasMessage)
        {
            Console.Write("Trykk Enter for å fortsette...");
            Console.ReadLine();
        }
    }
}
