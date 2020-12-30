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
using Pages.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Models;
using Tests.TestDataProviders;
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

        [Theory]
        [ClassData(typeof(AuthorizationTestDataProvider))]
        public void AuthorizationTest(AuthorizationTestDataModel model)
        {
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

        [Theory]
        [ClassData(typeof(SaveCartFromGuestUserTestDataProvider))]
        public void SaveCartFromGuestUserTest(SaveCartFromGuestUserTestDataModel model)
        {       
            //prepare - login, clear cart and logout
            HomePage page = new HomePage(driver);
            page.Open();
            page.AcceptCookies();
            page.OpenSignInPage().Login(model.LoginData);
            CartPage cartPage = new CartPage(driver);
            cartPage.Open();
            cartPage.ClearCart().LogOut();

            //add products from guest
            foreach (var link in model.Links)
            {
                ProductPage product = new ProductPageBuilder(driver).SetProductLink(link).Build();
                product.Open();
                product.AcceptCookies();

                product.AddToCart();
            }

            //save products in guest cart
            page = new HomePage(driver);
            page.Open();
            cartPage = new CartPage(driver);
            cartPage.Open();
            List<ProductInfo> guestCart = cartPage.Products.Select(x => (ProductInfo)x).ToList();

            //login and save product in user cart
            cartPage.OpenSignInPage().Login(model.LoginData);
            cartPage = new CartPage(driver);
            cartPage.Open();
            List<ProductInfo> userCart = cartPage.Products.Select(x => (ProductInfo)x).ToList();


            Assert.True(Enumerable.SequenceEqual(guestCart, userCart));

            cartPage.ClearCart();

        }

        [Theory]
        [ClassData(typeof(CouponTestDataProvider))]
        public void CouponTest(CouponTestDataModel model)
        {

            foreach (var link in model.Links)
            {
                ProductPage product = new ProductPageBuilder(driver).SetProductLink(link).Build();
                product.Open();
                product.AcceptCookies();

                product.AddToCart();
            }

            CartPage cartPage = new CartPage(driver);
            cartPage.Open();

            bool isCouponValid;
            cartPage.EnterCoupon(model.Coupon, out isCouponValid);

            Assert.Equal(model.IsCouponValid, isCouponValid);
        }

        [Fact]
        public void ChatTest()
        {
            HomePage page = new HomePage(driver);
            page.Open();
            page.AcceptCookies();

            page.OpenChat();

            page.Chat.GetHiddenText(driver);
        }

        private IMapper _mapper => (new MapperConfiguration(cfg => cfg.CreateMap<ShippingInfo, ShippingPage>())).CreateMapper();
    }
}
