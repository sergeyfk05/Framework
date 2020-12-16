﻿using OpenQA.Selenium;
using Pages.Models;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    public class CartPage : AbstractPage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        public override void Open()
        {
            _driver.Navigate().GoToUrl("https://www.dell.com/en-us/buy");
        }

        public double Subtotal => Convert.ToDouble(_driver.SafeFindElementBy(_subtotalLocators).Text.Replace("$", ""),
                WebDriverUtils.CostToDoubleConverterProvider);

        public IEnumerable<ProductInCart> Products
        {
            get
            {
                List<ProductInCart> products = new();

                IEnumerable<IWebElement> productContainers = _driver.FindElements(_productContainerLocators);

                foreach (var el in productContainers)
                {
                    products.Add(new ProductInCart(el));
                }

                return products;
            }
        }

        private static readonly IEnumerable<By> _subtotalLocators = new List<By>()
            {
                By.XPath("//*[@data-testid='itemssubtotalItemValue']"),
                By.XPath("//*[@data-testid='cartSubtotal']")
            };

        private static readonly By _productContainerLocators = By.XPath("//div[starts-with(@ng-repeat, 'item in DataModel.CartItems')]");

    }
}