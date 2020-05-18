using System;
using System.Security.Cryptography;
using System.Text;

namespace MES_IFM_MVC.Models
{
    public static class EncryptData
    {
        public static string RandomSalt(int maximumSaltLength)
        {
            if (maximumSaltLength < 8) maximumSaltLength = 32;
            char randomChar;
            Random random = new Random();
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < maximumSaltLength; i++)
            {
                int ranNumber = random.Next(48, 122);
                if ((ranNumber > 57 && ranNumber < 65) || (ranNumber > 90 && ranNumber < 97)) ranNumber = random.Next(65, 122);
                randomChar = Convert.ToChar(ranNumber);
                randomString.Append(randomChar);
            }
            return randomString.ToString();
        }

        public static string StringToHash(this string data, string salt, HashAlgorithm algorithm)
        {
            byte[] saltedBytes = Encoding.UTF8.GetBytes(data + salt);     // Combine the data with the salt
            byte[] hashedBytes = algorithm.ComputeHash(saltedBytes);      // Compute the hash value of our input
            return BitConverter.ToString(hashedBytes);
        }
    }
}
