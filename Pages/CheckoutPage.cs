// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using OpenQA.Selenium;
using Pages.Models;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages
{
    public class CheckoutPage : AbstractPage
    {
        public CheckoutPage(IWebDriver driver) : base(driver) { }


        public ShippingPage Login(User user)
        {
            if(user.loginOption == LoginOption.Authorization)
            {
                _emailLoginInput.SendKeys(user.email); //-V3080
                _passwordLoginInput.SendKeys(user.password); //-V3080
                _loginButton.Click(); //-V3080
            }
            else if(user.loginOption == LoginOption.Guest)
            {
                _loginAsGuestButton.Click(); //-V3080
            }

            _driver.WaitUntiLoading();
            return new ShippingPage(_driver);
        }

        private IWebElement _emailLoginInput => WebDriverUtils.SafeFindElementBy(_driver, _emailLoginInputLocators);

        private IWebElement _passwordLoginInput => WebDriverUtils.SafeFindElementBy(_driver, _passwordLoginInputLocators);
        private IWebElement _loginAsGuestButton => WebDriverUtils.SafeFindElementBy(_driver, _loginAsGuestButtonLocators);
        private IWebElement _loginButton => WebDriverUtils.SafeFindElementBy(_driver, _loginButtonLocators);

        private static readonly IEnumerable<By> _emailLoginInputLocators = new List<By>()
            {
                By.XPath("//input[@name='EmailAddress']")
            };

        private static readonly IEnumerable<By> _passwordLoginInputLocators = new List<By>()
            {
                By.XPath("//input[@name='Password']")
            };
        private static readonly IEnumerable<By> _loginAsGuestButtonLocators = new List<By>()
            {
                By.XPath("//button[@id='btn-guescheckout']")
            };
        private static readonly IEnumerable<By> _loginButtonLocators = new List<By>()
            {
                By.XPath("//button[@id='sign-in-button']")
            };
    }
}
