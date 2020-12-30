// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using Core.Pages.PageElements;
using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CartPage ClearCart()
        {
            int cartCount = Products.Count();
            for (; cartCount > 0; cartCount--)
                Products.ToList()[0].Remove();

            return this;
        }

        public CartPage EnterCoupon(string coupon, out bool status)
        {
            _couponInput.SendKeys(coupon);
            _couponApplyButton.Click();


            _driver.WaitUntilElementShowed(_couponLoaderLocators);

            status = true;

            try
            {
                _couponReturnMessage.GetHiddenText(_driver);
                status = false;
            }
            catch
            {
            }

            return this;

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
        

        private IWebElement _couponLoader => _driver.SafeFindFirstDisplayedElementBy(_couponLoaderLocators);
        private static readonly By _couponLoaderLocators = By.XPath("//div[@id='loaderDivImage']");

        private IWebElement _couponReturnMessage => _driver.SafeFindFirstDisplayedElementBy(_couponReturnMessageLocators, 5);
        private static readonly IEnumerable<By> _couponReturnMessageLocators = new List<By>()
            {
                By.XPath("//p[@data-testid='coupon-return-message-paragraph']")
            };

        private IWebElement _couponInput => _driver.SafeFindFirstDisplayedElementBy(_couponInputLocators);
        private static readonly IEnumerable<By> _couponInputLocators = new List<By>()
            {
                By.XPath("//input[@id='appendedCouponInputButton']")
            };
        private IWebElement _couponApplyButton => _driver.SafeFindFirstDisplayedElementBy(_couponApplyButtonLocators);
        private static readonly IEnumerable<By> _couponApplyButtonLocators = new List<By>()
            {
                By.XPath("//button[@id='applyCouponButton']")
            };

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
