using APM.WebApi.Controllers;
using APM.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net.Http;
using System.Web.Http.Hosting;

namespace APM.WebApi.Tests
{
    [TestClass]
    public class TestProductsController
    {
        public IProductRepository productRepository { get; }

        // IProductRepository
        protected Mock<IProductRepository> ProductRepository { get; } = new Mock<IProductRepository>();

        [TestMethod]
        public void Get_ShouldReturnAllProducts()
        {
            // arrange
            var product = new List<Product>
            {
                Mock.Of<Product>(),
                Mock.Of<Product>(),
                Mock.Of<Product>()
            };
            var productsController = new ProductsController(ProductRepository.Object);
            ProductRepository.Setup(p => p.Retrieve()).Returns(product);

            // act
            IHttpActionResult actionResult = productsController.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<System.Linq.IQueryable<Product>>;

            // assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Get_ShouldReturnOneProduct()
        {
            // arrange
            var productId = 5;
            var product = new List<Product>()
            {
                new Product
                {
                    ProductId = 5,
                    Description = "Test product",
                    ProductName = "Hammer",
                    Price = 20,
                    ProductCode = "ABC-9876",
                    ReleaseDate = DateTime.Now.Date
                }
            };
            product.Add(Mock.Of<Product>());
            var productsController = new ProductsController(ProductRepository.Object);
            ProductRepository.Setup(p => p.Retrieve()).Returns(product);

            // act
            IHttpActionResult actionResult = productsController.Get(productId);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            // assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(productId, contentResult.Content.ProductId);
        }

        [TestMethod]
        // public IHttpActionResult Post([FromBody]Product product)
        public void Post_ShouldReturnCreatedProduct()
        {
            // arrange
            Product product = new Product()
            {
                ProductId = 0,
                Description = "Test product",
                ProductName = "Hammer",
                Price = 20,
                ProductCode = "ABC-9876",
                ReleaseDate = DateTime.Now
            } ;
            var productsController = new ProductsController(ProductRepository.Object);
            ProductRepository.Setup(p => p.Save(product)).Returns(product);

            productsController.Request = new HttpRequestMessage();
            productsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                  new HttpConfiguration());
            productsController.Request.RequestUri = new Uri("http://localhost");

            // act
            IHttpActionResult actionResult = productsController.Post(product);
            var contentResult = actionResult as CreatedNegotiatedContentResult<Product>;

            // assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(product.ProductId, contentResult.Content.ProductId);
        }

        [TestMethod]
        // public IHttpActionResult Post([FromBody]Product product)
        public void Post_Update_ShouldReturnCreatedProduct()
        {
            // arrange
            var productId = 5;
            Product product = new Product()
            {
                ProductId = productId,
                Description = "Test product",
                ProductName = "Hammer",
                Price = 20,
                ProductCode = "ABC-9876",
                ReleaseDate = DateTime.Now
            };
            var productsController = new ProductsController(ProductRepository.Object);
            ProductRepository.Setup(p => p.Save(productId, product)).Returns(product);

            productsController.Request = new HttpRequestMessage();
            productsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                  new HttpConfiguration());
            productsController.Request.RequestUri = new Uri("http://localhost");

            // act
            IHttpActionResult actionResult = productsController.Put(productId, product);
            var contentResult = actionResult as OkResult;

            // assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        //public void Delete(int id)
        // Method not implemented in Controller - refactor test once it is written
        public void Delete_ShouldReturnNull()
        {
            // arrange
            var productId = 5;
            Product product = new Product()
            {
                ProductId = productId,
                Description = "Test product",
                ProductName = "Hammer",
                Price = 20,
                ProductCode = "ABC-9876",
                ReleaseDate = DateTime.Now
            };
            var productsController = new ProductsController(ProductRepository.Object);
            //ProductRepository.Setup(p => p.Save(productId, product)).Returns(product);

            // act
            IHttpActionResult actionResult = productsController.Delete(productId);
            var contentResult = actionResult as OkResult;

            // assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }
            
    }
}
