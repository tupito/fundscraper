using System;
using System.Collections.Generic;
using System.Net.Http;
using FundScraperCore.Investments;
using FundScraperCore.Utilities;

namespace FundScraperCore
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        static readonly HttpClient httpClient = new HttpClient();

        // Most console writers is handled via this object
        static readonly ConsoleWriteMethod consoleWriter = new ConsoleWriteMethod();

        // All file related will be handled via this object
        static readonly FileMethod fileMethod = new FileMethod();

        static void Main(string[] args)
        {
            // create list from input.json, exception stops the whole program
            List<FundInvestment> fundInvestments = fileMethod.CreateFundListFromJson();

            // user input
            string userCmd;

            // has user been used web scraper in this session, affects to behaviour of the report generation
            bool scraperUsed = false;

            // main data 
            FundInvestmentCollection fundCollection = new FundInvestmentCollection(fundInvestments);

            // list for the report generation
            List<RegionalInvestment> regionalInvestments = new List<RegionalInvestment>();

            // just for the sake of demonstration, not really used for anything useful
            fileMethod.ToJsonFile(fundInvestments);




            // main loop
            do
            {
                consoleWriter.WriteMainMenu();  
                userCmd = Console.ReadLine();

                switch (userCmd)
                {
                    case "1": // Program description
                        consoleWriter.WriteProgramDescription();
                        break;

                    case "2": // Scrape data from webpage for every Fund based on Fund url
                        
                        fundCollection.SetFundTopRegionsAndPercentagesFromWeb(httpClient);
                        scraperUsed = true;
                        break;

                    case "3": // Generate report

                        // user haven't triggered scraper, read data from file.
                        if (!scraperUsed)
                        {
                            // set top regions for every Fund from file
                            fileMethod.SetFundTopRegionsFromFile(fundInvestments);

                            // notify user that data is read from a file
                            consoleWriter.WriteInfoUsingInputFile();
                        }
                        else
                        {
                            // notify user that data is read from web
                            consoleWriter.WriteInfoUsingScraperData();
                        }

                        // convert regional percentages to currency
                        regionalInvestments = fundCollection.GetRegionalInvestmentsInCurrency();

                        Report report = new Report("Sijoitusten maantieteellinen jakauma");
                        report.PrintRegionalReport(regionalInvestments);

                        break;

                    case "4": // just playing with interface, merge two different types of Lists to one list with the help of interface

                        // set top regions for every Fund from file, just to make sure that Lists are populated
                        fileMethod.SetFundTopRegionsFromFile(fundInvestments);

                        // convert regional percentages to currency, just to make sure that Lists are populated
                        regionalInvestments = fundCollection.GetRegionalInvestmentsInCurrency();

                        Report report2 = new Report("Sijoitettavat asiat");
                        report2.PrintInvestableThings(fundInvestments, regionalInvestments);

                        break;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }

            } while (userCmd != "q"); // end program
        }
    }
}
