namespace ClaimTheSquareConsole;

public class GameColor
{
    private static readonly List<string> ValidColorNames = new List<string>
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
        if (!IsValidColorName(name))
        {
            return null;
        }

        if (!Enum.TryParse(name, true, out ConsoleColor consoleColor))
        {
            return null;
        }

        return new GameColor(name, consoleColor);
    }

    public static List<string> GetValidColorNames()
    {
        return ValidColorNames;
    }

    private static bool IsValidColorName(string colorName)
    {
        return ValidColorNames.Contains(colorName);
    }
}
