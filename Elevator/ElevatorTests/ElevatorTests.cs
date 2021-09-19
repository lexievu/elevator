using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElevatorNS;
using System;
using System.Collections.Generic;
using PassengerNS;

namespace ElevatorTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void addHigherFloor_elevatorUp()
        {
            // Arrange
            Elevator elevator = new Elevator(3);
            elevator.Direction = Elevator.ElevatorDirection.UP;
            elevator.currentQueue.Add(4);
            elevator.currentQueue.Add(6);
            elevator.currentQueue.Add(10);
            elevator.oppositeQueue.Add(1);

            // Act
            elevator.addFloorToQueue(7);

            int[] expectedCurrentQueue = { 4, 6, 7, 10 };
            int[] expectedOppositeQueue = { 1 };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void addLowerFloor_elevatorUp()
        {
            // Arrange
            Elevator elevator = new Elevator(3);
            elevator.Direction = Elevator.ElevatorDirection.UP;
            elevator.currentQueue.Add(4);
            elevator.currentQueue.Add(6);
            elevator.currentQueue.Add(10);

            elevator.oppositeQueue.Add(1);

            // Act
            elevator.addFloorToQueue(2);

            int[] expectedCurrentQueue = { 4, 6, 10 };
            int[] expectedOppositeQueue = { 2, 1 };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void addCurrentFloor_elevatorUp()
        {
            // Arrange
            Elevator elevator = new Elevator(3);
            elevator.Direction = Elevator.ElevatorDirection.UP;
            elevator.currentQueue.Add(4);
            elevator.currentQueue.Add(6);
            elevator.currentQueue.Add(10);

            elevator.oppositeQueue.Add(1);

            // Act
            elevator.addFloorToQueue(3);

            int[] expectedCurrentQueue = { 4, 6, 10 };
            int[] expectedOppositeQueue = { 1 };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue, String.Join("; ", elevator.currentQueue));
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void addHigherFloor_elevatorDown()
        {
            // Arrange
            Elevator elevator = new Elevator(5);
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.currentQueue.Add(4);
            elevator.currentQueue.Add(2);

            elevator.oppositeQueue.Add(6);
            elevator.oppositeQueue.Add(9);

            // Act
            elevator.addFloorToQueue(7);

            int[] expectedCurrentQueue = { 4, 2 };
            int[] expectedOppositeQueue = { 6, 7, 9 };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue, String.Join("; ", elevator.currentQueue));
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void addLowerFloor_elevatorDown()
        {
            // Arrange
            Elevator elevator = new Elevator(5);
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.currentQueue.Add(4);
            elevator.currentQueue.Add(2);

            elevator.oppositeQueue.Add(8);
            elevator.oppositeQueue.Add(9);

            // Act
            elevator.addFloorToQueue(3);

            int[] expectedCurrentQueue = { 4, 3, 2 };
            int[] expectedOppositeQueue = { 8, 9 };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void addCurrentFloor_elevatorDown()
        {
            // Arrange
            Elevator elevator = new Elevator(5);
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.currentQueue.Add(4);
            elevator.currentQueue.Add(2);

            elevator.oppositeQueue.Add(8);
            elevator.oppositeQueue.Add(9);

            // Act
            elevator.addFloorToQueue(5);

            int[] expectedCurrentQueue = { 4, 2 };
            int[] expectedOppositeQueue = { 8, 9 };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void addHigherFloor_elevatorStationary()
        {
            // Arrange
            Elevator elevator = new Elevator(5);
            elevator.Direction = Elevator.ElevatorDirection.STATIONARY;

            // Act
            elevator.addFloorToQueue(7);

            int[] expectedCurrentQueue = { 7 };
            int[] expectedOppositeQueue = {  };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
            Assert.AreEqual(Elevator.ElevatorDirection.UP, elevator.Direction);
        }

        [TestMethod]
        public void addLowerFloor_elevatorStationary()
        {
            // Arrange
            Elevator elevator = new Elevator(5);
            elevator.Direction = Elevator.ElevatorDirection.STATIONARY;

            // Act
            elevator.addFloorToQueue(2);

            int[] expectedCurrentQueue = { 2 };
            int[] expectedOppositeQueue = { };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
            Assert.AreEqual(Elevator.ElevatorDirection.DOWN, elevator.Direction);
        }

        [TestMethod]
        public void addCurrentFloor_elevatorStationary()
        {
            // Arrange
            Elevator elevator = new Elevator(5);
            elevator.Direction = Elevator.ElevatorDirection.STATIONARY;

            // Act
            elevator.addFloorToQueue(5);

            int[] expectedCurrentQueue = { };
            int[] expectedOppositeQueue = { };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
            Assert.AreEqual(Elevator.ElevatorDirection.STATIONARY, elevator.Direction);
        }

        [TestMethod] 
        public void changeDirection_bothListEmpty() 
        {
            List<int> emptyList = new List<int>();
            Elevator elevator = new Elevator (3); 
            elevator.currentQueue = new List<int>(); 
            elevator.oppositeQueue = new List<int>(); 
            elevator.Direction = Elevator.ElevatorDirection.UP; 
            elevator.changeDirection(); 

            Assert.AreEqual(Elevator.ElevatorDirection.STATIONARY, elevator.Direction); 
            CollectionAssert.AreEquivalent(emptyList, elevator.currentQueue);
            CollectionAssert.AreEquivalent(emptyList, elevator.oppositeQueue);
        }

        [TestMethod]
        public void changeDirection_upToDown() 
        {
            Elevator elevator = new Elevator(3); 
            elevator.currentQueue = new List<int>(); 
            elevator.oppositeQueue = new List<int>() {7, 3, 1};
            elevator.Direction = Elevator.ElevatorDirection.UP; 
            elevator.changeDirection(); 

            List<int> newCurrentQueue = new List<int>() {7, 3, 1};
            List<int> newOppositeQueue = new List<int>();  
            
            Assert.AreEqual(Elevator.ElevatorDirection.DOWN, elevator.Direction); 
            CollectionAssert.AreEquivalent(newCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(newOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void changeDirection_downToUp() 
        {
            Elevator elevator = new Elevator(3); 
            elevator.currentQueue = new List<int>(); 
            elevator.oppositeQueue = new List<int>() {4,7,9};
            elevator.Direction = Elevator.ElevatorDirection.DOWN; 
            elevator.changeDirection(); 

            List<int> newCurrentQueue = new List<int>() {4,7,9};
            List<int> newOppositeQueue = new List<int>();  
            
            Assert.AreEqual(Elevator.ElevatorDirection.UP, elevator.Direction); 
            CollectionAssert.AreEquivalent(newCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(newOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
            "The current queue inappropriately not empty allowed.")]
        public void changeDirection_NotEmptyCurrentQueue()
        {
            Elevator elevator = new Elevator(3);
            elevator.currentQueue = new List<int> { 2 };
            elevator.oppositeQueue = new List<int>() { 4, 7, 9 };
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.changeDirection();
        }

        [TestMethod]
        public void increment_elevatorStationary() {
            Elevator elevator = new Elevator(3); 
            elevator.increment(); 

            List<int> emptyList = new List<int>(); 

            Assert.AreEqual(3, elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.STATIONARY, elevator.Direction); 
            CollectionAssert.AreEquivalent(emptyList, elevator.currentQueue);
            CollectionAssert.AreEquivalent(emptyList, elevator.oppositeQueue);
        }

        [TestMethod]
        public void increment_elevatorUp() {
            Elevator elevator = new Elevator(3); 
            elevator.Direction = Elevator.ElevatorDirection.UP;
            elevator.currentQueue = new List<int>(){5}; 
            elevator.increment(); 

            List<int> emptyList = new List<int>(); 
            List<int> expectedCurrentQueue = new List<int>(){5}; 

            Assert.AreEqual(3.1 , elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.UP, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(emptyList, elevator.oppositeQueue);
        }

        [TestMethod]
        public void increment_elevatorDown() {
            Elevator elevator = new Elevator(3); 
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.currentQueue = new List<int>(){1}; 
            elevator.increment(); 

            List<int> emptyList = new List<int>(); 
            List<int> expectedCurrentQueue = new List<int>(){1}; 

            Assert.AreEqual(2.9 , elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.DOWN, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(emptyList, elevator.oppositeQueue);
        }

        [TestMethod]
        public void increment_elevatorUp_removeFromQueue() {
            Elevator elevator = new Elevator(4.9); 
            elevator.Direction = Elevator.ElevatorDirection.UP;
            elevator.currentQueue = new List<int>(){5, 7};
            elevator.oppositeQueue = new List<int>(){3,2,1}; 
            elevator.increment(); 

            List<int> expectedOppositeQueue = new List<int>(){3,2,1}; 
            List<int> expectedCurrentQueue = new List<int>(){7}; 

            Assert.AreEqual(5 , elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.UP, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }
        
        [TestMethod]
        public void increment_elevatorDown_removeFromQueue() {
            Elevator elevator = new Elevator(3.1); 
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.currentQueue = new List<int>(){3, 1}; 
            elevator.increment(); 

            List<int> emptyList = new List<int>(); 
            List<int> expectedCurrentQueue = new List<int>(){1}; 

            Assert.AreEqual(3 , elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.DOWN, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(emptyList, elevator.oppositeQueue);
        }

        [TestMethod]
        public void increment_elevatorUp_removeFromQueue_currentQueueEmpty_oppositeQueueNotEmpty() {
            Elevator elevator = new Elevator(4.9); 
            elevator.Direction = Elevator.ElevatorDirection.UP;
            elevator.currentQueue = new List<int>(){5};
            elevator.oppositeQueue = new List<int>(){3,2,1}; 
            elevator.increment(); 

            List<int> expectedCurrentQueue = new List<int>(){3,2,1}; 
            List<int> expectedOppositeQueue = new List<int>(); 

            Assert.AreEqual(5 , elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.DOWN, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void increment_elevatorDown_removeFromQueue_currentQueueEmpty_oppositeQueueNotEmpty() {
            Elevator elevator = new Elevator(3.1); 
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.currentQueue = new List<int>(){3}; 
            elevator.oppositeQueue = new List<int>(){5, 7};
            elevator.increment(); 

            List<int> expectedCurrentQueue = new List<int>(){5, 7}; 
            List<int> expectedOppositeQueue = new List<int>(); 

            Assert.AreEqual(3, elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.UP, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void increment_elevatorUp_removeFromQueue_currentQueueEmpty_oppositeQueueEmpty() {
            Elevator elevator = new Elevator(4.9); 
            elevator.Direction = Elevator.ElevatorDirection.UP;
            elevator.currentQueue = new List<int>(){5};
            elevator.oppositeQueue = new List<int>(); 
            elevator.increment(); 

            List<int> expectedCurrentQueue = new List<int>(); 
            List<int> expectedOppositeQueue = new List<int>(); 

            Assert.AreEqual(5 , elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.STATIONARY, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void increment_elevatorDown_removeFromQueue_currentQueueEmpty_oppositeQueueEmpty() 
        {
            Elevator elevator = new Elevator(7.1); 
            elevator.Direction = Elevator.ElevatorDirection.DOWN;
            elevator.currentQueue = new List<int>(){7}; 
            elevator.oppositeQueue = new List<int>();
            elevator.increment(); 

            List<int> expectedCurrentQueue = new List<int>(); 
            List<int> expectedOppositeQueue = new List<int>(); 

            Assert.AreEqual(7, elevator.currentFloor, 0.0001);
            Assert.AreEqual(Elevator.ElevatorDirection.STATIONARY, elevator.Direction); 
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }

        [TestMethod]
        public void testDropOff()
        {
            Elevator elevator = new Elevator(); 
            elevator.currentFloor = 5; 

            Passenger pas1 = new Passenger(1, 1, 5, 1);
            Passenger pas2 = new Passenger(2, 2, 7, 4);

            elevator.peopleInLift = new List<Passenger>() { pas1, pas2 };

            elevator.dropOffPassengers(); 

            var expectedPeopleInLift = new List<Passenger>() { pas2 };

            CollectionAssert.AreEquivalent(expectedPeopleInLift, elevator.peopleInLift);
        }

        [TestMethod]
        public void addFloorToQueueRep ()
        {
            // Arrange
            Elevator elevator = new Elevator(5);
            elevator.Direction = Elevator.ElevatorDirection.STATIONARY;

            // Act
            elevator.addFloorToQueue(1);
            elevator.addFloorToQueue(2);
            elevator.addFloorToQueue(7);
            elevator.addFloorToQueue(1);

            int[] expectedCurrentQueue = { 2, 1 };
            int[] expectedOppositeQueue = { 7 };

            // Assert
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
            CollectionAssert.AreEquivalent(expectedOppositeQueue, elevator.oppositeQueue);
        }
    }
}
