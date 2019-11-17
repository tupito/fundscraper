using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using FundScraperCore.Utilities;

namespace FundScraperCore.Scrapers
{
    class RegionScraper : Scraper
    {

        protected override string Xpath { get; set; }

        public RegionScraper(HttpClient client, string url, string fundCode) : base(client,url,fundCode)
        {
            Xpath = "//*[@id='overviewPortfolioTopRegionsDiv']";
        }

        //returns _regionsAndPercentages, also saves the same information to the file
        public override Dictionary<string, double> DoScrape()
        {
            //start an Action 
            Task t = Task.Run(async () =>
            {
                try
                {
                    // asyncronous work which gets the raw html from url
                    string response = await _httpClient.GetStringAsync(_url);

                    // info for user
                    Console.WriteLine("\nInfo: Got a response from {0}", _url);

                    // parser result
                    _investmentPercentages = ParseResponse(response, Xpath); // Dictionary, scraper return result

                    // save to file
                    FileMethod filemethod = new FileMethod();
                    filemethod.DictionaryToTxtFile(_investmentPercentages, this._fundCode);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }).ContinueWith(t2 => Console.WriteLine("Info: Task for {0} is ready", this._fundCode));

            Console.WriteLine("\nInfo: Start downloading content from {0} to {1}, task id: {2} \n\nPlease wait...", _url, _fundCode, t.Id);

            t.Wait();

            return _investmentPercentages;
        }

        protected override Dictionary<string, double> ParseResponse(string html, string xpath)
        {
            HtmlDocument doc = new HtmlDocument(); //HtmlAgilityPack
            doc.LoadHtml(html);

            var node = doc.DocumentNode.SelectSingleNode(xpath);

            //Scraper return result: Collection of regions and stock weight percentage, eg. {[Euroalue, 94.22], [Yhdistynyt kuningaskunta, 0.54], ...}
            Dictionary<string, double> rowResults = new Dictionary<string, double>();

            //loop html table rows
            foreach (var n in node.Descendants("tr"))
            {
                //table header text is not wanted
                if (n.InnerText == "5 suurinta aluetta%") continue;

                //show user whats happening
                Console.WriteLine("Info: Parsed: {0} {1}% {2}", n.FirstChild.InnerHtml, n.LastChild.InnerHtml, _fundCode);

                //region information in the first child <td>
                string region = n.FirstChild.InnerHtml;

                //region percentage in the last child <td>
                if (!Double.TryParse(n.LastChild.InnerHtml, out double percentage))
                {
                    Console.WriteLine("Cannot parse {0}", n.LastChild.InnerHtml);
                }
                rowResults.Add(region, percentage);
            }
            return rowResults;
        }
    }
}
