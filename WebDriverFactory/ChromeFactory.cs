using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDriverFactory
{
    public class ChromeFactory : IBrowserFactory
    {
        public IWebDriver Build()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("no-sandbox");
            //options.AddArguments("headless");
            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(150));
        }

    }
}
