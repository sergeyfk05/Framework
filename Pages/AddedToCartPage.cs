using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    public class AddedToCartPage : AbstractPage
    {
        public AddedToCartPage(IWebDriver driver) : base(driver) { }

        public override void Open()
        {
            throw new InvalidOperationException();
        }

        public double Subtotal => Convert.ToDouble(_driver.SafeFindElementBy(_subtotalTextLocators).Text.Replace("$", ""),
                WebDriverUtils.CostToDoubleConverterProvider);

        private static readonly IEnumerable<By> _subtotalTextLocators = new List<By>()
            {
                By.XPath("//*[@data-price]")
            };
    }
}
