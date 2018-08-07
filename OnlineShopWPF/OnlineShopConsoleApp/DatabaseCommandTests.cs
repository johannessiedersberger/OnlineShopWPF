using Moq;
using NUnit.Framework;
using OnlineShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopConsoleApp
{
  [TestFixture]
  class DatabaseCommandTests
  {
    [Test]
    public void AddValueTest()
    {
      var commandMock = new Mock<IDbCommand>(MockBehavior.Strict);
      var parameterCollectionMock = new Mock<IDataParameterCollection>();
      commandMock.SetupProperty(c => c.Parameters);

      var db = new Mock<IDatabase>(MockBehavior.Strict);
      db.Setup(d => d.Open());
      db.Setup(d => d.CreateCommand(CPU.CommandAddCPU)).Returns(commandMock.Object);

      //CPU cpu = new CPU(4, 3.2, "Test");
      //cpu.WriteToDatabase(db.Object);

      //Assert.That(() => db.Verify(d => d.CreateParameter("$id", null), Times.Once), Throws.Nothing);
      //Assert.That(() => db.Verify(d => d.CreateParameter("$count", "4"), Times.Once), Throws.Nothing);
      //Assert.That(() => db.Verify(d => d.CreateParameter("$clockRate", "3.2"), Times.Once), Throws.Nothing);
      //Assert.That(() => db.Verify(d => d.CreateParameter("$name", "Test"), Times.Once), Throws.Nothing);

    }

  }
}
