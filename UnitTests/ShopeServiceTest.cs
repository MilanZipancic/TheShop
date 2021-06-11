using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using TheShop;
using TheShop.API;
using TheShop.DataAccess;
using TheShop.Logging;
using TheShop.Model;

namespace UnitTests
{
    [TestClass]
    public class ShopeServiceTest
    {
        [TestMethod]
        public void OrderAndSellArticle_TooSmallMaxExpectedPrice_ThrowsException()
        {
            //Arrange
            var mockedLogger = new Mock<ILogger>();
            mockedLogger.Setup(logger => logger.Info(It.IsAny<string>()));
            mockedLogger.Setup(logger => logger.Error(It.IsAny<string>()));
            mockedLogger.Setup(logger => logger.Debug(It.IsAny<string>()));

            var mocekedDB = new Mock<IDatabaseDriver>();
            mocekedDB.Setup(db => db.Save(It.IsAny<Article>()));
            mocekedDB.Setup(db => db.GetById(It.IsAny<int>())).Returns(new Article());

            //act and assert
            Assert.ThrowsException<Exception>(() => new ShopService(mockedLogger.Object, mocekedDB.Object).OrderAndSellArticle(0, 457, 0, GetMockedSuppliersList()));
        }

        private List<ISupplier> GetMockedSuppliersList()
        {
            var mockedList = new List<ISupplier>();

            var mockSupplier1 = new Mock<ISupplier>();
            mockSupplier1.Setup(s => s.ArticleInInventory(It.IsAny<int>())).Returns(true);
            mockSupplier1.Setup(s => s.GetArticle(It.IsAny<int>())).Returns(new Article()
            {
                ArticlePrice = 458
            });
            mockedList.Add(mockSupplier1.Object);

            var mockSupplier2 = new Mock<ISupplier>();
            mockSupplier2.Setup(s => s.ArticleInInventory(It.IsAny<int>())).Returns(true);
            mockSupplier2.Setup(s => s.GetArticle(It.IsAny<int>())).Returns(new Article()
            {
                ArticlePrice = 459
            });
            mockedList.Add(mockSupplier2.Object);

            var mockSupplier3 = new Mock<ISupplier>();
            mockSupplier3.Setup(s => s.ArticleInInventory(It.IsAny<int>())).Returns(true);
            mockSupplier3.Setup(s => s.GetArticle(It.IsAny<int>())).Returns(new Article()
            {
                ArticlePrice = 460
            });
            mockedList.Add(mockSupplier3.Object);

            return mockedList;
        }
    }
}
