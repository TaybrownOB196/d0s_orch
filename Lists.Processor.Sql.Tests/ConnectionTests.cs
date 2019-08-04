using NUnit.Framework;
using Lists.Processor.Sql;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Lists.Processor.Options;

namespace Tests
{
    [TestFixture]
    public class ConnectionTests
    {
        Mock<ILogger<Database>> _loggerMock;
        Mock<IOptionsMonitor<SqlOptions>> _optionsMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Database>>();
            _optionsMock = new Mock<IOptionsMonitor<SqlOptions>>();
        }

        [Test]
        public void Should_Connect()
        {
            var sqlOptions = new SqlOptions();
            sqlOptions.ConnectionString = "server=192.168.99.100;database=temp;port=9999;user=dosorchuser;password=password;";
            _optionsMock.Setup(x => x.CurrentValue).Returns(sqlOptions);
            var db = new Database(_optionsMock.Object, _loggerMock.Object);
            db.connect();
        }
    }
}