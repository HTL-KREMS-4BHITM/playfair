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
        
        List<string> paars  =new List<string>();
        for (int i = 0; i < msg.Length; i=i+2)
        {
            paars.Add(msg[i] + "" + msg[i+1]);
            
        }

        foreach (var paar in paars)
        {
            if (CheckMethode(paar[0]).x == CheckMethode(paar[1]).x)
            {
                return CheckHorizontal(paar[0], paar[1]);
            }
            else if (CheckMethode(paar[0]).y == CheckMethode(paar[1]).y)
            {
                return CheckVertikal(paar[0], paar[1]);
            }
            else
            {
                //CheckRectangle(paar[0], paar[1]);
            }
            
            
            
            
        }

        return null;
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
        StringBuilder sb = new StringBuilder();
        char newletter = (CheckMethode(c1).y< 5 ?  Grid[CheckMethode(c1).x, CheckMethode(c1).y+1] : Grid[CheckMethode(c1).x, 0]);
        char secondletter = (CheckMethode(c2).y< 5 ?  Grid[CheckMethode(c2).x, CheckMethode(c2).y+1] : Grid[CheckMethode(c2).x, 0]);
        return sb.Append(newletter).Append(secondletter).ToString();
    }

    private string CheckVertikal(char c1, char c2)
    {
        StringBuilder sb = new StringBuilder();
        char newletter = (CheckMethode(c1).x< 5 ?  Grid[CheckMethode(c1).x+1, CheckMethode(c1).y] : Grid[0, CheckMethode(c1).y]);
        char secondletter = (CheckMethode(c2).x< 5 ?  Grid[CheckMethode(c2).x+1, CheckMethode(c2).y] : Grid[0, CheckMethode(c1).y]);
        return sb.Append(newletter).Append(secondletter).ToString();
    }
    #endregion
    
    
    
    #region Matrix
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
    #endregion
}
