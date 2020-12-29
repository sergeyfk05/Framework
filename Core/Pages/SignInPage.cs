// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using Core.Models;
using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pages
{
    public class SignInPage : AbstractPage
    {
        public SignInPage(IWebDriver driver) : base(driver) { }


        public bool Login(User user)
        {
            if (user.loginOption == LoginOption.Guest)
                throw new ArgumentException();

            EmailInput.SendKeys(user.email);
            PasswordInput.SendKeys(user.password);
            SignInButton.Click();

            _driver.WaitUntiLoading();

            try
            {
                return _driver.SafeFindFirstDisplayedElementBy(_validationIncorrectPasswordWarningLocator, 3) == null;
            }
            catch
            {
                return false;
            }

        }

        protected IWebElement EmailInput => _driver.SafeFindFirstDisplayedElementBy(_emailInputLocator);

        private static readonly IEnumerable<By> _emailInputLocator = new List<By>()
            {
                By.XPath("//input[@name='EmailAddress']")
            };

        protected IWebElement PasswordInput => _driver.SafeFindFirstDisplayedElementBy(_passwordInputLocator);

        private static readonly IEnumerable<By> _passwordInputLocator = new List<By>()
            {
                By.XPath("//input[@name='Password']")
            };

        protected IWebElement SignInButton => _driver.SafeFindFirstDisplayedElementBy(_signInButtonLocator);

        private static readonly IEnumerable<By> _signInButtonLocator = new List<By>()
            {
                By.XPath("//button[@title='Sign In']")
            };
        private static readonly IEnumerable<By> _validationIncorrectPasswordWarningLocator = new List<By>()
            {
                By.XPath("//div[@id='validationSummaryText']")
            };
    }
}
