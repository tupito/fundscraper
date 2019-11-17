using System.Collections.Generic;
using System.Net.Http;

namespace FundScraperCore.Scrapers
{
    //parses given url and returns a Dictionary eg. {[Euroalue, 94.22], [Yhdistynyt kuningaskunta, 0.54], ...}
    abstract class Scraper
    {
        protected HttpClient _httpClient;
        protected string _url;
        protected string _fundCode;
        protected virtual string Xpath { get; set; } // selector for parsing regional information from the web page, will be defined in inherited Subclasses
        protected Dictionary<string, double> _investmentPercentages; //scraper result, eg. {[Euroalue, 94.22], [Yhdistynyt kuningaskunta, 0.54], ...}
        
        public Scraper(HttpClient httpClient, string url, string fundCode)
        {
            this._httpClient = httpClient;
            this._url = url;
            this._fundCode = fundCode;
        }

        //returns _regionsAndPercentages, also saves the same information to the file
        public abstract Dictionary<string, double> DoScrape();

        //workhorce for parsing html
        protected abstract Dictionary<string, double> ParseResponse(string html, string xpath);
    }
}
