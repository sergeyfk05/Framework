// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace WebDriverFactory
{
    public class WebDriverFactory
    {
        [Obsolete]
        public static IWebDriver Build()
        {
            if(Environment.GetEnvironmentVariable("selenoidurl") == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("no-sandbox");
                //options.AddArguments("headless");
                return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(150));
            }
            else
            {
                DesiredCapabilities browser = new DesiredCapabilities();
                browser.SetCapability(CapabilityType.BrowserName, Environment.GetEnvironmentVariable("browser") ?? "chrome");
                browser.SetCapability(CapabilityType.BrowserVersion, Environment.GetEnvironmentVariable("browserversion") ?? "");
                Enum.TryParse(Environment.GetEnvironmentVariable("platform") ?? "Linux", out PlatformType platform);
                browser.SetCapability(CapabilityType.Platform, PlatformType.Linux);
                browser.SetCapability("enableVNC", true);
                var driver = new RemoteWebDriver(new Uri(Environment.GetEnvironmentVariable("selenoidurl")), browser);
                driver.Manage().Window.Maximize();
                return driver;
            }
        }
    }
}
