using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace npivalidator
{
    class Program
    {
        private const string token = "3932f3b0-cfab-11dc-95ff-0800200c9a663932f3b0-cfab-11dc-95ff-0800200c9a66";
        
        static void Main(string[] args)
        {
            try
            {
                string fileContent = readFirstLineOfFile("npis.csv");
                IEnumerable<int> npis = StringToIntList(fileContent);
                Console.WriteLine($"{npis.Count()} NPIs read");

                foreach (int npi in npis)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string restEndPoint = $"http://www.hipaaspace.com/api/npi/check_status?q={npi}&token={token}";
                        string response = client.GetStringAsync(restEndPoint).Result;
                        Console.WriteLine(response);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong...");
            }
        }

        private static string readFirstLineOfFile(string filename)
        {
            string lineOne;
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                lineOne = reader.ReadLine();
            }
            fileStream.Close();
            return lineOne;
        }

        private static IEnumerable<int> StringToIntList(string str)
        {
            return (str ?? "").Split(',').Select<string, int>(int.Parse);
        }
    }
}
