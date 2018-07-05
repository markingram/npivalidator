using System;
using System.IO;

namespace npivalidator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">>> Main");
            string fileContent = readFirstLineOfFile("npis.csv");
            Console.WriteLine("<<< Main");
        }

        private static string readFirstLineOfFile(string filename)
        {
            string lineOne;
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                lineOne = reader.ReadLine();
                Console.WriteLine(lineOne);
            }
            fileStream.Close();
            return lineOne;
        }
    }
}
