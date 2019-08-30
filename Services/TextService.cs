using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public class TextService
{
    /// <summary>
    /// Encrypts a string
    /// </summary>
    /// <param name="Data">The string to encrypt</param>
    /// <returns>Returns an encrypted string</returns>
    public static string Encrypt(string Data)
    {
        try
        {
            string passPhrase = "ZnwDa62kyP44k6I7fJ3XOIlGJpF5hC";
            string saltValue = "9f568L78prUkVVD6fa06hbVcbDxcct";
            string initVector = "koxskfruvdslbsxu";

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(Data);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, "MD5", 1);
            byte[] keyBytes = password.GetBytes(16);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }
        catch { }

        return "";
    }

    /// <summary>
    /// Decrypts a string
    /// </summary>
    /// <param name="Data">The string to decrypt</param>
    /// <returns>Returns a decrypted string</returns>
    public static string Decrypt(string Data)
    {
        try
        {
            string passPhrase = "ZnwDa62kyP44k6I7fJ3XOIlGJpF5hC";
            string saltValue = "9f568L78prUkVVD6fa06hbVcbDxcct";
            string initVector = "koxskfruvdslbsxu";

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(Data);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, "MD5", 1);
            byte[] keyBytes = password.GetBytes(16);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }
        catch { }

        return "";
    }
}