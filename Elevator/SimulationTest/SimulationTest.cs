using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulationNS; 
using System.Collections.Generic;
using PassengerNS;
using ElevatorNS;

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
        public void adjustingPassengerLists() 
        {
            Simulation simulation = new Simulation(); 
            simulation.elevator.currentFloor = 5;
            simulation.time = 1; 

            Passenger pas1 = new Passenger(1, 5, 1, 1); 
            Passenger pas2 = new Passenger(2, 7, 1, 1);
            Passenger pas3 = new Passenger(3, 4, 8, 1);
            Passenger pas4 = new Passenger(4, 5, 2, 1);
            Passenger pas5 = new Passenger(5, 9, 3, 5);

            simulation.remainingPassengers = new List<Passenger>() {pas1, pas2, pas3, pas4, pas5}; 

            simulation.movingFromRemainingPassengers(); 

            List<Passenger> expectedWaitingPassengers = new List<Passenger>() {pas2, pas3};
            List<Passenger> expectedPeopleInLift = new List<Passenger>() {pas1, pas4};
            List<Passenger> expectedRemainingPassengers = new List<Passenger>() {pas5};
            
            CollectionAssert.AreEquivalent(expectedWaitingPassengers, simulation.waitingPassengers);
            CollectionAssert.AreEquivalent(expectedPeopleInLift, simulation.elevator.peopleInLift); 
            CollectionAssert.AreEquivalent(expectedRemainingPassengers, simulation.remainingPassengers);
        }

        [TestMethod]
        public void testMovingFromWaitingPassengers() 
        {
            Simulation simulation = new Simulation();
            simulation.elevator.currentFloor = 5;
            simulation.time = 40;
            simulation.elevator.Direction = Elevator.ElevatorDirection.UP;

            Passenger pas1 = new Passenger(1, 1, 5, 1);
            Passenger pas2 = new Passenger(2, 2, 7, 4);
            Passenger pas3 = new Passenger(3, 7, 4, 10);
            Passenger pas4 = new Passenger(4, 5, 9, 30);
            Passenger pas5 = new Passenger(5, 2, 10, 35);

            simulation.elevator.peopleInLift = new List<Passenger>() { pas1, pas2 };
            simulation.waitingPassengers = new List<Passenger>() { pas3, pas4, pas5 };

            simulation.elevator.dropOffPassengers();
            simulation.movingFromWaitingPassengers();

            var expectedPeopleInLift = new List<Passenger>() {pas2, pas4}; 
            var expectedWaitingPassengers = new List<Passenger>() {pas3, pas5};

            CollectionAssert.AreEquivalent(expectedPeopleInLift, simulation.elevator.peopleInLift); 
            CollectionAssert.AreEquivalent(expectedWaitingPassengers, simulation.waitingPassengers);
        }

        [TestMethod]
        public void adjustPassengerList1()
        {
            Simulation simulation = new Simulation();
            simulation.elevator.currentFloor = 5;
            simulation.time = 40;
            simulation.elevator.Direction = Elevator.ElevatorDirection.UP;

            Passenger pas1 = new Passenger(1, 1, 5, 1);
            Passenger pas2 = new Passenger(2, 2, 7, 4);
            Passenger pas3 = new Passenger(3, 7, 4, 10);
            Passenger pas4 = new Passenger(4, 5, 9, 30);
            Passenger pas5 = new Passenger(5, 2, 10, 35);
            Passenger pas6 = new Passenger(6, 5, 10, 40);
            Passenger pas7 = new Passenger(7, 4, 1, 40);
            Passenger pas8 = new Passenger(8, 10, 2, 52);

            simulation.elevator.peopleInLift = new List<Passenger>() { pas1, pas2 };
            simulation.waitingPassengers = new List<Passenger>() { pas3, pas4, pas5 };
            simulation.remainingPassengers = new List<Passenger>() { pas6, pas7, pas8 };

            simulation.elevator.dropOffPassengers();
            simulation.movingFromWaitingPassengers();
            simulation.movingFromRemainingPassengers();

            var expectedPeopleInLift = new List<Passenger>() { pas2, pas4, pas6 };
            var expectedWaitingPassengers = new List<Passenger>() { pas3, pas5, pas7 };
            var expectedRemainingPassengers = new List<Passenger>() { pas8 };

            CollectionAssert.AreEquivalent(expectedPeopleInLift, simulation.elevator.peopleInLift);
            CollectionAssert.AreEquivalent(expectedWaitingPassengers, simulation.waitingPassengers);
            CollectionAssert.AreEquivalent(expectedRemainingPassengers, simulation.remainingPassengers);
        }

        [TestMethod]
        public void increment_mockTest()
        {
            Simulation simulation = new Simulation(); 
            simulation.elevator.currentFloor = 4.9;
            simulation.time = 39;
            simulation.elevator.Direction = Elevator.ElevatorDirection.UP;

            Passenger pas1 = new Passenger(1,1,5,1); 
            Passenger pas2 = new Passenger(2,2,7,4); 
            Passenger pas3 = new Passenger(3,7,4,10); 
            Passenger pas4 = new Passenger(4,5,9,30); 
            Passenger pas5 = new Passenger(5,2,10,35); 
            Passenger pas6 = new Passenger(6,5,10,40); 
            Passenger pas7 = new Passenger(7,4,1,40); 
            Passenger pas8 = new Passenger(8,10,2,52);

            simulation.elevator.peopleInLift = new List<Passenger>() {pas1, pas2};
            simulation.waitingPassengers = new List<Passenger>() {pas3, pas4, pas5}; 
            simulation.remainingPassengers = new List<Passenger>() {pas6, pas7, pas8}; 

            simulation.elevator.currentQueue = new List<int>() {5,7}; 
            simulation.elevator.oppositeQueue = new List<int>() {2}; 

            simulation.increment(); 

            var expectedPeopleInLift = new List<Passenger>(){pas2, pas4, pas6};
            var expectedWaitingPassengers = new List<Passenger>(){pas3, pas5, pas7}; 
            var expectedRemainingPassengers = new List<Passenger>(){pas8}; 
            var expectedCurrentQueue = new List<int>(){7,9,10};
            var expectedOppositeQueue = new List<int>(){4,2};

            Assert.AreEqual(40, simulation.time, 0.001);
            Assert.AreEqual(5, simulation.elevator.currentFloor, 0.001);
            CollectionAssert.AreEquivalent(expectedPeopleInLift, simulation.elevator.peopleInLift); 
            CollectionAssert.AreEquivalent(expectedWaitingPassengers, simulation.waitingPassengers); 
            CollectionAssert.AreEquivalent(expectedRemainingPassengers, simulation.remainingPassengers); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, simulation.elevator.currentQueue); 
            CollectionAssert.AreEquivalent(expectedOppositeQueue, simulation.elevator.oppositeQueue); 
        }
    }
}
