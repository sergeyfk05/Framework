using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using WebDriverFactory;
using Selenoid.Client;
using OpenQA.Selenium.Remote;

namespace Framework
{
    public class CommonConditions : IDisposable
    {

        protected IWebDriver driver => _driver ?? WebDriverFactory.WebDriverFactory.Build();
        private IWebDriver _driver;


        public void Dispose()
        {
            _driver?.Quit();
        }
    }
}
