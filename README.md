# **Simple Elevator Prototype**

## **Background & Motivation**

An office building has 10 floors and one simple elevator. The people working there are distributed evenly across all floors. The lift has only one external button on each floor. Inside the lift, there is a set of buttons to send the lift to a desired floor. The lift has a maximum capacity of 8 people.

## **Solutions**

### Elevator algorithm

The elevator algorithm has two rules: 

- As long as there is someone inside who wants to keep going in that direction or ahead of the elevator, keep heading in that direction.
- Once the elevator has exhausted the requests in its current direction, switch directions if there's a request in the other direction. Otherwise, stop and wait for a call.

Following the elevator algorithm, the maximum wait time for a passenger would be 90 seconds (10 floors, 10 seconds to travel between adjacent floors). The maximum travel time would be 170 seconds (passengers wants to go from floor 9 to floor 10, and picked up while the elevator is going down). The elevator algorithm ensures that all passengers are served within a reasonable period of time.   

### **Assumptions made**

**Passengers:** 

- Passengers are infinitely patient, and would always wait at the floor until picked up by the elevator.
- Passengers never change their minds about their destination floor, and would always go to the desired floor.
- All passenger list passed in is sorted by start waiting at time. This would be the case in for a real elevator, as time flows in only one way.

**Elevators:** 

- It has no wait time between each floor, even when picking a passenger up (i.e. the time it takes for passenger to enter the lift is much smaller than the time taken for the lift to travel between floor).

### **Technical Details**

The solutions contain 4 classes: 

- CSVFile: reading the passengers' information from a csv file and writing the results to an output.csv file.
- Elevator: encapsulating the behaviour of an elevator in our prototype.
- Passenger: encapsulating the behaviour of a passenger.
- Simulation: encapsulating the information and functionalities needed for the prototype.

The Main() method can be found in the Simulation.cs file. 

## **Results**

The result of the simulation is saved in "output.csv". According to the algorithm presented here under the listed assumptions, the elevator takes 665 seconds to respond to all the calls. 

The "output.csv" file has 4 columns: 

- Current Time: the current time, in seconds. The simulation starts at time = 1.
- People In Lift: the list of IDs of the people currently in the lift.
- Current Floor: the current position of the lift.
- Floor Queue: the list of floor in order that the lift would have to go to.

## **Future Enhancements**

**Measure the performance of the elevator**

- Passenger class to record the customer journey time (time from when the passenger enters the elevator to when the passenger leaves the elevator at the desired floor)
- Passenger class to record the customer wait time (time from when the passenger starts waiting to when the elevator picks the passenger up)

**Enhance the prototype to make it more realistic** 

- Add the time elevator wait between floors
- Run multiple elevators per building
- Allow passengers to specify which direction (up or down) they would like to travel in