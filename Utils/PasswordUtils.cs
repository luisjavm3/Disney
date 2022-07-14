namespace Disney.Utils
{
    public static class PasswordUtils
    {
        public static void GetHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool MatchHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHast = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (var i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != computedHast[i])
                        return false;
                }
            }

            return true;
        }
    }
}