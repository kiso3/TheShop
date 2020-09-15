using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheShop;
using TheShop.Domain;

namespace TestTheShop
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<ISupplire> supplires = new List<ISupplire>() { new Supplier1(), new Supplier2(), new Supplier3() };
            ShopService shopService = new ShopService(new DatabaseDriver(), new Logger(), supplires);

            
            var response = shopService.OrderAndSellArticle(1, 458, 10);
            // Act
            

            // Assert
            Product product;
            Assert.IsTrue(response.TryGetContentValue<Product>(out product));
            Assert.AreEqual(10, product.Id);

        }
    }
}
