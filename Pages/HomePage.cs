using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages
{
    public class HomePage : AbstractPage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        public override void Open()
        {
            _driver.Navigate().GoToUrl("https://www.dell.com/en-us");
        }
    }
}
