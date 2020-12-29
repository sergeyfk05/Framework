// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace WebDriverFactory
{
    public class WebDriverFactory
    {
        public static IWebDriver Build()
        {
            DesiredCapabilities browser = new DesiredCapabilities();
            browser.SetCapability(CapabilityType.BrowserName, Environment.GetEnvironmentVariable("browser") ?? "chrome");
            browser.SetCapability(CapabilityType.BrowserVersion, "87.0");
            browser.SetCapability("enableVNC", true);
            return new RemoteWebDriver(new Uri("http://bstujenkinsselenoid.ddns.net:4444/wd/hub"), browser);
        }
    }
}
