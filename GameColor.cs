namespace ClaimTheSquareConsole;

public class GameColor
{
    private static readonly GameColor[] Colors =
    {
        new GameColor("black", ConsoleColor.Black),
        new GameColor("white", ConsoleColor.White),
        new GameColor("gray", ConsoleColor.Gray),
        new GameColor("red", ConsoleColor.Red),
        new GameColor("green", ConsoleColor.Green),
        new GameColor("blue", ConsoleColor.Blue),
        new GameColor("yellow", ConsoleColor.Yellow),
        new GameColor("cyan", ConsoleColor.Cyan),
        new GameColor("magenta", ConsoleColor.Magenta)
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

        foreach (var color in Colors)
        {
            if (color.Name == colorName.Trim().ToLower())
            {
                return color;
            }
        }

        return null;
    }

    public static GameColor[] GetAll()
    {
        var copy = new GameColor[Colors.Length];
        Array.Copy(Colors, copy, Colors.Length);
        return copy;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not GameColor other)
        {
            return false;
        }

        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}
