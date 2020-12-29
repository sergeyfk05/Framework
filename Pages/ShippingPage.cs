// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using OpenQA.Selenium;
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace Pages
{
    public class ShippingPage : AbstractPage
    {
        public ShippingPage(IWebDriver driver) : base(driver) { }

        public PaymentPage Next(out bool hasValidationErrors)
        {
            _nextButton.Click(); //-V3080
            if (HasValidationErrors)
            {
                hasValidationErrors = true;
                return null;
            }

            hasValidationErrors = false;
            return new PaymentPage(_driver);
        }

        public string FirstName
        {
            get
            {
                return _driver.SafeFindElementBy(_firstNameInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_firstNameInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_firstNameInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _firstNameInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-FirstName']")
            };
        public string MI
        {
            get
            {
                return _driver.SafeFindElementBy(_MIInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_MIInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_MIInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _MIInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-MiddleInitial']")
            };

        public string LastName
        {
            get
            {
                return _driver.SafeFindElementBy(_lastNameInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_lastNameInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_lastNameInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _lastNameInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-LastName']")
            };

        public string StreetAddress
        {
            get
            {
                return _driver.SafeFindElementBy(_streetAddressInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_streetAddressInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_streetAddressInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _streetAddressInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-Line1']")
            };

        public string Apt
        {
            get
            {
                return _driver.SafeFindElementBy(_aptInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_aptInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_aptInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _aptInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-Line2']")
            };

        public int PostalCode
        {
            get
            {
                return Convert.ToInt32(_driver.SafeFindElementBy(_postalCodeInputLocators).GetHiddenText(_driver));
            }
            set
            {
                _driver.SafeFindElementBy(_postalCodeInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_postalCodeInputLocators).SendKeys(value.ToString());
            }
        }
        private static readonly IEnumerable<By> _postalCodeInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-PostalCode']")
            };

        public string City
        {
            get
            {
                return _driver.SafeFindElementBy(_cityInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_cityInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_cityInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _cityInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-City']")
            };

        public string State
        {
            get
            {
                return _driver.SafeFindElementBy(_stateInputLocators).GetHiddenText(_driver);
            }
            set
            {
                new SelectElement(_driver.SafeFindElementBy(_stateInputLocators)).SelectByText(value);
            }
        }
        private static readonly IEnumerable<By> _stateInputLocators = new List<By>()
            {
                By.XPath("//select[@name='DataModel-ShippingContact-State']")
            };

        public string PhoneNumber
        {
            get
            {
                return _driver.SafeFindElementBy(_phoneNumberInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_phoneNumberInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_phoneNumberInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _phoneNumberInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-Phone-PhoneNumber']")
            };

        public string EmailAddress
        {
            get
            {
                return _driver.SafeFindElementBy(_emailInputLocators).GetHiddenText(_driver);
            }
            set
            {
                _driver.SafeFindElementBy(_emailInputLocators).Clear(); //-V3080
                _driver.SafeFindElementBy(_emailInputLocators).SendKeys(value);
            }
        }
        private static readonly IEnumerable<By> _emailInputLocators = new List<By>()
            {
                By.XPath("//input[@name='DataModel-ShippingContact-Email']")
            };

        private IWebElement _nextButton => WebDriverUtils.SafeFindFirstDisplayedElementBy(_driver, _nextButtonLocators);
        private static readonly IEnumerable<By> _nextButtonLocators = new List<By>()
            {
                By.XPath("//button[@id='continueButton']")
            };

        

        private static readonly By _validationErrorsBlockLocators = By.XPath("//*[@id='validationSummary']");

        public bool HasValidationErrors
        {
            get
            {
                try
                {
                    return _driver.FindElement(_validationErrorsBlockLocators).Displayed;
                }
                catch { }
                return false;
            }
        }


    }
}
