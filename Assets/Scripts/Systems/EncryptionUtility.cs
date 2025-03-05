using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionUtility
{
    private static readonly string encryptionKey = "S52OR1J7HPok3iic"; // 16-byte key (128-bit AES)

    // Encrypt method with random IV generation
    public static string Encrypt(string plainText)
    {
        byte[] key = Encoding.UTF8.GetBytes(encryptionKey);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.GenerateIV(); // Generate a random IV for each encryption

            using (MemoryStream ms = new MemoryStream())
            {
                // First, write the IV to the memory stream
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }
                }

                // Return the IV and encrypted text combined as Base64 string
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    // Decrypt method with IV extraction from the cipher text
    public static string Decrypt(string cipherText)
    {
        byte[] key = Encoding.UTF8.GetBytes(encryptionKey);
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            // Extract the IV from the beginning of the cipher text (first 16 bytes)
            byte[] iv = new byte[16];
            Array.Copy(buffer, iv, iv.Length);
            aes.IV = iv;

            using (MemoryStream ms = new MemoryStream(buffer, 16, buffer.Length - 16)) // Skip IV bytes
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (StreamReader reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

     /// <summary>
     /// /////////////////mã code RSA
     /// </summary>
     /// <param name="plainText"></param>
     /// <param name="publicKey"></param>
     /// <returns></returns>

    // RSA Encryption and Decryption
    public static string EncryptWithRSA(string plainText, RSAParameters publicKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);

            byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedData);
        }
    }

    public static string DecryptWithRSA(string cipherText, RSAParameters privateKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(privateKey);

            byte[] decryptedData = rsa.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }

    // Generate RSA Key Pair (for example, to generate the public/private keys)
    public static RSAParameters GenerateRSAKeyPair()
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.KeySize = 2048; // 2048-bit RSA key size

            return rsa.ExportParameters(true); // Export both public and private key
        }
    }
}
