namespace WebSample.Models.Library
{
    public static class EncryptWithBCrypt
    {
        public static string EncryptString(this string inputString)
        {
            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            string myHash = BCrypt.Net.BCrypt.HashPassword(inputString, mySalt);
            return myHash;
        }
    }
}