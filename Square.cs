namespace ClaimTheSquareConsole;

public class Square
{
    private readonly int _index;
    private string? _text;
    private GameColor? _foreColor;
    private GameColor? _backColor;

    public Square(int index)
    {
        _index = index;
    }

    public bool IsEmpty()
    {
        return _text == null;
    }

    public ClaimResult Claim(string text, GameColor foreColor, GameColor backColor)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new ClaimResult(false, "Teksten kan ikke være tom.");
        }

        if (IsEmpty())
        {
            _text = text;
            _foreColor = foreColor;
            _backColor = backColor;
            return new ClaimResult(true, "");
        }

        if (text != _text)
        {
            return new ClaimResult(false, "Ruten er allerede tatt. Du må skrive nøyaktig samme tekst for å kunne bytte farger.");
        }

        if (foreColor.Name != _backColor!.Name || backColor.Name != _foreColor!.Name)
        {
            return new ClaimResult(false, "Fargene må være nøyaktig motsatt av før.");
        }

        _foreColor = foreColor;
        _backColor = backColor;
        return new ClaimResult(true, "");
    }

    public TextObjectDto ToDto()
    {
        return new TextObjectDto
        {
            Index = _index,
            Text = _text!,
            ForeColor = _foreColor!.Name,
            BackColor = _backColor!.Name
        };
    }

    public string? LoadFromDto(TextObjectDto textObject)
    {
        if (textObject.Index != _index)
        {
            return "DTO-en hører til en annen rute.";
        }

        if (string.IsNullOrWhiteSpace(textObject.Text))
        {
            return "tekst mangler.";
        }

        var foreColor = GameColor.Create(textObject.ForeColor);
        if (foreColor == null)
        {
            return "forgrunnsfargen er ugyldig.";
        }

        var backColor = GameColor.Create(textObject.BackColor);
        if (backColor == null)
        {
            return "bakgrunnsfargen er ugyldig.";
        }

        _text = textObject.Text;
        _foreColor = foreColor;
        _backColor = backColor;
        return null;
    }

    public void Show()
    {
        var oldForeColor = Console.ForegroundColor;
        var oldBackColor = Console.BackgroundColor;

        if (IsEmpty())
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" {_index,2}    ");
        }
        else
        {
            Console.ForegroundColor = _foreColor!.ConsoleColor;
            Console.BackgroundColor = _backColor!.ConsoleColor;
            Console.Write($" {ShortText(_text!),-5} ");
        }

        Console.ForegroundColor = oldForeColor;
        Console.BackgroundColor = oldBackColor;
    }

    private string ShortText(string text)
    {
        if (text.Length <= 5)
        {
            return text;
        }

        return text.Substring(0, 5);
    }
}
