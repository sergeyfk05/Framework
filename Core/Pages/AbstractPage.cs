// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pages
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
            dropdownContainer.FindElement(_openCartButtonInCartDropdownLocator).Click();

            _driver.WaitUntiLoading();

            return new CartPage(_driver);
        }

        public SignInPage OpenSignInPage()
        {
            if (IsSignedIn)
                throw new Exception("User is already signed in");

            _signInDropdownButton?.Click();

            _signInButton?.Click();
            _driver.WaitUntiLoading();

            return new SignInPage(_driver);
        }

        public bool LogOut()
        {
            if (!IsSignedIn)
                return true;

            _signInDropdownButton?.Click();

            _signOutButton.Click();
            _driver.WaitUntiLoading();
            return !IsSignedIn;
        }


        public virtual void Open()
        {
            throw new NotImplementedException();
        }


        public bool IsSignedIn => _signInOrUserLabel.GetHiddenText(_driver).Trim() != "Sign In";

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

        private IWebElement _signOutButton => _signInDropdown.SafeFindFirstDisplayedElementBy(_driver, _signOutButtonLocator);
        private static readonly By _signOutButtonLocator = By.XPath("//a[text()='Sign Out']");
        private IWebElement _signInDropdown => _signInDropdownButton.SafeFindFirstDisplayedElementBy(_driver, _signInDropdownLocator);
        private static readonly By _signInDropdownLocator = By.XPath("//div[contains(@class, 'mh-ma-dropdown')]");

        private IWebElement _signInDropdownButton => _driver.SafeFindElementBy(_signInDropdownButtonLocator);
        private static readonly By _signInDropdownButtonLocator = By.XPath("//div[@class='mh-tw-sign-in-wrap']");

        private IWebElement _signInOrUserLabel => _signInDropdownButton.SafeFindFirstDisplayedElementBy(_driver, _signInOrUserLabelLocator);
        private static readonly By _signInOrUserLabelLocator = By.XPath("//span[@mh-sign-in-label='Sign In']");

    }
}
