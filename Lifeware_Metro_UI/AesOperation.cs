using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Security.Cryptography;
using System.Threading;

namespace Lifeware_Metro_UI
{
    public class AesOperation
    {


        /// <summary>
        /// Mit der Methode GenerateCoupon wird ein Key (X-Länge) erzeugt jeh nach Anforderung des Schlüssel.
        /// Es wird ein Random über die DateTime-Methode unter Verwendung vom Tick generiert damit dann in der Schleife über den Thread.Sleep(1) 
        /// zufäälig ein Random Char erzeugt wird. => (result += characters[random.Next(0, characters.Count)]
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateCoupon(int length)
        {
            string result = string.Empty;
            Random random = new Random((int)DateTime.Now.Ticks);
            List<string> characters = new List<string>() { };
            for (int i = 48; i < 58; i++)
            {
                characters.Add(((char)i).ToString());
            }
            for (int i = 65; i < 91; i++)
            {
                characters.Add(((char)i).ToString());
            }
            for (int i = 97; i < 123; i++)
            {
                characters.Add(((char)i).ToString());
            }
            for (int i = 0; i < length; i++)
            {
                result += characters[random.Next(0, characters.Count)];
                Thread.Sleep(1);
            }
            return result;
        }


        /// <summary>
        /// Mit den der System Kalsse System.Security.Cryptography wird auf die AES und den CryptoStream zugegriffen um die Verschlüsselung durch zu führen.
        /// Dies erfolgt unter Verwendung des Master-Key und der Eingabe aus dem Feldern.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        /// <summary>
        /// Hier wird unter Verwendung des Master-Key und den Daten aus der Datenbank eine Entschlüsselung eingeleitet, diese wird dann in dem Dashbaord
        /// angezeigt.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>


        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }


        }
    }
}
