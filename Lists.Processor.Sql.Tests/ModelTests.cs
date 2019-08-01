using NUnit.Framework;
using Lists.Processor.Sql.Operations;
using Lists.Processor.Sql;
using Lists.Processor.Sql.Models;
using System.Data.Common;
using Moq;
using System;

namespace Tests
{
    [TestFixture]
    public class ModelTests
    {
        Mock<IDatabase> _dbMock;

        [SetUp]
        public void Setup()
        {
            _dbMock = new Mock<IDatabase>();
        }

        [Test]
        public void Should_Hydrate_DosList()
        {
            var dateTime = DateTime.UtcNow;
            var readerMock = new Mock<DbDataReader>();
            readerMock.Setup(x => x[It.Is<string>(s => s.Contains("Date"))]).Returns(dateTime.ToString());
            readerMock.Setup(x => x[It.Is<string>(s => s == "isActive")]).Returns("true");
            readerMock.Setup(x => x[It.Is<string>(s => s == "name")]).Returns("some string");
            readerMock.Setup(x => x[It.Is<string>(s => s == "description")]).Returns("some desc");

            var model = new DosList(readerMock.Object);
            Assert.AreEqual("some string", model.name);
            Assert.AreEqual("some desc", model.description);
            Assert.AreEqual(true, model.isActive);
            Assert.AreEqual(dateTime.ToString(), model.createDate.ToString());
            Assert.AreEqual(dateTime.ToString(), model.lastUpdateDate.ToString());
        }
    }
}