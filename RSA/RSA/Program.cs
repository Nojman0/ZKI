using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace rsa
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoding = Encoding.GetEncoding(1251);
            StreamReader sr = new StreamReader("text.txt", encoding: encoding);
            string line = sr.ReadToEnd();
            Console.WriteLine(line);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(line);
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.Write(bytes[i]);
            }
            Console.WriteLine();
            string newline = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(newline);
            byte[] enbytes = rsa.Encrypt(bytes, true);
            for (int i = 0; i < enbytes.Length; i++)
            {
                Console.Write(enbytes[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            string newline2 = Encoding.UTF8.GetString(enbytes);

            byte[] debytes = rsa.Decrypt(enbytes, true);
            for (int i = 0; i < debytes.Length; i++)
            {
                Console.Write(debytes[i]);
            }
            Console.WriteLine();
            string newline3 = Encoding.UTF8.GetString(debytes);
            Console.WriteLine(newline3);
        }
    }
}
