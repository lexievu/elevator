using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSVFileNS;
using System.Collections.Generic;
using PassengerNS;

namespace CSVFileTests
{
    [TestClass]
    public class CSVUnitTests
    {
        [TestMethod]
        public void ReadCSV()
        {
            List<Passenger> passengers = CSVFile.readPassengersCSV();

            Assert.AreEqual(3, passengers[2].id);
            Assert.AreEqual(1, passengers[12].atFloor);
            Assert.AreEqual(259, passengers[23].startWaitingAt);
            Assert.AreEqual(6, passengers[49].goingToFloor);
        }
    }
}
