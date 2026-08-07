using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class CodeGenerator
    {
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string GenerateRandomCode(int length = 6)
        {
            char[] code = new char[length];

            for (int i = 0; i < length; i++)
            {
                int randomIndex = RandomNumberGenerator.GetInt32(_chars.Length);
                code[i] = _chars[randomIndex];
            }
            return new string(code);
        }
    }
}
