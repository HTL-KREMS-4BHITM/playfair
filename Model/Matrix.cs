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
        msg = msg.ToLower().Replace("j", "i").Replace(" ", "");

        List<string> pairs = new List<string>();
        for (int i = 0; i < msg.Length; i += 2)
        {
            char first = msg[i];
            char second;

            if (i + 1 >= msg.Length)
            {
                second = 'x';
            }
            else if (msg[i] == msg[i + 1])
            {
                second = 'x';
                i--; 
            }
            else
            {
                second = msg[i + 1];
            }

            pairs.Add($"{first}{second}");
        }

        StringBuilder encrypted = new StringBuilder();
        foreach (var pair in pairs)
        {
            var pos1 = CheckMethode(pair[0]);
            var pos2 = CheckMethode(pair[1]);

            if (pos1.y == pos2.y) 
            {
                encrypted.Append(CheckHorizontal(pair[0], pair[1]));
            }
            else if (pos1.x == pos2.x)
            {
                encrypted.Append(CheckVertikal(pair[0], pair[1]));
            }
            else 
            {
                encrypted.Append(CheckRectangle(pair[0], pair[1]));
            }
        }

        return encrypted.ToString();
    }


    public string Decrypt(string msg)
    {
        msg = msg.ToLower().Replace("j", "i").Replace(" ", "");

        List<string> pairs = new List<string>();
        for (int i = 0; i < msg.Length; i += 2)
        {
            char first = msg[i];
            char second = (i + 1 >= msg.Length) ? 'x' : msg[i + 1];
            pairs.Add($"{first}{second}");
        }

        StringBuilder decrypted = new StringBuilder();
        foreach (var pair in pairs)
        {
            var pos1 = CheckMethode(pair[0]);
            var pos2 = CheckMethode(pair[1]);

            if (pos1.y == pos2.y)
            {
                decrypted.Append(CheckHorizontal(pair[0], pair[1], false));
            }
            else if (pos1.x == pos2.x)
            {
                decrypted.Append(CheckVertikal(pair[0], pair[1], false));
            }
            else
            {
                decrypted.Append(CheckRectangle(pair[0], pair[1]));
            }
        }

        return decrypted.ToString();

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

    private string CheckHorizontal(char c1, char c2, bool encrypt = true)
    {
        var (x1, y1) = CheckMethode(c1);
        var (x2, y2) = CheckMethode(c2);

        int shift = encrypt ? 1 : -1;

        char newletter = Grid[y1, (x1 + shift + 5) % 5];
        char secondletter = Grid[y2, (x2 + shift + 5) % 5];

        return new string(new[] { newletter, secondletter });
    }

    private string CheckVertikal(char c1, char c2, bool encrypt = true)
    {
        var (x1, y1) = CheckMethode(c1);
        var (x2, y2) = CheckMethode(c2);

        int shift = encrypt ? 1 : -1;

        char newletter = Grid[(y1 + shift + 5) % 5, x1];
        char secondletter = Grid[(y2 + shift + 5) % 5, x2];

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
