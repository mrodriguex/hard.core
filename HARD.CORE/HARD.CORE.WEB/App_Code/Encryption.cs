using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de Encryption
/// </summary>
public class Encryption
{
    #region "Singleton"

    private static Encryption instance = null;

    private static object mutex = new object();
    private Encryption()
    {
    }

    public static Encryption GetInstance()
    {

        if (instance == null)
        {
            lock ((mutex))
            {
                instance = new Encryption();
            }
        }

        return instance;

    }

    #endregion    

    public string EncryptQS(string text)
    {
        return HttpUtility.UrlEncode(Encrypt(text.Trim()));
    }

    public string DecryptQS(string text)
    {
        return Decrypt(HttpUtility.UrlDecode(text));
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "CRY#2020.TI";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using (MemoryStream ms = new MemoryStream())
            {

                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }

                clearText = Convert.ToBase64String(ms.ToArray());

            }

        }

        return clearText;

    }

    private string Decrypt(string cipherText)
    {

        string EncryptionKey = "CRY#2020.TI";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes encryptor = Aes.Create())
        {

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using (MemoryStream ms = new MemoryStream())
            {

                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }

                cipherText = Encoding.Unicode.GetString(ms.ToArray());

            }

        }

        return cipherText;

    }

}