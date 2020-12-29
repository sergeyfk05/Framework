// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using Core.Pages.PageElements;
using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pages
{
    public class CartPage : AbstractPage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        public override void Open()
        {
            _driver.Navigate().GoToUrl("https://www.dell.com/en-us/buy");
        }

        public double Subtotal => Convert.ToDouble(_driver.SafeFindElementBy(_subtotalLocators).Text.Replace("$", ""), //-V3080
                WebDriverUtils.CostToDoubleConverterProvider);

        private IWebElement _checkoutButton => WebDriverUtils.SafeFindFirstDisplayedElementBy(_driver, _checkoutButtonLocators);
        public CheckoutPage Checkout()
        {
            _checkoutButton?.Click();
            _driver.WaitUntiLoading();
            return new CheckoutPage(_driver);
        }

        public IEnumerable<ProductInCart> Products
        {
            get
            {
                List<ProductInCart> products = new();

                IEnumerable<IWebElement> productContainers = _driver.FindElements(_productContainerLocators);

                foreach (var el in productContainers)
                {
                    products.Add(new ProductInCart(el, _driver));
                }

                return products;
            }
        }

        private static readonly IEnumerable<By> _checkoutButtonLocators = new List<By>()
            {
                By.XPath("//*[@data-testid='continueCheckoutButton']")
            };

        private static readonly IEnumerable<By> _subtotalLocators = new List<By>()
            {
                By.XPath("//*[@data-testid='itemssubtotalItemValue']"),
                By.XPath("//*[@data-testid='cartSubtotal']")
            };

        private static readonly By _productContainerLocators = By.XPath("//div[starts-with(@ng-repeat, 'item in DataModel.CartItems')]");

    }
}
