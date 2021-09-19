using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using PassengerNS; 

namespace ElevatorNS
{

    public class Elevator
    {
        private List<Passenger> _peopleInLift = new List<Passenger>();
        private double _currentFloor = 1.0;
        private List<int> _currentQueue = new List<int>();
        private List<int> _oppositeQueue = new List<int>();
        private ElevatorDirection _Direction = ElevatorDirection.STATIONARY;
        private int topFloor;
        private double timeToTravelOneFloor = 10; 

        public double currentFloor 
        {
            get 
            {
                return _currentFloor;
            }
            set 
            {
                if (value >= 1.0 && value <= topFloor) 
                {
                    _currentFloor = value;
                }
            }
        }

        public List<Passenger> peopleInLift 
        {
            get 
            {
                return _peopleInLift;
            }
            set 
            {
                _peopleInLift = value;
            }
        }

        public List<int> currentQueue 
        {
            get 
            {
                return _currentQueue; 
            }
            set 
            {
                _currentQueue = value;
            }
        }

        public List<int> oppositeQueue 
        {
            get 
            {
                return _oppositeQueue;
            }
            set 
            {
                _oppositeQueue = value;
            }
        }

        public ElevatorDirection Direction 
        {
            get 
            {
                return _Direction;
            }
            set 
            {
                _Direction = value;
            }
        }

        public Elevator (double currentFloor = 1.0, int numberOfFloors = 10)
        {
            this.currentFloor = currentFloor; 
            topFloor = numberOfFloors; 
        }

        public void addFloorToQueue (int floor) {
            // Only do something if the given floor is not the current floor and not in the queue
            if (Math.Abs(currentFloor - floor) > 0.0001 && !currentQueue.Contains(floor) && !oppositeQueue.Contains(floor))
            {
                if (Direction == ElevatorDirection.UP)
                {
                    if (floor > currentFloor)
                    {
                        currentQueue.Add(floor);
                        currentQueue.Sort();
                    }
                    else if (floor < currentFloor)
                    {
                        oppositeQueue.Add(floor);
                        oppositeQueue.Sort((a,b) => b.CompareTo(a));
                    }
                }

                else if (Direction == ElevatorDirection.DOWN)
                {
                    if (floor > currentFloor)
                    {
                        oppositeQueue.Add(floor);
                        oppositeQueue.Sort(); 
                    }
                    else if (floor < currentFloor)
                    {
                        currentQueue.Add(floor);
                        currentQueue.Sort((a, b) => b.CompareTo(a)); 
                    }
                }
                else if (Direction == ElevatorDirection.STATIONARY)
                {
                    currentQueue.Add(floor);
                    if (floor > currentFloor)
                    {
                        Direction = ElevatorDirection.UP;
                    }
                    else if (floor < currentFloor)
                    {
                        Direction = ElevatorDirection.DOWN;
                    }
                } 
            }
        }

        public void changeDirection() 
        {
            // Thow an exception if the current queue is not empty 
            if (this.currentQueue.Count > 0) 
            {
                throw new InvalidOperationException("The elevator should not change direction while there are floors in the current queue."); 
            }
            else 
            {
                if (this.oppositeQueue.Count == 0) 
                {
                    this.Direction = ElevatorDirection.STATIONARY;
                }
                else if (this.oppositeQueue.Count > 0)
                {
                    currentQueue = new List<int>(oppositeQueue); 
                    oppositeQueue = new List<int>(); 
                    if (this.Direction == ElevatorDirection.UP) 
                    {
                        this.Direction = ElevatorDirection.DOWN;
                    }
                    else {
                        this.Direction = ElevatorDirection.UP;
                    }
                }
            }
        }

        public void dropOffPassengers() 
        {
            if (peopleInLift.Count > 0) 
            {
                // foreach (var passenger in peopleInLift) 
                // {
                //     if (Math.Abs(passenger.goingToFloor - currentFloor) < 0.0001) 
                //     {
                //         peopleInLift.Remove(passenger); 
                //     }
                // }

                peopleInLift.RemoveAll(passenger => Math.Abs(passenger.goingToFloor - currentFloor) < 0.0001);
            }
        }

        public void increment (double timeStep = 1) 
        {
            if (Direction != ElevatorDirection.STATIONARY) 
            {
                if (Direction == ElevatorDirection.UP) 
                {
                    this.currentFloor += timeStep / timeToTravelOneFloor; 
                }
                else if (Direction == ElevatorDirection.DOWN)
                {
                    this.currentFloor -= timeStep / timeToTravelOneFloor;
                }

                // Remove floor from queue when the elevator gets to the floor 
                if (this.currentQueue.Count > 0 && Math.Abs(currentFloor - currentQueue[0]) < 0.0001) 
                { 
                    this.currentQueue.RemoveAt(0); 
                    dropOffPassengers();
                }

                // If current queue is empty, change the elevator direction
                if (this.currentQueue.Count == 0) 
                {
                    this.changeDirection();
                }
            }
        }

        public enum ElevatorDirection
        {
            UP,
            DOWN,
            STATIONARY
        }
    }
}
