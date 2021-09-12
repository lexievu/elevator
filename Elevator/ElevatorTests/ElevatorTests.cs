using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElevatorNS;
using System;
using System.Collections.Generic;

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

            elevator.oppositeQueue.Add(8);
            elevator.oppositeQueue.Add(9);

            // Act
            elevator.addFloorToQueue(7);

            int[] expectedCurrentQueue = { 4, 2 };
            int[] expectedOppositeQueue = { 7, 8, 9 };

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
    }
}
