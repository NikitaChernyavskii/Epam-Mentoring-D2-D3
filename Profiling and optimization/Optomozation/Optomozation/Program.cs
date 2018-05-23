using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Optomozation
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneratePasswordHashUsingSalt("Tantik", new byte[100]);
            Console.ReadLine();
        }

        private static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            //1. As we copy 16 hardcoded bytes - we should check length of salt. If it's too small there is no need to start hashing
            if (salt.Length < 16)
            {
                throw new ArgumentException($"salt lenght must be 16 or greater. but its lenght is ${salt.Length}");
            }

            // 2. removed redundant variables

            byte[] hash = new Rfc2898DeriveBytes(passwordText, salt, 10000).GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

    }
}
