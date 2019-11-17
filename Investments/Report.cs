using System;
using System.Collections.Generic;

using System.Linq;

namespace FundScraperCore.Investments
{
    class Report
    {

        private string name;

        public Report(string name)
        {
            this.name = name;
        }

        public void PrintRegionalReport(List<RegionalInvestment> regionalInvestments)
        {
            // order RegionalInvestment objects by region
            regionalInvestments.Sort((x, y) => x.InvestmentTarget.CompareTo(y.InvestmentTarget));

            // find unique regions using linq
            IEnumerable<string> regions = regionalInvestments.Select(ri => ri.InvestmentTarget).Distinct();

            // find unique funds using linq
            IEnumerable<string> funds = regionalInvestments.Select(ri => ri.Fundcode).Distinct();

            // generate unique, random color for each fund
            Dictionary<string, ConsoleColor> fundsAndColors = GenerateColors(funds);

            // unique region investments
            List<RegionalInvestment> uniqueRegionalInvestments = new List<RegionalInvestment>();

            // get investments for unique regions (merge investments) 
            foreach (var item in regions)
            {
                // sum of owning in currency for this region using linq
                double regionOwningSum = regionalInvestments.Where(ri => ri.InvestmentTarget == item).Sum(ri => ri.OwningInCurrency);
                // overload RegionalInvestment constructor
                uniqueRegionalInvestments.Add(new RegionalInvestment(item, regionOwningSum)); 
            }

            // order by OwningInCurrency using linq
            uniqueRegionalInvestments = uniqueRegionalInvestments.OrderByDescending(ri => ri.OwningInCurrency).ToList();

            // report table header
            Console.WriteLine("|----------------------------------------------------|");
            Console.WriteLine("|****** Sijoitusten maantieteellinen jakauma ********|");
            Console.WriteLine("|----------------------------------------------------|");
            Console.Write(String.Format("|{0,35}|{1,7}|{2,8}| ", "alue", "e", "%"));

            // report table header, append colored fund codes 
            foreach (var item in funds)
            {
                Console.ForegroundColor = fundsAndColors[item];
                Console.Write("{0} ", item);
                Console.ResetColor();
            }
            Console.Write("\n-----------------------------------------------------");

            double sum = 0;

            // report table datarow
            foreach (var item in uniqueRegionalInvestments)
            {
                // sum of all ownings in currency for average counting, linq
                sum = uniqueRegionalInvestments.Sum(ri => ri.OwningInCurrency);

                // percentage for report table row
                double regionPercentage = item.OwningInCurrency / sum * 100;

                // datarow for report table
                Console.Write(String.Format("\n|{0,35}|{1,7:f2}|{2,7:f2}%| ", item.InvestmentTarget, item.OwningInCurrency, regionPercentage));

                // print colored star "graph" for every region
                foreach (var ri in regionalInvestments)
                {
                    // if Fund has currency in this region, print corresponding amount of coloured stars
                    if (ri.InvestmentTarget == item.InvestmentTarget)
                    {
                        regionPercentage = ri.OwningInCurrency / sum * 100;

                        for (int i = 0; i < (int)regionPercentage; i++)
                        {
                            Console.ForegroundColor = fundsAndColors[ri.Fundcode];
                            Console.Write("*");
                            Console.ResetColor();
                        }
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("|-----------------------------------------------------");
        }

        // generate unique color for each fund for the report
        private static Dictionary<string, ConsoleColor> GenerateColors(IEnumerable<string> funds)
        {
            // key-value (fund-color) -pairs
            Dictionary<string, ConsoleColor> fundsAndColors = new Dictionary<string, ConsoleColor>();

            //random generator
            Random random = new Random();

            foreach (var item in funds)
            {
                // ConsoleColor has a numerical value of 0-16, 0 = black won't be used
                ConsoleColor color = (ConsoleColor)random.Next(1, 16); 
                do
                {
                    color = (ConsoleColor)random.Next(1, 16);
                } 
                while (fundsAndColors.ContainsValue(color)); // unique color for every fund

                fundsAndColors.Add(item, color);
            }

            return fundsAndColors;
        }

        public void PrintInvestableThings(List<FundInvestment> fundInvestments, List<RegionalInvestment> regionalInvestments)
        {

            // Make a joint list for Investable things
            List<Interfaces.IInvestable> investableThings = new List<Interfaces.IInvestable>();

            // add FundInvestments to the joint List
            foreach (FundInvestment fundInvestment in fundInvestments)
            {
                investableThings.Add(fundInvestment);
            }

            // add RegionalInvestments to the joint list
            foreach (RegionalInvestment regionalInvestment in regionalInvestments)
            {
                investableThings.Add(regionalInvestment);
            }

            // for strings
            List<string> investmentTargets = new List<string>();

            // pick just investment targets
            foreach (Interfaces.IInvestable item in investableThings)
            {
                investmentTargets.Add(item.InvestmentTarget);
            }

            // remove duplicates
            List<string> uniqueInvestmentTargets = investmentTargets.Distinct().ToList();

            // print report
            Console.WriteLine("|----------------------------------------------------|");
            Console.WriteLine("|****** Sijoitettavat asiat (input-rahastoilla) *****|");
            Console.WriteLine("|----------------------------------------------------|");
            foreach (var item in uniqueInvestmentTargets)
            {
                Console.WriteLine(item);
            }
        }
    }
}
