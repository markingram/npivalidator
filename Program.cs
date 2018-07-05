using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace npivalidator
{
    class Program
    {   
        static void Main(string[] args)
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                
                string token = config["access_token"];

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
            catch (Exception e)
            {
                Console.WriteLine(e);
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
