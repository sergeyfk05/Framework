using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace WebDriverFactory
{
    public class WebDriverFactory
    {
        public static WebDriverFactory Instance => _webDriverFactory ?? (_webDriverFactory = new WebDriverFactory());
        private static WebDriverFactory _webDriverFactory;

        private static readonly IDictionary<string, IBrowserFactory> _factories = new Dictionary<string, IBrowserFactory>()
        {
            {"chrome", new ChromeFactory() }
        };


        public IWebDriver Build(string browser = "chrome")
        {
            IBrowserFactory browserFactory;
            _factories.TryGetValue(browser ?? "", out browserFactory);
            return (browserFactory ?? _factories["chrome"]).Build();
        }
    }
}
