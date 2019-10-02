using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FundScraperFramework
{
    class Program
    {

        // HttpClient is intended to be instantiated once per application, rather than per-use.
        static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Fund f1 = new Fund("SXR8", "iShares Core S&P 500 UCITS ETF USD (Acc) (EUR)",
                "http://www.morningstar.fi/fi/etf/snapshot/snapshot.aspx?id=0P0000OO21");
            Fund f2 = new Fund("DXET", "Xtrackers Euro Stoxx 50 UCITS ETF 1C",
                "http://www.morningstar.fi/fi/etf/snapshot/snapshot.aspx?id=0P0000HNXD");

            List<Fund> dummies = new List<Fund>();
            dummies.Add(f1);
            dummies.Add(f2);

            foreach (Fund fund in dummies)
            {
                Scraper scraper = new Scraper(client, fund.Url);
                scraper.DoScrape();
                fund.Scraper = scraper;
            }

            Console.ReadLine();
        }
    }
}
