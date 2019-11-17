using System.Collections.Generic;
using System.Net.Http;

namespace FundScraperCore.Scrapers
{

    //this class is just for inheritance demonstration, nothing is really used. Working code can be found in Scrapers.RegionScraper
    class SectorScraper : Scraper
    {
        protected override string Xpath { get; set; }

        public SectorScraper(HttpClient client, string url, string fundCode) : base(client, url, fundCode)
        {
            Xpath = "xpath for sector data";
        }
        public override Dictionary<string, double> DoScrape()
        {

            return _investmentPercentages;
        }

        protected override Dictionary<string, double> ParseResponse(string html, string xpath)
        {
            Dictionary<string, double> justDemo = new Dictionary<string, double>();
            return justDemo;
        }
    }
}
