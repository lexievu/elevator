using System;
using System.Collections.Generic;


namespace ElevatorNS
{
    // public class Program
    // {
    //     public static void Main(string[] args)
    //     {
    //         Elevator elevator = new Elevator(5); 

    //         elevator.Direction = Elevator.ElevatorDirection.UP;
    //         elevator.currentQueue.Add(4);
    //         elevator.currentQueue.Add(6);
    //         elevator.currentQueue.Add(10);
    //         elevator.oppositeQueue.Add(1);

    //         // Act
    //         elevator.addFloorToQueue(7);

    //         Console.WriteLine(String.Join("; ", elevator.currentQueue));
    //         Console.WriteLine(String.Join("; ", elevator.oppositeQueue));
    //     }
    // }

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

        void pickUp (int passenger) { }

        public enum ElevatorDirection
        {
            UP,
            DOWN,
            STATIONARY
        }
    }
}
