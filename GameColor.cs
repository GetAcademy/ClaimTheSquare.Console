namespace ClaimTheSquareConsole;

public class GameColor
{
    private static readonly string[] ValidColorNames =
    {
        "black",
        "white",
        "gray",
        "red",
        "green",
        "blue",
        "yellow",
        "cyan",
        "magenta"
    };

    private GameColor(string name, ConsoleColor consoleColor)
    {
        Name = name;
        ConsoleColor = consoleColor;
    }

    public string Name { get; }
    public ConsoleColor ConsoleColor { get; }

    public static GameColor? Create(string? colorName)
    {
        if (colorName == null)
        {
            return null;
        }
        var name = colorName.Trim().ToLower();
        if (Array.IndexOf(ValidColorNames, name) == -1)
        {
            return null;
        }

        if (!Enum.TryParse(name, true, out ConsoleColor consoleColor))
        {
            return null;
        }

        return new GameColor(name, consoleColor);
    }

    public static string[] GetValidColorNames()
    {
        var copy = new string[ValidColorNames.Length];
        Array.Copy(ValidColorNames, copy, ValidColorNames.Length);
        return copy;
    }
}
