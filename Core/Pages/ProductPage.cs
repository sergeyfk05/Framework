// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using Core.Models;
using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pages
{
    public class ProductPage : AbstractPage
    {
        public ProductPage(IWebDriver driver) : base(driver) { }

        public string Link { get; internal set; }

        private IWebElement _addToCartButton => WebDriverUtils.SafeFindElementBy(_driver, _addToCartButtonLocators);

        public AddedToCartPage AddToCart()
        {
            _addToCartButton?.Click();
            _driver.WaitUntiLoading();
            _driver.SafeFindElementBy(_elementConfirmingAddingToCartLocators);
            return new AddedToCartPage(_driver);
        }



        public double Price => Convert.ToDouble(_driver.SafeFindElementBy(_priceTextLocators).Text.Replace("$", ""), //-V3080
                WebDriverUtils.CostToDoubleConverterProvider);

        public string Title => _driver.SafeFindElementBy(_productTitleLocators).GetHiddenText(_driver).Trim();

        public override void Open()
        {
            _driver.Navigate().GoToUrl(Link);
        }

        private static readonly IEnumerable<By> _addToCartButtonLocators = new List<By>()
            {
                By.XPath("//*[@data-testid='addToCartButton']")
            };
        private static readonly IEnumerable<By> _priceTextLocators = new List<By>()
            {
                By.XPath("//div[@id='cf-strike-through-price']/div/div[@data-price]"),
                By.XPath("//*[@data-testid='sharedPSPDellPrice']")
            };
        private static readonly IEnumerable<By> _productTitleLocators = new List<By>()
            {
                By.XPath("//h1[@class='cf-pg-title']"),
                By.XPath("//*[@id='page-title']/div/div/h1/span")
            };
        private static readonly IEnumerable<By> _elementConfirmingAddingToCartLocators = new List<By>()
            {
                By.XPath("//h3[@id='cf-subtotal-label']"),
                By.XPath("//*[@data-soldout='false']")
            };

        public static explicit operator ProductInfo(ProductPage product)
        {
            return new ProductInfo()
            {
                Price = product.Price,
                Title = product.Title
            };
        }
    }
}
