using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace Pages.Models
{
    public class ProductInCart
    {
        IWebElement _container;
        IWebDriver _driver;
        public ProductInCart(IWebElement container, IWebDriver driver)
        {
            _container = container;
            _driver = driver;
        }
        public string Title => _container.FindElement(_productTitleLocator).Text;
        public int Count => Convert.ToInt32(_container.FindElement(_productCountLocator).Text);
        public double Subtotal => Convert.ToDouble(_container.FindElement(_productSubtotalLocator).Text.Replace("$", ""),
            WebDriverUtils.CostToDoubleConverterProvider);

        private IWebElement _removeButton => _container.FindElements(_productRemoveButtonLocator).First(x => x.Displayed);

        public void Remove()
        {
            _removeButton.Click();
            _driver.SafeFindElementBy(_loadingIndicatorLocator);
        }

        public override bool Equals(object obj)
        {
            return obj is ProductInCart product &&
                   Title == product.Title &&
                   Count == product.Count &&
                   Subtotal == product.Subtotal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Count, Subtotal);
        }


        private static readonly By _productSubtotalLocator = By.XPath(".//*[@data-testid='itemTotalAmount']");

        private static readonly By _productCountLocator = By.XPath(".//select[@name='repeatSelect']/option[@selected='selected']");

        private static readonly By _productTitleLocator = By.XPath(".//*[@data-testid='itemTitle']");

        private static readonly By _productRemoveButtonLocator = By.XPath(".//*[@data-testid='cartRemoveItemAction']");
        private static readonly By _loadingIndicatorLocator = By.XPath("//div[@id='loaderDivImage' and contains(@style, 'display: none;')]");
    }
}
