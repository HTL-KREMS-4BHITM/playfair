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
        throw new NotImplementedException();
    }

    public string Decrypt(string msg)
    {
        throw new NotImplementedException();
    }

    private char[,] GenerateMatrix(char[] key)
    {
        int alphabetCounter = 0;
        char[] alphabet = GetAlphabet(key);
        char[,] matrix = new char[5, 5];
        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 5; j++)
            {
                if (j + i * 5 < key.Length)
                {
                    matrix[i, j] = key[j + i * 5];
                }
                else
                {
                    matrix[i, j] = alphabet[alphabetCounter];
                    alphabetCounter++;
                }
          
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
        char[] keyArray = new char[key.Length];
        for (int i = 0; i < key.Length; i++)
        {
            if (keyArray.Contains(key[i])==false)
                keyArray[i] = key[i];
        }
        return new string(keyArray);
    }
}