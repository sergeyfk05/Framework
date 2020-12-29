// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using Core.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Builders
{
    public class ProductPageBuilder
    {
        private IWebDriver _driver;
        private ProductPage _product;
        public ProductPageBuilder(IWebDriver driver)
        {
            _driver = driver;
            _product = new ProductPage(driver);
        }

        public ProductPageBuilder SetProductLink(string link)
        {
            _product.Link = link;
            return this;
        }

        public ProductPage Build()
        {
            return _product;
        }
    }
}
