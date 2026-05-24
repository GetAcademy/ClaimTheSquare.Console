namespace ClaimTheSquareConsole;

public class GameColor
{
    public static List<string> ValidColorNames { get; } = new List<string>
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
        if (!ValidColorNames.Contains(name))
        {
            return null;
        }

        if (!Enum.TryParse(name, true, out ConsoleColor consoleColor))
        {
            return null;
        }

        return new GameColor(name, consoleColor);
    }
}
