using System;
using System.IO;
using System.Linq;

namespace TempNoteBook
{
    class Program
    {
        public static void Encode(ref string text, StreamReader first_note, string fileName)
        {
            char[] string_from_note = new char[text.Length];
            string deleted_message = null;
            char[] tmp = text.ToCharArray();
            first_note.Read(string_from_note, 0, text.Length);
            deleted_message = first_note.ReadToEnd();
            first_note.Close();
            //XOR
            for (int i = 0; i < text.Length; ++i)
                tmp[i] ^= string_from_note[i];
            //Del used symbols
            using (var writer = new StreamWriter(fileName))
            {
                deleted_message = deleted_message.Remove(0, string_from_note.Length);
                writer.Write(deleted_message);
            }
            text = null;
            for (int i = 0; i < tmp.Length; ++i)
                text += tmp[i];
        }
        public static void Decode(ref string text, StreamReader second_note, string fileName)
        {
            char[] string_from_note = new char[text.Length];
            string deleted_message = null;
            char[] tmp = text.ToCharArray();
            second_note.Read(string_from_note, 0, text.Length);
            deleted_message = second_note.ReadToEnd();
            second_note.Close();
            //XOR
            for (int i = 0; i < text.Length; ++i)
                tmp[i] ^= string_from_note[i];
            //Del used symbols
            using (var writer = new StreamWriter(fileName))
            {
                deleted_message = deleted_message.Remove(0, string_from_note.Length);
                writer.Write(deleted_message);
            }
            text = null;
            for (int i = 0; i < tmp.Length; ++i)
                text += tmp[i];
        }
        static void Main(string[] args)
        {
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] letters = new char[1000];
            var rand = new Random();

            for (int i = 0; i < 1000; ++i)
                letters[i] = (char)rand.Next(65, 91);

            //Chcking working
            using (var writer = new StreamWriter("check.txt"))
            {
                for (int i = 0; i < 1000; ++i)
                    writer.Write(letters[i]);
            }

            //Generating new note
            using (var writer = new StreamWriter("first.txt"))
            {
                for (int i = 0; i < 1000; ++i)
                    writer.Write(letters[i]);
            }
            //Generating new note
            using (var writer = new StreamWriter("second.txt"))
            {
                for (int i = 0; i < 1000; ++i)
                    writer.Write(letters[i]);
            }
            //Text to encrypt
            string text = "Encode this message, or u will die!";
            text = text.ToUpper();
            //Remove not containments symbols 
            string tmp = new string(alphabet);
            for (int i = 0; i < text.Length; ++i)
                if (!tmp.Contains(text[i]))
                    text = text.Remove(i--, 1);
            Console.WriteLine("First text\n" + text);
            Encode(ref text, new StreamReader("first.txt"), "first.txt");
            Console.WriteLine("Encrypted text\n" + text);
            Decode(ref text, new StreamReader("second.txt"), "second.txt");
            Console.Write("Decrypted text\n");
            Console.WriteLine(text);
        }
    }
}