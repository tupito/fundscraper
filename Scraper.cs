using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FundScraperFramework
{
    class Scraper
    {
        private readonly string _url;
        private readonly HttpClient _client;
        private Dictionary<string, double> _scraperResult;

        public Scraper(HttpClient client, string url)
        {
            this._client = client;
            this._url = url;
        }


        public void DoScrape()
        {
            //start an Action 
            Task t = Task.Run(() =>
            {
                GetPage(this._client, this._url);  //The work to execute asynchronously
            });

            Console.WriteLine("Downloading...");
        }
        private async void GetPage(HttpClient client, string url)
        {
            try
            {
                string responseBody = await client.GetStringAsync(url);
                Console.WriteLine("\nGot a response from {0}: {1}  ...", url, responseBody.Substring(0, 50));
                this._scraperResult = ParseResponse(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private Dictionary<string, double> ParseResponse(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var xpath = "//*[@id='overviewPortfolioTopRegionsDiv']";
            var node = doc.DocumentNode.SelectSingleNode(xpath);

            //collection for the parse result, eg. {[Euroalue, 94.22], [Yhdistynyt kuningaskunta, 0.54], ...}
            Dictionary<string, double> rowResult = new Dictionary<string, double>();

            //loop table rows
            foreach (var n in node.Descendants("tr"))
            {
                //jump over table header text
                if (n.InnerText == "5 suurinta aluetta%") continue;

                Console.WriteLine("{0} {1}%", n.FirstChild.InnerHtml, n.LastChild.InnerHtml);

                //region information in the first child <td>
                string region = n.FirstChild.InnerHtml;

                //region percentage in the last child <td>
                double percentage;
                if (!Double.TryParse(n.LastChild.InnerHtml, out percentage))
                {
                    Console.WriteLine("Cannot parse {0}", n.LastChild.InnerHtml);
                }

                rowResult.Add(region, percentage);
            }

            return rowResult;
        }
    }
}
