using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using WebDriverFactory;

namespace Framework
{
    public class CommonConditions : IDisposable
    {
        protected IWebDriver driver => _driver ?? (_driver = WebDriverFactory.WebDriverFactory.Instance.Build(Environment.GetEnvironmentVariable("testenv1")));
        private IWebDriver _driver;


        public void Dispose()
        {
            _driver?.Quit();
        }
    }
}
