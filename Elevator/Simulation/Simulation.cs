using System;
using PassengerNS; 
using CSVFileNS;
using ElevatorNS;
using System.Collections.Generic;

namespace SimulationNS
{
    public class Program 
    {
        public static void Main () 
        {
            Simulation simulation = new Simulation(); 

            do 
            {
                simulation.increment(); 
                // The total floor queue will be the concatenation of the current queue and the opposite queue
                List<int> currentQueue = new List<int>(simulation.elevator.currentQueue);
                currentQueue.AddRange(simulation.elevator.oppositeQueue);
                CSVFile.WriteResults(simulation.time, simulation.elevator.peopleInLift.ConvertAll(passenger => passenger.id), simulation.elevator.currentFloor, currentQueue);
            }
            while (simulation.remainingPassengers.Count > 0 || simulation.elevator.peopleInLift.Count > 0);
        }
    }

    /// <summary>
    /// This simulation class encapsulates the information and functionalities needed for the Prototype. 
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// allPassengers is the list of all the passengers as read from the input csv file. 
        /// </summary>
        public readonly List<Passenger> allPassengers = new List<Passenger>();
        /// <summary>
        /// There are 3 lists of passengers that we're most interested at as a part of this prototype: 
        /// (1) elevator.peopleInLift: list of the passengers that are currently in the lift, waiting to be delivered to their desired floors. 
        /// (2) waitingPassengers: list of the passengers that are outside of the elevator, waiting for the elevator to pick them up 
        /// (3) remainingPassengers: list of the passengers that will call the elevator in the future (but have not called the elevator yet). We assume that this list is sorted by the time the passenger starts waiting outside the elevator. 
        /// </summary>
        private List<Passenger> _remainingPassengers = new List<Passenger>();
        private List<Passenger> _waitingPassengers = new List<Passenger>(); 
        private int _time = 0; 
        private Elevator _elevator; 

        public List<Passenger> remainingPassengers 
        {
            get 
            {
                return _remainingPassengers; 
            }
            set 
            {
                _remainingPassengers = value;
            }
        }

        public List<Passenger> waitingPassengers 
        {
            get
            {
                return _waitingPassengers; 
            }
            set 
            {
                _waitingPassengers = value;
            }
        }

        public int time  
        {
            get 
            {
                return _time;
            }
            set 
            {
                _time = value;
            }
        }

        public Elevator elevator 
        {
            get 
            {
                return _elevator;
            }
            set 
            {
                _elevator = value;
            }
        }

        /// <summary>
        /// Instantiates an instance of the prototype
        /// </summary>
        public Simulation() 
        {
            allPassengers = CSVFile.readPassengersCSV();
            remainingPassengers = new List<Passenger>(allPassengers);
            elevator = new Elevator();
        }

        /// <summary>
        /// This function moves a passenger from the waitingPassengers list to the elevator.peopleInLift list, when the elevator arrives at the floor where the passenger is waiting. 
        /// </summary>
        public void movingFromWaitingPassengers() 
        {
            if (waitingPassengers.Count > 0) 
            {
                List<Passenger> passengersToRemove = new List<Passenger>(); 

                foreach (var passenger in waitingPassengers)
                {
                    if (Math.Abs(passenger.atFloor - elevator.currentFloor) < 0.0001 && Math.Abs(passenger.goingToFloor - elevator.currentFloor) > 0.0001)
                    {
                        // Pick up the passengers that are not being picked up and dropped off at the same floor (e.g. passenger changed their mind)
                        if (elevator.peopleInLift.Count < 8) 
                        {
                            elevator.peopleInLift.Add(passenger); 
                            elevator.addFloorToQueue(passenger.goingToFloor);
                            passengersToRemove.Add(passenger);
                        }
                        
                    }
                }
                
                // Remove the passengers waiting at the current floor from the list of waitingPassengers
                // waitingPassengers.RemoveAll(passenger => Math.Abs(passenger.atFloor - elevator.currentFloor) < 0.0001);
                if (passengersToRemove.Count > 0)
                {
                    foreach (Passenger passenger in passengersToRemove)
                    {
                        waitingPassengers.Remove(passenger);
                    }
                }
                
            }
        }

        /// <summary>
        /// This function moves a passenger from the remainingPassengers list to either the elevator.peopleInLift list or waitingPassengers list, when the time has passed the time the passengers would start waiting.
        /// If the current floor is the floor where the passenger is waiting at, the passenger will enter the list and be in the peopleInLift list. 
        /// If the passenger is waiting at a different floor, the passenger will be in the waitingPassengers list, waiting for the lift to arrive.  
        /// </summary>
        public void movingFromRemainingPassengers() 
        {
            // Assume that the remaining passengers list is sorted by the start waiting at time
            while (remainingPassengers.Count >= 1 && remainingPassengers[0].startWaitingAt <= time) 
            {
                elevator.addFloorToQueue(remainingPassengers[0].atFloor); // Note: the elevator is not aware of the passenger's goingToFloor until the passenger has entered the elevator
                if (Math.Abs(remainingPassengers[0].atFloor - elevator.currentFloor) > 0.0001 || elevator.peopleInLift.Count >= elevator.maximumCapacity)
                {
                    waitingPassengers.Add(remainingPassengers[0]); 
                } 
                else 
                {
                    elevator.peopleInLift.Add(remainingPassengers[0]); 
                    elevator.addFloorToQueue(remainingPassengers[0].goingToFloor);
                }
                remainingPassengers.RemoveAt(0); 
            }
        }

        /// <summary>
        /// This function calculates the new state of the prototype after a single time step. 
        /// </summary>
        public void increment(int timeStep = 1) 
        {
            time += timeStep; 

            this.elevator.increment(timeStep);

            movingFromWaitingPassengers();
            movingFromRemainingPassengers();
        }
    }
}
