using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace ElevatorNS
{
    public class Program
    {
        public static void Main()
        {

        }
    }
    
    public class Elevator
    {
        private int currentTime = 0;
        public List<int> peopleInLift = new List<int>();
        private int currentFloor = 1;
        public List<int> currentQueue = new List<int>();
        public List<int> oppositeQueue = new List<int>();
        public ElevatorDirection Direction = ElevatorDirection.STATIONARY;
        private int topFloor;

        public Elevator (int currentFloor = 1, int numberOfFloors = 10)
        {
            this.currentFloor = currentFloor; 
            topFloor = numberOfFloors; 
        }

        void addRow () { }

        public void addFloorToQueue (int floor) {
            // Only do something if the given floor is not the current floor
            if (Math.Abs(currentFloor - floor) > 0.0001)
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

        void pickUp (int passenger) { }

        public enum ElevatorDirection
        {
            UP,
            DOWN,
            STATIONARY
        }
    }
}
