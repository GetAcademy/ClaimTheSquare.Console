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

    public bool TryClaimOrUpdateColors(
        string text,
        GameColor foreColor,
        GameColor backColor,
        out string message)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            message = "Teksten kan ikke være tom.";
            return false;
        }

        if (IsEmpty())
        {
            _text = text;
            _foreColor = foreColor;
            _backColor = backColor;
            message = $"Rute {_index} ble tatt.";
            return true;
        }

        if (text != _text)
        {
            message = "Ruten er allerede tatt. Du må skrive nøyaktig samme tekst for å kunne bytte farger.";
            return false;
        }

        if (foreColor.Name != _backColor!.Name || backColor.Name != _foreColor!.Name)
        {
            message = "Fargene må være nøyaktig motsatt av før.";
            return false;
        }

        _foreColor = foreColor;
        _backColor = backColor;
        message = $"Fargene på rute {_index} ble byttet.";
        return true;
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

    public bool LoadFromDto(TextObjectDto textObject, out string message)
    {
        if (textObject.Index != _index)
        {
            message = "DTO-en hører til en annen rute.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(textObject.Text))
        {
            message = "tekst mangler.";
            return false;
        }

        var foreColor = GameColor.Create(textObject.ForeColor);
        if (foreColor == null)
        {
            message = "forgrunnsfargen er ugyldig.";
            return false;
        }

        var backColor = GameColor.Create(textObject.BackColor);
        if (backColor == null)
        {
            message = "bakgrunnsfargen er ugyldig.";
            return false;
        }

        _text = textObject.Text;
        _foreColor = foreColor;
        _backColor = backColor;
        message = "OK";
        return true;
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
