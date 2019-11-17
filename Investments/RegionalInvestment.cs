using System;

using FundScraperCore.Interfaces;

namespace FundScraperCore.Investments
{
    class RegionalInvestment : IInvestable
    {
        public RegionalInvestment(string region, double owningInCurrency, string code)
        {
            this.InvestmentTarget = region;
            this.OwningInCurrency = owningInCurrency;
            this.Fundcode = code;
        }

        public RegionalInvestment(string region, double owningInCurrency) // overload
        {
            this.InvestmentTarget = region;
            this.OwningInCurrency = owningInCurrency;
        }

        public string InvestmentTarget { get; set; }

        public double OwningInCurrency { get; set; } 

        public string Fundcode { get; }

        public void WriteInfo()
        {
            Console.WriteLine("RegionalInvestment: Just for Interface-demonstration");
        }

    }
}
