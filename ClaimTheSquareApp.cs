using System.Text.Json;

namespace ClaimTheSquareConsole;

public class ClaimTheSquareApp
{
    private readonly Board _board;
    private readonly string _fileName;
    private readonly JsonSerializerOptions _jsonOptions;

    public ClaimTheSquareApp(string fileName)
    {
        _board = new Board();
        _fileName = fileName;
        _jsonOptions = new JsonSerializerOptions { WriteIndented = true };
    }

    public void Run()
    {
        LoadFromJson();

        while (true)
        {
            ClearConsole();
            Console.WriteLine("Claim the Square");
            Console.WriteLine();
            _board.Show();
            Console.WriteLine();
            Console.WriteLine("1. Velg rute");
            Console.WriteLine("2. Avslutt");
            Console.Write("Hva vil du gjøre? ");

            var choice = Console.ReadLine();
            if (choice == "1")
            {
                ChooseSquare();
            }
            else if (choice == "2")
            {
                return;
            }
        }
    }

    private void ChooseSquare()
    {
        Console.WriteLine();
        var index = ReadSquareIndex();
        var text = ReadText();

        Console.WriteLine();
        Console.WriteLine("Velg forgrunnsfarge:");
        var foreColor = ColorMenu.ReadColor();

        Console.WriteLine();
        Console.WriteLine("Velg bakgrunnsfarge:");
        var backColor = ColorMenu.ReadColor();

        var wasChanged = _board.ClaimSquare(index, text, foreColor, backColor, out var message);
        Console.WriteLine();
        Console.WriteLine(message);

        if (wasChanged)
        {
            SaveToJson();
            Console.WriteLine("Endringen ble lagret til JSON.");
        }

        Console.WriteLine();
        Console.Write("Trykk Enter for å fortsette...");
        Console.ReadLine();
    }

    private int ReadSquareIndex()
    {
        while (true)
        {
            Console.Write("Velg rute fra 0 til 63: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out var index) && index >= 0 && index < Board.SquareCount)
            {
                return index;
            }

            Console.WriteLine("Ugyldig rute. Skriv et tall fra 0 til 63.");
        }
    }

    private string ReadText()
    {
        while (true)
        {
            Console.Write("Tekst: ");
            var text = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            Console.WriteLine("Teksten kan ikke være tom.");
        }
    }

    private void LoadFromJson()
    {
        if (!File.Exists(_fileName))
        {
            return;
        }

        try
        {
            var json = File.ReadAllText(_fileName);
            var textObjects = JsonSerializer.Deserialize<List<TextObjectDto>>(json);
            if (textObjects == null)
            {
                return;
            }

            _board.LoadFrom(textObjects);
        }
        catch
        {
            Console.WriteLine("Klarte ikke å lese JSON-filen. Appen starter med tomt brett.");
            Console.Write("Trykk Enter for å fortsette...");
            Console.ReadLine();
        }
    }

    private void SaveToJson()
    {
        var textObjects = _board.ToDtos();
        var json = JsonSerializer.Serialize(textObjects, _jsonOptions);
        File.WriteAllText(_fileName, json);
    }

    private void ClearConsole()
    {
        try
        {
            Console.Clear();
        }
        catch
        {
            // Console.Clear fungerer ikke når programmet kjøres med omdirigert input/output.
        }
    }
}
