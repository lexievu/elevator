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

        [TestMethod]
        public void WriteToNewCSV() 
        {
            List<int> peopleInLift = new List<int>();
            peopleInLift.Add(4);
            peopleInLift.Add(34);
            peopleInLift.Add(7);

            List<int> floorQueue = new List<int>();
            floorQueue.Add(14);
            floorQueue.Add(2);
            floorQueue.Add(6);
            floorQueue.Add(3);
            floorQueue.Add(0);
            floorQueue.Add(45);

            string filePath = CSVFile.WriteResults(1, peopleInLift, 4, floorQueue); 

            Assert.AreEqual("/Users/thienhuongvu/Projects/elevator/Elevator/output.csv", filePath);
        }

        [TestMethod]
        public void WriteToSameCSV()
        {
            List<int> peopleInLift = new List<int>(){1,34,2};

            List<int> floorQueue = new List<int>(){12,4,5,3,2,4}; 

            string filePath = CSVFile.WriteResults(2, peopleInLift, 7, floorQueue);

            Assert.AreEqual("/Users/thienhuongvu/Projects/elevator/Elevator/output.csv", filePath);
        }
    }
}
