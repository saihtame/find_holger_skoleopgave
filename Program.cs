using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Xml.Schema;

internal record Vector2i
{
    public int x;
    public int y;

    public Vector2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector2i GetRandom(int xMax, int yMax)
    {
        Random r = new();
        Vector2i v = new(r.Next(xMax), r.Next(yMax));
        return v;
    }
}

internal class Program
{
    // Parameters
    const int width = 40;
    const int height = 20;
    const int cellWidth = 2;

    private static void Main(string[] args)
    {
        // Determine position to spawn @
        Vector2i target = Vector2i.GetRandom(width, height);

        PrintHeader();
        PrintBoard(target);
        Vector2i answer = GetAnswer();

        // Print result
        if (answer == target)
        {
            Console.WriteLine("Correct answer!");
        } else
        {
            Console.WriteLine("Wrong answer!");
            Console.WriteLine("Correct answer: " + (target.x + 1).ToString() + " " + (height - target.y).ToString());
        }

        // Wait for keypress before we close the console
        Console.ReadKey();
    }

    private static void PrintHeader()
    {
        // Start with empty space to compensate for row headers
        String header = "".PadRight(cellWidth * 2);

        // Create header number for every second column
        for (int i = 0; i < width; i += 2)
        {
            String num = (i + 1).ToString();
            header += num.PadRight(cellWidth * 2);
        }

        Console.WriteLine(header);
    }

    private static void PrintBoard(Vector2i target)
    {
        ConsoleColor initialColor = Console.ForegroundColor;
        // Used to generate random chars
        Random r = new();
        // Iterate by line
        for (int y = 0; y < height; y++)
        {
            // Print row header
            Console.Write((height - y).ToString().PadLeft(cellWidth) + "-".PadRight(cellWidth));
            // Generate random line
            for (int x = 0; x < width; x++)
            {
                // Change to a random color
                Console.ForegroundColor = (ConsoleColor)r.Next(1, 15);
                // Check if character should be replaced with @
                if (y == target.y && x == target.x)
                {
                    Console.Write("@".PadRight(cellWidth));
                } else
                {
                    // Add random character to line
                    Console.Write(Convert.ToChar(r.Next(65, 91)).ToString().PadRight(cellWidth));
                }
                // Switch console color back to normal
                Console.ForegroundColor = initialColor;
            }
            // Go to next line
            Console.WriteLine();
        }
    }

    private static Vector2i GetAnswer()
    {
        // Loop forever until we get a valid answer
        while (true)
        {
            // Get an answer string from the user
            String? answer = null;
            while (answer == null)
            {
                answer = Console.ReadLine();
            }

            // Split answer between spaces
            String[] answerSplit = answer.Split(" ");
            if (answerSplit.Length != 2)
            {
                continue;
            }

            int x;
            int y;
            // Try to parse x coordinate
            bool res;
            res = int.TryParse(answerSplit[0], out x);
            if (!res)
            {
                continue;
            }
            // Try to parse y coordinate
            res = int.TryParse(answerSplit[1], out y);
            if (!res)
            {
                continue;
            }

            // Return true if all was succesfull
            return new Vector2i(x-1, height-y);
        }
    }

}