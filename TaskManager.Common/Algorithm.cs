using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common
{
    public static class Algorithm
    {
        public static string Encrypt(string texto, string key)
        {

            if (!string.IsNullOrEmpty(texto))
            {
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar =
                  Encoding.UTF8.GetBytes(texto);

                MD5CryptoServiceProvider hashmd5 =
                  new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(
                  Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes =
                  new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform =
                  tdes.CreateEncryptor();

                byte[] ArrayResultado =
                  cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
                    0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                return Convert.ToBase64String(ArrayResultado,
                  0, ArrayResultado.Length);
            }

            return string.Empty;
        }


        public static string Decrypt(string textoEncriptado, string key)
        {
            if (!string.IsNullOrEmpty(textoEncriptado))
            {
                byte[] keyArray;
                byte[] Array_a_Descifrar =
                  Convert.FromBase64String(textoEncriptado);

                MD5CryptoServiceProvider hashmd5 =
                  new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(
                  Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes =
                  new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform =
                  tdes.CreateDecryptor();

                byte[] resultArray =
                  cTransform.TransformFinalBlock(Array_a_Descifrar,
                    0, Array_a_Descifrar.Length);

                tdes.Clear();
                return Encoding.UTF8.GetString(resultArray);
            }

            return string.Empty;
        }

        public static byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        public static string HashPassword(byte[] salt, string value)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: value,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA256,
              iterationCount: 1000,
              numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
