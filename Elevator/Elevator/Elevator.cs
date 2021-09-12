﻿using System;
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
    // public class Program {
    //     public static void Main() {
    //         var path = @"/Users/thienhuongvu/elevator/data.csv"; // Person ID,At Floor,Going to Floor,Time
    //         using (TextFieldParser csvParser = new TextFieldParser(path))
    //         {
    //             csvParser.CommentTokens = new string[] { "#" };
    //             csvParser.SetDelimiters(new string[] { "," });
    //             csvParser.HasFieldsEnclosedInQuotes = true;

    //             // Skip the row with the column names
    //             csvParser.ReadLine();

    //             while (!csvParser.EndOfData) 
    //             {
    //                 // Read current line fields, pointer moves to the next line.
    //                 string[] fields = csvParser.ReadFields(); // Person ID,At Floor,Going to Floor,Time
    //                 int personID = Int32.Parse(fields[0]);
    //                 int atFloor = Int32.Parse(fields[1]); 
    //                 int goingToFloor = Int32.Parse(fields[2]); 
    //                 int time = Int32.Parse(fields[3]); 
    //                 Console.WriteLine(personID.ToString() + " " + atFloor.ToString()+ " " + goingToFloor.ToString()+ " " + time.ToString());
    //             }
    //         }
    //     }
    // }

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
