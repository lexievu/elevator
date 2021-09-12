using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulationNS; 
using System.Collections.Generic;
using PassengerNS;

namespace SimulationTestNS
{
    [TestClass]
    public class SimulationTest
    {
        [TestMethod]
        public void addAllPassengersTest()
        {
            Simulation simulation = new Simulation();
            
            Assert.AreEqual(3, simulation.allPassengers[2].id);
            Assert.AreEqual(1, simulation.allPassengers[12].atFloor);
            Assert.AreEqual(259, simulation.allPassengers[23].startWaitingAt);
            Assert.AreEqual(6, simulation.allPassengers[49].goingToFloor);
        }

        [TestMethod]
        public void pickUpPassengers () {
            Simulation simulation = new Simulation(); 
            Passenger pas1 = new Passenger(1, 1, 5, 1); 
            Passenger pas2 = new Passenger(2, 1, 1, 1);

            simulation.pickUpPassenger(pas1); 
            simulation.pickUpPassenger(pas2);

            List<int> expectedFloorQueue = new List<int>() {5}; 

            Assert.AreEqual(1, simulation.elevator.peopleInLift.Count, 0.001);
            Assert.AreEqual(5, simulation.elevator.peopleInLift[0].goingToFloor, 0.001);
            CollectionAssert.AreEquivalent(expectedFloorQueue, simulation.elevator.currentQueue);
        }

        [TestMethod]
        public void dropOffPassengers () {
            Simulation simulation = new Simulation(); 
            simulation.elevator.currentFloor = 5; 
            Passenger pas1 = new Passenger(1, 1, 5, 1); 
            Passenger pas2 = new Passenger(2, 7, 1, 1);
            Passenger pas3 = new Passenger(3, 4, 8, 1);
            Passenger pas4 = new Passenger(4, 2, 5, 1);
            Passenger pas5 = new Passenger(5, 9, 3, 1);

            simulation.pickUpPassenger(pas1); 
            simulation.pickUpPassenger(pas2);
            simulation.pickUpPassenger(pas3);
            simulation.pickUpPassenger(pas4);
            simulation.pickUpPassenger(pas5);

            List<Passenger> expectedPassengers = new List<Passenger>() {pas2, pas3, pas5}; 

            Assert.AreEqual(3, simulation.elevator.peopleInLift.Count, 0.001);
            Assert.AreEqual(1, simulation.elevator.peopleInLift[0].goingToFloor, 0.001);
            CollectionAssert.AreEquivalent(expectedPassengers, simulation.elevator.peopleInLift);
        }

        [TestMethod]
        public void increment_adjustingPassengerLists() 
        {
            
        }
    }
}
