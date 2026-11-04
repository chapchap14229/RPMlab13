using System;
using System.IO;

namespace Lab13
{
    public class LoginService
    {
        private const string UsersFileName = "users.txt";

        // Возвращает имя пользователя, если логин и пароль верны. Иначе null.
        public string Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, UsersFileName);

            if (!File.Exists(filePath))
                return null;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("#")) continue;

                string[] parts = line.Split(';');
                if (parts.Length < 3) continue;

                if (parts[0] == login && parts[1] == password)
                {
                    return string.IsNullOrEmpty(parts[2]) ? login : parts[2];
                }
            }

            return null;
        }
    }
}