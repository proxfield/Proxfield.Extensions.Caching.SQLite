using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Proxfield.Extensions.Caching.SQLite.Security
{
    /// <summary>
    /// Provides encryption using AES
    /// </summary>
    public class AesProvider : EncryptionProvider
    {
        //<inheritdoc />
        public override string? Key { get; set; }

        //<inheritdoc />
        public AesProvider(string? key) : base(key)
        {
        }

        //<inheritdoc />
        public override string Encrypt(string text)
        {
            var key = Encoding.UTF8.GetBytes(Key!);
            using var aesAlg = Aes.Create();
            using var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }
            var iv = aesAlg.IV;
            var decryptedContent = msEncrypt.ToArray();
            var result = new byte[iv.Length + decryptedContent.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);
            return Convert.ToBase64String(result);
        }

        //<inheritdoc />
        public override string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - 16];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            var key = Encoding.UTF8.GetBytes(Key!);
            using var aesAlg = Aes.Create();
            using var decryptor = aesAlg.CreateDecryptor(key, iv);
            string result;
            using (var msDecrypt = new MemoryStream(cipher))
            {
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                result = srDecrypt.ReadToEnd();
            }
            return result;
        }
    }
}