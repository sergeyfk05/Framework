// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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

        public double Subtotal => Convert.ToDouble(_driver.SafeFindElementBy(_subtotalTextLocators).Text.Replace("$", ""), //-V3080
                WebDriverUtils.CostToDoubleConverterProvider);

        private static readonly IEnumerable<By> _subtotalTextLocators = new List<By>()
            {
                By.XPath("//*[@data-price]")
            };
    }
}
