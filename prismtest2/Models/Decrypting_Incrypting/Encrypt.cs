using prismtest2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prismtest2.Models.Decrypting_Incrypting
{
    public class Encrypt
    {
        private readonly DataBaseContext context;

        public Encrypt()
        {
            context = new DataBaseContext();
        }

        public byte[] Encrypting(byte[] plainBytes, byte[] key, byte[] iv)
        {
            byte[] encryptedBytes = null;
            // Set up the encryption objects
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Encrypt the input plaintext using the AES algorithm
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }

            return encryptedBytes;
        }
    }
}
