namespace Model;

public interface DeEnCrypt
{
    public string Encrypt(string msg);
    public string Decrypt(string msg);
}