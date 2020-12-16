using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    public abstract class AbstractPage
    {
        protected IWebDriver _driver;
        public AbstractPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement _cookieUsageAcceptButton => WebDriverUtils.SafeFindElementBy(_driver, _cookieUsageAcceptButtonLocators, 3);

        public void AcceptCookies()
        {
            _cookieUsageAcceptButton?.Click();
        }

        public virtual void Open()
        {
            throw new NotImplementedException();
        }

        private static readonly IEnumerable<By> _cookieUsageAcceptButtonLocators = new List<By>()
            {
                By.XPath("//button[@id='_evidon-accept-button']")
            };
    }
}
