// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using AutoMapper;
using Core.Builders;
using Core.Models;
using Core.Pages;
using Core.Pages.PageElements;
using Framework.Models;
using Framework.TestDataProviders;
using OpenQA.Selenium;
using Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Models;
using Xunit;

namespace Framework
{
    public class Tests : CommonConditions
    {

        [Theory]
        [ClassData(typeof(RemoveFromCartTestDataProvider))]
        public void RemoveFromCartTest(IEnumerable<string> links)
        {
            List<ProductInfo> productsInfo = new();

            foreach (var link in links)
            {
                ProductPage product = new ProductPageBuilder(driver).SetProductLink(link).Build();
                product.Open();
                product.AcceptCookies();

                productsInfo.Add((ProductInfo)product);
                product.AddToCart();
            }


            CartPage cart = new CartPage(driver);
            cart.Open();
            List<ProductInCart> cartProducts = cart.Products.ToList();  

            Assert.Equal(links.Count(), cartProducts.Count);

            //remove one product in cart by step
            for (int i = 0; i < links.Count(); i++)
            {
                Assert.Equal(cartProducts.Sum(x => x.Subtotal), cart.Subtotal, 1);
                Assert.Equal(productsInfo[i], (ProductInfo)cart.Products.ToList()[0]);

                cartProducts[0].Remove();
                cartProducts = cart.Products.ToList();
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
            _mapper.Map(data.Info, shippingPage);
            
            bool hasValidationErrors;
            shippingPage.Next(out hasValidationErrors);
            Assert.Equal(data.HasValidationErrors, hasValidationErrors);
        }

        private IMapper _mapper => (new MapperConfiguration(cfg => cfg.CreateMap<ShippingInfo, ShippingPage>())).CreateMapper();

        [Fact]
        public void AuthorizationTest()
        {
            AuthorizationTestDataModel model = new()
            {
                IsValidLoginData = true,
                LoginData = new User()
                {
                    loginOption = LoginOption.Authorization,
                    email = "sergeyfk05@gmail.com",
                    password = "Qwerty123"
                }
            };

            HomePage page = new HomePage(driver);
            page.Open();
            page.AcceptCookies();

            page.OpenSignInPage().Login(model.LoginData);
            page = new HomePage(driver);
            page.Open();

            Assert.Equal(model.IsValidLoginData, page.IsSignedIn);
            //finish test if login data is incorrect
            if (!model.IsValidLoginData)
                return;

            page.LogOut();
            Assert.False(page.IsSignedIn);


        }
    }
}
