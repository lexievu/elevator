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
                if (simulation.elevator.currentQueue.Count > 0) 
                {
                    List<int> currentQueue = new List<int>(simulation.elevator.currentQueue);
                    currentQueue.AddRange(simulation.elevator.oppositeQueue);
                    CSVFile.WriteResults(simulation.time, simulation.elevator.peopleInLift.ConvertAll(passenger => passenger.id), simulation.elevator.currentFloor, currentQueue);
                }
                else {
                    CSVFile.WriteResults(simulation.time, simulation.elevator.peopleInLift.ConvertAll(passenger => passenger.id), simulation.elevator.currentFloor,simulation.elevator.oppositeQueue);
                }
            }
            while (simulation.allPassengers.Count > 0 || simulation.elevator.peopleInLift.Count > 0);
        }
    }

    public class Simulation
    {
        public readonly List<Passenger> allPassengers = new List<Passenger>();
        public List<Passenger> remainingPassengers = new List<Passenger>();
        public List<Passenger> waitingPassengers = new List<Passenger>(); 
        public int time = 0; 
        public Elevator elevator; 

        public Simulation() 
        {
            allPassengers = CSVFile.readPassengersCSV();
            remainingPassengers = new List<Passenger>(allPassengers);
            elevator = new Elevator();
        }

        public void movingFromWaitingPassengers() 
        {
            if (waitingPassengers.Count > 0) {
                foreach (var passenger in waitingPassengers)
                {
                    if (Math.Abs(passenger.atFloor - elevator.currentFloor) < 0.0001 && Math.Abs(passenger.goingToFloor - elevator.currentFloor) > 0.0001)
                    {
                        // Pick up the passengers that are not being picked up and dropped off at the same floor (e.g. passenger changed their mind)

                        elevator.peopleInLift.Add(passenger); 
                        elevator.addFloorToQueue(passenger.goingToFloor);
                    }
                }

                waitingPassengers.RemoveAll(passenger => Math.Abs(passenger.atFloor - elevator.currentFloor) < 0.0001 && Math.Abs(passenger.goingToFloor - elevator.currentFloor) > 0.0001);
            }
        }

        public void pickUpPassenger(Passenger passenger)
        {
            if (Math.Abs(passenger.goingToFloor - elevator.currentFloor) > 0.0001)
                {
                    // Pick up the passengers that are not being picked up and dropped off at the same floor (e.g. passenger changed their mind)

                    elevator.peopleInLift.Add(passenger);
                    elevator.addFloorToQueue(passenger.goingToFloor);
                }
        }

        public void movingFromRemainingPassengers() 
        {
            // Assume that the remaining passengers list is sorted by the start waiting at time
            while (remainingPassengers.Count >= 1 && remainingPassengers[0].startWaitingAt <= time) 
            {
                elevator.addFloorToQueue(remainingPassengers[0].atFloor); // Note: the elevator is not aware of the passenger's goingToFloor until the passenger has entered the elevator
                if (Math.Abs(remainingPassengers[0].atFloor - elevator.currentFloor) > 0.0001)
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

        public void increment(int timeStep = 1) 
        {
            time += timeStep; 

            this.elevator.increment(timeStep);

            movingFromWaitingPassengers();
            movingFromRemainingPassengers();
        }
    }
}
