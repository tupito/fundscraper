using System;
using System.Collections.Generic;

using FundScraperCore.Interfaces;
using FundScraperCore.Scrapers;

namespace FundScraperCore.Investments
{
    // SUOM.HUOM. Fund = Rahasto, joka sijoittaa useaan indeksin mukaiseen yritykseen. Esim. S&P 500 sijoittaa Yhdysvaltojen 500 suurimpaan yritykseen.
    class FundInvestment : IInvestable
    {
        private string _name; // Full name of the Fund, eg. "Nordnet Superrahasto Suomi"
        private Scraper _scraper;

        public FundInvestment(string code, string name, string url, int owningInCurrency)
        {
            this.InvestmentTarget = code;
            this._name = name;
            this.Url = url;
            this.OwningInCurrency = owningInCurrency;
        }

        public double OwningInCurrency { get; set; }
        public string InvestmentTarget { get; set; }
        public string Url { get; }
        public Scraper Scraper
        {
            set { this._scraper = value; }
        }
        public Dictionary<string, double> TopRegionsAndPercentages { set; get; }

        public void WriteInfo() {
            Console.WriteLine("FundIvestment: Just for Interface-demonstration");
        }
    }
}
