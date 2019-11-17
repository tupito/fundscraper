using System.Collections.Generic;

using System.Net.Http;
using FundScraperCore.Scrapers;

namespace FundScraperCore.Investments
{
    class FundInvestmentCollection
    {
        private List<FundInvestment> funds;

        public FundInvestmentCollection(List<FundInvestment> funds)
        {
            this.funds = funds;
        }

        // Get top 5 regions and percentage collection (Dictionary) for every Fund from the website and set it to Fund.TopRegionsAndPercentages
        public void SetFundTopRegionsAndPercentagesFromWeb(HttpClient httpClient)
        {
            foreach (FundInvestment fund in this.funds)
            {
                RegionScraper regionScraper = new RegionScraper(httpClient, fund.Url, fund.InvestmentTarget);
                fund.TopRegionsAndPercentages = regionScraper.DoScrape();  // Dictionary<string,double>
            }
        }

        // Convert regions/percentages to regions/currency/fundcode collection
        public List<RegionalInvestment> GetRegionalInvestmentsInCurrency()
        {
            List<RegionalInvestment> regInvestments = new List<RegionalInvestment>();

            foreach (FundInvestment fund in this.funds)
            {
                foreach (KeyValuePair<string, double> entry in fund.TopRegionsAndPercentages)
                {
                    double percentage = entry.Value; // region percentage of a Fund (eg. Yhdysvallat 99,03%)
                    double regionalOwningInCurrency = fund.OwningInCurrency * (percentage / 100); // how much money has been invested to this region

                    //in top 5 regions might be regions with 0% values
                    if (regionalOwningInCurrency > 0)
                    {
                        RegionalInvestment region = new RegionalInvestment(entry.Key, regionalOwningInCurrency, fund.InvestmentTarget);
                        regInvestments.Add(region);
                    }
                }
            }
            return regInvestments;  //actual investments in currency
        }
    }
}
