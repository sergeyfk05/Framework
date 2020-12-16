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

        public CartPage OpenCart()
        {
            WebDriverUtils.SafeFindElementBy(_driver, _openCartDropdownButtonLocator, 2).Click();


            IWebElement dropdownContainer =  _driver.SafeFindElementBy(_openCartDropdownLocator);
            dropdownContainer.FindElement(_addToCartButtonInCartDropdownLocator).Click();

            _driver.WaitUntiLoading();

            return new CartPage(_driver);
        }

        public virtual void Open()
        {
            throw new NotImplementedException();
        }

        private static readonly IEnumerable<By> _cookieUsageAcceptButtonLocators = new List<By>()
            {
                By.XPath("//button[@id='_evidon-accept-button']")
            };

        private static readonly IEnumerable<By> _openCartSectionLocator = new List<By>()
            {
                By.XPath("//div[@id='mh-cart']")
            };

        private static readonly By _openCartDropdownButtonLocator = By.XPath("//div[@id='mh-cart']//a");
        private static readonly By _openCartDropdownLocator = By.XPath("//div[@class='mh-ct-dropdown']");
        private static readonly By _addToCartButtonInCartDropdownLocator = By.XPath(".//button");
    }
}
