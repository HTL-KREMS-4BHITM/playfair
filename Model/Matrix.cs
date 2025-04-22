using System.Text;

namespace Model;

public class Matrix : IChiffre
{
    public string Key { get; set; }
    public char[,] Grid { get; set; }
    public Matrix(string key)
    {
        Key = key.Replace('j', 'i');
        Key = CheckKeyForduplicates(Key);
        Grid = GenerateMatrix(Key.ToCharArray());
    }
    public string Encrypt(string msg)
    {
        // Normalize input: remove spaces, replace 'j' with 'i', etc.
        msg = msg.ToLower().Replace("j", "i").Replace(" ", "");

        // Prepare digraphs (pairs of    characters)
        List<string> pairs = new List<string>();
        for (int i = 0; i < msg.Length; i += 2)
        {
            char first = msg[i];
            char second;

            if (i + 1 >= msg.Length)
            {
                second = 'x'; // pad with 'x' if odd number of chars
            }
            else if (msg[i] == msg[i + 1])
            {
                second = 'x'; // insert filler between duplicate letters
                i--; // redo second letter on next loop
            }
            else
            {
                second = msg[i + 1];
            }

            pairs.Add($"{first}{second}");
        }

        // Encrypt each pair
        StringBuilder encrypted = new StringBuilder();
        foreach (var pair in pairs)
        {
            var pos1 = CheckMethode(pair[0]);
            var pos2 = CheckMethode(pair[1]);

            if (pos1.y == pos2.y) // Same row
            {
                encrypted.Append(CheckHorizontal(pair[0], pair[1]));
            }
            else if (pos1.x == pos2.x) // Same column
            {
                encrypted.Append(CheckVertikal(pair[0], pair[1]));
            }
            else // Rectangle case
            {
                encrypted.Append(CheckRectangle(pair[0], pair[1]));
            }
        }

        return encrypted.ToString();
    }


    public string Decrypt(string msg)
    {
        throw new NotImplementedException();
    }
    
    #region Crypting

    private (int x, int y) CheckMethode(char c1)
    {
        int x = 0, y = 0;
        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 5; j++)
            {
                if (Grid[i, j] == c1)
                {
                    x = j;
                    y = i; 
                }
         
          
            }
        }
        return (x, y);
    }

    private string CheckHorizontal(char c1, char c2)
    {
        var (x1, y1) = CheckMethode(c1);
        var (x2, y2) = CheckMethode(c2);

        // Wrap around horizontally using modulo
        char newletter = Grid[y1, (x1 + 1) % 5];
        char secondletter = Grid[y2, (x2 + 1) % 5];

        return new string(new[] { newletter, secondletter });
    }

    private string CheckVertikal(char c1, char c2)
    {
        var (x1, y1) = CheckMethode(c1);
        var (x2, y2) = CheckMethode(c2);

        // Wrap around vertically using modulo
        char newletter = Grid[(y1 + 1) % 5, x1];
        char secondletter = Grid[(y2 + 1) % 5, x2];

        return new string(new[] { newletter, secondletter });
    }

    private string CheckRectangle(char c1, char c2)
    {
        var (x1, y1) = CheckMethode(c1);
        var (x2, y2) = CheckMethode(c2);

        char newletter = Grid[y1, x2];
        char secondletter = Grid[y2, x1];

        return new string(new[] { newletter, secondletter });
    }
    #endregion
    
    
    
    #region Matrix
    private char[,] GenerateMatrix(char[] key)
    {
        char[,] matrix = new char[5, 5];
        List<char> merged = new List<char>(key);
        merged.AddRange(GetAlphabet(key)); // Fill up with missing letters

        int index = 0;
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                matrix[row, col] = merged[index];
                index++;
            }
        }

        return matrix;
    }




    private char[] GetAlphabet(char[] key)
    {
        List<char> alphabet = new List<char> {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
            't', 'u', 'v', 'w', 'x', 'y', 'z'
        };
        for (int i = 0; i < key.Length; i++)
        {
            if (alphabet.Contains(key[i]))
            {
                alphabet.Remove(key[i]);
            }
        }
        return alphabet.ToArray();
    }

    private string CheckKeyForduplicates(string key)
    {
        List<char> result = new List<char>();

        foreach (char c in key)
        {
            if (!result.Contains(c))
            {
                result.Add(c);
            }
        }

        return new string(result.ToArray());
    }

    #endregion
}
