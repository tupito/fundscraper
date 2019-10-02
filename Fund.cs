using System;
using System.Collections.Generic;
using System.Text;

namespace FundScraperFramework
{
    class Fund
    {
        private string _code;
        private string _name;
        private string _url;
        private Scraper _scraper;

        public Fund(string code, string name, string url)
        {
            this._code = code;
            this._name = name;
            this._url = url;
        }
        public string Url
        {
            get { return this._url; }
        }

        public Scraper Scraper
        {
            set { this._scraper = value; }
        }
    }
}
