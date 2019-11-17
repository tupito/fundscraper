using System;

namespace FundScraperCore.Utilities
{
    class ConsoleWriteMethod
    {
        public void WriteProgramDescription()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nOhjelma laskee rahastosalkun maantieteellisen hajautuksen valuutassa.\n\n" +
                    "Inputtina toimivat x-määrä rahastoja (..\\FundScraperCore\\bin\\Debug\\netcoreapp2.1\\input.json) ja paljonko kyseiseen rahastoon on sijoitettu valuuttaa.\n" +
                    "Ohjelma lataa ja parsii Morningstar-sivustolta näiden rahastojen maantieteellisen hajautuksen prosentuaaliset tiedot ja\n" +
                    "tuottaa raporttina paljonko missäkin alueessa on valuuttaa sijoitettuna." +
                    "\n\nWebscraper kirjoittaa tiedot myös txt-tiedostoon, ettei seuraavassa sessiossa tarvitse erikseen ladata tietoja webistä. \n" +
                    "\n 2 - scraper - hakee rahastojen maantieteelliset sijoitusosuudet morningstar.fi-sivustolta, päivittää tiedot olioihin ja tiedostoihin" +
                    "\n 3 - show regional report - generoi raportin, mikäli scraperia ei ole käytetty sessiossa, niin luetaan data tiedostosta" +
                    "\n 4 - list investable things - demoa Interfacen käytöstä");
            Console.ResetColor();
        }

        public void WriteMainMenu()
        {
            Console.WriteLine("\n****************************\nCommands: \n" +
                "1 program description\n" +
                "2 scraper, update fund regional information from the web \n" +
                "3 show regional report \n" +
                "4 list investable things (merge two lists using IInvestable) \n" +
                "q quit\n" +
                "****************************");
            Console.Write("\nfundscraper>");
        }

        public void WriteInfoUsingInputFile()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nInfo: Using top region data from saved file, web scraper hasn't been triggered in this session.\n");
            Console.ResetColor();
        }

        public void WriteInfoUsingScraperData()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nInfo: Using scraped top region data\n");
            Console.ResetColor();
        }

        public void WriteInfoFileGenerated(string filepath)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nInfo: Created file {filepath}");
            Console.ResetColor();
        }

        public void WriteInfoFileRead(string filepath)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nInfo: Read file {filepath}");
            Console.ResetColor();
        }
    }
}
