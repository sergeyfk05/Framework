// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using OpenQA.Selenium;
using Pages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pages.Utils;

namespace Pages
{
    public class PaymentPage : AbstractPage
    {
        public PaymentPage(IWebDriver driver) : base(driver) { }

        public PaymentSource source
        {
            get
            {
                PaymentSource result = PaymentSource.Undefined;
                Enum.TryParse(_paymentSourcesBlock.FindElement(_paymentSourceLocator).Text, out result); //-V3080
                return result;
            }
            set
            {
                _paymentSourcesBlock.FindElements(_paymentOptionLocators).FirstOrDefault(x => x.Text.Equals(value.ToString()))?.Click();
            }
        }
        //tabbed-payment-types[@ng-if='isTabbedPaymentTypesEnabled']//a[contains(@class, 'selectedTab')]
        private IWebElement _paymentSourcesBlock => _driver.SafeFindElementBy(_paymentSourcesBlockLocators);
        private static readonly By _paymentSourceLocator = By.XPath(".//a[contains(@class, 'selectedTab')]");
        private static readonly IEnumerable<By> _paymentSourcesBlockLocators = new List<By>()
            {
                By.XPath("//tabbed-payment-types[@ng-if='isTabbedPaymentTypesEnabled']")
            };
        private static readonly By _paymentOptionLocators = By.XPath(".//div/div");

    }
}
