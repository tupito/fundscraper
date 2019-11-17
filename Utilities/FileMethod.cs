using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FundScraperCore.Investments;

namespace FundScraperCore.Utilities
{
    class FileMethod
    {
        ConsoleWriteMethod consoleWriter = new ConsoleWriteMethod();

        // Collection (List) of Funds (rahasto) will be generated here from json file
        public List<FundInvestment> CreateFundListFromJson()
        {
            //get input.json to string
            string filename = "input.json";

            string json = File.ReadAllText(filename);

            //deserialize json data, create Fund-objects and add Fund objects to List
            List<FundInvestment> funds = JsonConvert.DeserializeObject<List<FundInvestment>>(json);

            // info for user
            consoleWriter.WriteInfoFileRead(Path.GetFullPath(filename));

            return funds;
        }

        // write collection to json file
        public void ToJsonFile(List<FundInvestment> funds) //using Newtonsoft.Json;
        {
            JsonSerializer serializer = new JsonSerializer();
            string filename = "output.json";
            StreamWriter sw = new StreamWriter(filename);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, funds);
            }
            
            // info for user
            consoleWriter.WriteInfoFileGenerated(Path.GetFullPath(filename));
        }

        // Will set Fund.TopRegions from a file (used only if web scraper has been triggered during session)
        public void SetFundTopRegionsFromFile(List<FundInvestment> funds)
        {
            foreach (FundInvestment fund in funds)
            {
                string[] fileLines = File.ReadAllLines($"{fund.InvestmentTarget}.txt");
                Dictionary<string, double> topRegions = new Dictionary<string, double>();

                foreach (string line in fileLines)
                {
                    string[] splitLine = line.Split(':');
                    string region = splitLine[0];
                    double percentage = Double.Parse(splitLine[1]);
                    topRegions.Add(region, percentage);
                }
                fund.TopRegionsAndPercentages = topRegions;
            }
        }

        // write dictionary data to txt-file 
        public void DictionaryToTxtFile(Dictionary<string, double> dict, string filename)
        {
            File.Delete($"{filename}.txt"); //delete old saved data
            Console.WriteLine("Info: {0}.txt deleted", filename);

            foreach (KeyValuePair<string, double> entry in dict)
            {
                File.AppendAllText($"{filename}.txt", $"{entry.Key}:{entry.Value}\n"); //region and percentage
            }

            Console.WriteLine("Info: {0}.txt created", Path.GetFullPath(filename));
        }
    }
}
