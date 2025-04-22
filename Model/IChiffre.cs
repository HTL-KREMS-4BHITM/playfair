namespace Model;

public interface IChiffre
{
    public string Encrypt(string msg);
    public string Decrypt(string msg);
}