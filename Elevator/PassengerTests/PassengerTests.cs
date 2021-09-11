using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassengerNS; 

namespace PassengerTestsNS
{
    [TestClass]
    public class PassengerTests
    {
        [TestMethod]
        public void createPassenger()
        {
            Passenger pas = new Passenger(2, 5, 1, 4);

            Assert.AreEqual(2, pas.id);
            Assert.AreEqual(5, pas.atFloor);
            Assert.AreEqual(1, pas.goingToFloor);
            Assert.AreEqual(4, pas.startWaitingAt);
        }
    }
}
