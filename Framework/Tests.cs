using Framework.Models;
using Framework.TestDataProviders;
using OpenQA.Selenium;
using Pages;
using Pages.Builders;
using Pages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Framework
{
    public class Tests : CommonConditions
    {
        [Theory]
        [ClassData(typeof(AddingToCartTestDataProvider))]
        public void AddingToCartTest(string link)
        {
            ProductPage product = new ProductPageBuilder(driver).SetProductLink(link).Build();
            product.Open();
            product.AcceptCookies();

            double productPagePrice = product.Price;
            string productPageTitle = product.Title;

            CartPage cart = product.AddToCart().OpenCart();
            List<ProductInCart> cartProducts = cart.Products.ToList();

            Assert.Single(cartProducts);
            Assert.Equal(1, cartProducts[0].Count); //products count should be equal "1" because the test adding one product
            Assert.Equal(productPageTitle, cartProducts[0].Title);
            Assert.Equal(productPagePrice, cartProducts[0].Subtotal);
        }

        [Theory]
        [ClassData(typeof(RemoveFromCartTestDataProvider))]
        public void RemoveFromCartTest(IEnumerable<string> links)
        {
            foreach (var link in links)
            {
                ProductPage product = new ProductPageBuilder(driver).SetProductLink(link).Build();
                product.Open();
                product.AcceptCookies();

                product.AddToCart();
            }


            CartPage cart = new CartPage(driver);
            cart.Open();
            List<ProductInCart> cartProducts = cart.Products.ToList();  

            Assert.Equal(links.Count(), cartProducts.Count);

            for (int i = links.Count(); i > 0; i--)
            {
                Assert.Equal(cartProducts.Sum(x => x.Subtotal), cart.Subtotal, 1);

                cartProducts[0].Remove();
                cartProducts = cart.Products.ToList();
                //Assert.Equal(i - 1, cartProducts.Count);
            }

            cartProducts = cart.Products.ToList();
            Assert.Empty(cartProducts);
        }

        [Theory]
        [ClassData(typeof(ShippingInfoValidationTestDataProvider))]
        public void ShippingInfoValidationTest(ShippingInfoValidationTestData data)
        {
            ProductPage product = new ProductPageBuilder(driver).SetProductLink(data.Link).Build();
            product.Open();
            product.AcceptCookies();

            ShippingPage shippingPage = product.AddToCart().OpenCart().Checkout().Login(data.UserInfo ?? new User() { loginOption = LoginOption.Guest });
            shippingPage.FirstName = data.Info.FirstName;
            shippingPage.LastName = data.Info.LastName;
            shippingPage.MI = data.Info.MI;
            shippingPage.StreetAddress = data.Info.StreetAddress;
            shippingPage.Apt = data.Info.Apt;
            shippingPage.PostalCode = data.Info.PostalCode;
            shippingPage.City = data.Info.City;
            shippingPage.State = data.Info.State;
            shippingPage.PhoneNumber = data.Info.PhoneNumber;
            shippingPage.EmailAddress = data.Info.EmailAddress;
            
            bool hasValidationErrors;
            shippingPage.Next(out hasValidationErrors);
            Assert.Equal(data.HasValidationErrors, hasValidationErrors);
        }

        [Fact]
        public void AuthorizationTest()
        {
            HomePage page = new HomePage(driver);
            page.Open();
            page.AcceptCookies();

            page.OpenSignInPage();
        }
    }
}
