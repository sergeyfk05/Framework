using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pages.Utils
{
    public static class WebDriverUtils
    {

        public static IWebElement SafeFindElementBy(this IWebDriver driver, By by, double timeout = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(d => d.FindElement(by));
        }
        public static IWebElement SafeFindElementBy(this IWebDriver driver, IEnumerable<By> byCollection, double timeout = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            try
            {
                return wait.Until(d => d.FindFirstExistingElementFromCollection(byCollection));
            }
            catch { return null; }
        }

        private static IWebElement FindFirstExistingElementFromCollection(this IWebDriver driver, IEnumerable<By> byCollection)
        {
            foreach (var by in byCollection)
            {
                try { return driver.FindElement(by); } catch { }
            }

            throw new NoSuchElementException();
        }

        public static string GetHiddenText(this IWebElement element, IWebDriver driver)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].innerText", element).ToString().Trim();
        }

        public static bool IsLoad(this IWebDriver driver)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Trim() == "complete";
        }

        public static IWebElement GetLoadedBodyOrDefault(this IWebDriver driver)
        {
            return (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("if(document.readyState=='complete') return document.body;");
        }

        public static IWebElement WaitUntiLoading(this IWebDriver driver, double timeout = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(d => d.GetLoadedBodyOrDefault());
        }

        public static readonly NumberFormatInfo CostToDoubleConverterProvider = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
            NumberGroupSeparator = ",",
            NumberGroupSizes = new int[] { 3 }
        };
    }
}
