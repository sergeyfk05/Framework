using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDriverFactory
{
    public interface IBrowserFactory
    {
        IWebDriver Build();
    }
}
