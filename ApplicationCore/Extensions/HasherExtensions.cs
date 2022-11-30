using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Extensions
{
    public static class HasherExtensions
    {
        public static string ToSHA256(this string origin)
        {
            byte[] source = Encoding.UTF8.GetBytes(origin);
            using (var mySHA256 = SHA256.Create())
            {
                byte[] hasValue = mySHA256.ComputeHash(source);
                string result = hasValue.Aggregate(string.Empty, (current, t) => current + t.ToString("X2"));

                return result.ToUpper();
            }

        }

        public static string AESEncrypt(string Data, string Key, string IV)
        {
            Byte[] plainBytes = Encoding.UTF8.GetBytes(Data);
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bVector.Length)), bVector, bVector.Length);

            Byte[] Cryptograph = null;

            Rijndael Aes = Rijndael.Create();
            try
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    using (var Encryptor = new CryptoStream(memory, Aes.CreateDecryptor(bKey, bVector), CryptoStreamMode.Write))
                    {
                        Encryptor.Write(plainBytes, 0, plainBytes.Length);
                        Encryptor.FlushFinalBlock();

                        Cryptograph = memory.ToArray();
                    }
                }
            }
            catch
            {
                Cryptograph = null;
            }
            return Convert.ToBase64String(Cryptograph);

        }

        public static string EncryptAESHex(string source, string cryptoKey, string cryptoIV)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(source))
            {
                var encryptValue = EncryptAES(Encoding.UTF8.GetBytes(source), cryptoKey, cryptoIV);

                if (encryptValue != null)
                {
                    result = BitConverter.ToString(encryptValue)?.Replace("-", string.Empty)?.ToLower();
                }
            }

            return result;
        }

        public static byte[] EncryptAES(byte[] source, string cryptoKey, string cryptoIV)
        {
            byte[] dataKey = Encoding.UTF8.GetBytes(cryptoKey);
            byte[] dataIV = Encoding.UTF8.GetBytes(cryptoIV);

            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                aes.Key = dataKey;
                aes.IV = dataIV;

                using (var encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(source, 0, source.Length);
                }
            }
        }
    }
}
