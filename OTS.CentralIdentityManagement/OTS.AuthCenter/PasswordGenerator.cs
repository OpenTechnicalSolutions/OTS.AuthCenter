using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter
{
    public class PasswordGenerator
    {
        private static string _alphachar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string _numchar = "1234567890";
        private static string _specchar = "!@#$%^&*()+-";

        public static string Generate()
        {
            string newpass = string.Empty;
            var rnd = new Random();

            for(int i = 0;i < 5;i++)
                newpass += _alphachar[rnd.Next(_alphachar.Length - 1)];

            for (int i = 0; i < 5; i++)
                newpass += _numchar[rnd.Next(_numchar.Length - 1)];

            for (int i = 0; i < 2; i++)
                newpass += _specchar[rnd.Next(_specchar.Length - 1)];

            return newpass;
        }
    }
}
