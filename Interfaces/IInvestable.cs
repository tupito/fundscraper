
namespace FundScraperCore.Interfaces
{
    interface IInvestable
    {
        double OwningInCurrency { get; set; }
        string InvestmentTarget { get; set; }
        void WriteInfo(); //just for demonstration
    }
}
