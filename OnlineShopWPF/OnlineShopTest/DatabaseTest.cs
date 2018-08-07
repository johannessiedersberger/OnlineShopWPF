using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using OnlineShop;
using System.Data;

namespace OnlineShopTest
{
  class DatabaseTest
  {
    [Test]
    public void Test1()
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
