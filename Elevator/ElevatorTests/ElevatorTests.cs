using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElevatorNS;
using System;

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
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
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
            CollectionAssert.AreEquivalent(expectedCurrentQueue, elevator.currentQueue);
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
    }
}
