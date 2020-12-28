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
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView()", _cookieUsageAcceptButton);
            _cookieUsageAcceptButton?.Click();
        }

        public CartPage OpenCart()
        {
            WebDriverUtils.SafeFindElementBy(_driver, _openCartDropdownButtonLocator, 2).Click();


            IWebElement dropdownContainer =  _driver.SafeFindElementBy(_openCartDropdownLocator);
            dropdownContainer.FindElement(_openCartButtonInCartDropdownLocator).Click();

            _driver.WaitUntiLoading();

            return new CartPage(_driver);
        }

        public SignInPage OpenSignInPage()
        {
            //check are user signed yet
#if RELEASE
            throw new NotImplementedException();
#endif
            _signInDropdownButton.Click();

            _signInButton.Click();
            _driver.WaitUntiLoading();

            return new SignInPage(_driver);
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
        private static readonly By _openCartButtonInCartDropdownLocator = By.XPath(".//button");

        private IWebElement _signInButton => _signInDropdown.SafeFindFirstDisplayedElementBy(_driver, _signInButtonLocator);
        private static readonly By _signInButtonLocator = By.XPath("//a[text()='Sign In']");
        private IWebElement _signInDropdown => _signInDropdownButton.SafeFindFirstDisplayedElementBy(_driver, _signInDropdownLocator);
        private static readonly By _signInDropdownLocator = By.XPath("//div[contains(@class, 'mh-ma-dropdown')]");

        private IWebElement _signInDropdownButton => _driver.SafeFindElementBy(_signInDropdownButtonLocator);
        private static readonly By _signInDropdownButtonLocator = By.XPath("//div[@class='mh-tw-sign-in-wrap']");
    }
}
