namespace ClaimTheSquareConsole;

public class ColorMenu
{
    public static GameColor ReadColor()
    {
        while (true)
        {
            ShowColors();
            Console.Write("Farge: ");
            var input = Console.ReadLine();
            var color = GameColor.Create(input);

            if (color != null)
            {
                return color;
            }

            Console.WriteLine("Ugyldig farge. Velg en farge fra listen.");
        }
    }

    private static void ShowColors()
    {
        foreach (var color in GameColor.GetAll())
        {
            var oldForeColor = Console.ForegroundColor;
            Console.ForegroundColor = color.ConsoleColor;
            Console.Write(color.Name);
            Console.ForegroundColor = oldForeColor;
            Console.Write(" ");
        }

        Console.WriteLine();
    }
}
