using Model;

namespace UnitTests;

public class Tests
{
    
    private Matrix m = new Matrix("zuhause");
    private Matrix keymatrix = new Matrix("key");
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MatrixGridTest()
    {
        Assert.That(25, Is.EqualTo(m.Grid.Length));
    }
    
    [Test]
    public void MatrixLettersTest()
    {
        HashSet<char> letters = new HashSet<char>();
        foreach (char letter in m.Grid)
        {
            letters.Add(letter);
        }

        Assert.That(letters.Count, Is.EqualTo(m.Grid.Length));
    }
    #region SpecificTests
    [Test]
    public void SpecificEncryptionTest()
    {
        
        string encryption = m.Encrypt("ha");
        Assert.That("tcdkeqqk", Is.EqualTo(encryption));
    }
    [Test]
    public void SpecificDecryptionTest()
    {
        
        string decryption = m.Decrypt("eghiwsmirf");
        Assert.Equals(decryption, "decryption");
    }
    [Test]
    public void CorrectEncryptionTest()
    {
        string encryption = keymatrix.Encrypt("test encryption");
        Assert.That("qbtpalfpkrpoio", Is.EqualTo(encryption));
    }
    [Test]
    public void CorrectDecryptionTest()
    {
        string decryption = keymatrix.Decrypt("qbtp ldfpkrpoio");
        Assert.Equals(decryption, "test decryption");
    }
    #endregion

    #region MethodEncryptionTests
    [Test]
    public void VerticalMethodEncryptionTest()
    {
        
        string encryption = keymatrix.Encrypt("gn");
        Assert.That("ns", Is.EqualTo(encryption));
    }
    [Test]
    public void RectangleMethodEncryptionTest()
    {
        string encryption = keymatrix.Encrypt("dn");
        Assert.That("gl", Is.EqualTo(encryption));
    }
    [Test]
    public void HorizontalMethodEncryptionTest()
    {
        string encryption = keymatrix.Encrypt("ey");
        Console.WriteLine(encryption);
        Assert.That(encryption, Is.EqualTo("ya"));
    }
    #endregion

    #region MethodDecryptionTests
    [Test]
    public void VerticalMethodDecryptionTest()
    {
        string decryption = keymatrix.Decrypt("ho");
        Assert.Equals(decryption, "bh");
    }
    [Test]
    public void RectangleMethodDecryptionTest()
    {
        string decryption = keymatrix.Decrypt("tm");
        Assert.Equals(decryption, "ro");
    }
    [Test]
    public void HorizontalMethodDecryptionTest()
    {
        string decryption = keymatrix.Decrypt("mn");
        Assert.Equals(decryption, "lm");
    }
    #endregion   

}