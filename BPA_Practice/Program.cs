using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace BPA_Practice
{
    class Program
    {
        // Global Variables
        public const int _key = 10;

        static void Main(string[] args)
        {
            // Initialization
            string[] lines = new string[5];
            int counter = 0;
            string line = string.Empty;

            #region // Encryption

            // Open file
            StreamReader file = new StreamReader(@"C:\Temp\plainText.txt");
            // Interate through lines
            while((line = file.ReadLine()) != null)
            {
                lines[counter] = Encrypt(line);
                counter++;
            }
            // Close file
            file.Close();

            // Output encrypted lines
            Console.WriteLine("Encrypted:");
            foreach(string encryptedLine in lines)
            {
                Console.WriteLine(encryptedLine);
            }

            // Create file
            FileStream fs = File.Create(@"C:\Temp\encryptedText.txt");
            fs.Close();

            // Output to file
            StreamWriter sw = new StreamWriter(@"C:\Temp\encryptedText.txt");
            foreach(string encryptedLine in lines)
            {
                sw.WriteLine(encryptedLine);
            }
            sw.Close();

            #endregion

            #region // Decryption

            // Reset counter
            counter = 0;
            // Open file
            StreamReader decryptFile = new StreamReader(@"C:\Temp\encryptedText.txt");
            // Interate through lines
            while ((line = decryptFile.ReadLine()) != null)
            {
                lines[counter] = Decrypt(line);
                counter++;
            }
            // Close file
            file.Close();

            // Output decrypted lines
            Console.WriteLine("\nDecrypted:");
            foreach (string decryptedLine in lines)
            {
                Console.WriteLine(decryptedLine);
            }

            #endregion
        }

        #region // Main Encypt/Decrypt Functions

        // Full Encrpytion Function
        public static string Encrypt(string input)
        {
            return CeasarCypher(ReverseCypher(input));
        }

        // Full Decryption Function
        public static string Decrypt(string input)
        {
            char[] characters = input.ToCharArray();
            string output = string.Empty;

            foreach (char letter in characters)
            {
                output += CCDecrypter(letter);
            }

            return ReverseCypher(output);
        }

        #endregion

        #region // Cypher Functions

        // Reverse Cypher Function
        public static string ReverseCypher(string input)
        {
            char[] characters = input.ToCharArray();
            string output = string.Empty;
            
            for (int i = characters.Length - 1; i >= 0; i--)
            {
                output += characters[i];
            }

            return output;
        }

        // Ceasar Cypher Function
        public static string CeasarCypher(string input)
        {
            char[] characters = input.ToCharArray();
            string output = string.Empty;

            foreach(char letter in characters)
            {
                output += CCEncrypter(letter, _key);
            }

            return output;
        }

        // Letter Encryption Function
        public static char CCEncrypter(char letter, int key)
        {
            // Check if 'letter' is a char
            if (!char.IsLetter(letter))
            {
                return letter;
            }

            // Convert and output char
            char outLet = char.IsUpper(letter) ? 'A' : 'a';
            return (char)((((letter + key) - outLet) % 26) + outLet);
        }

        // Letter Decryption Function
        public static char CCDecrypter(char input)
        {
            return CCEncrypter(input, 26 - _key);
        }

        #endregion
    }
}
