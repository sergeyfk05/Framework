// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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

        protected IWebDriver driver => _driver ?? (_driver = WebDriverFactory.WebDriverFactory.Build());
        private IWebDriver _driver;


        public void Dispose()
        {
            _driver?.Quit();
        }
    }
}
