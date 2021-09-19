using System;

namespace PassengerNS
{
    /// <summary> 
    /// Passenger class for passengers of the elevator.
    /// Here, we assume that 
    /// (1) The passenger is infinitely patient, and would always be waiting at the "atFloor"
    /// (2) The passenger never changes his mind, and would always go to "goingToFloor"
    /// </summary>
    public class Passenger
    {
        public readonly int id;
        public readonly int atFloor;
        public readonly int goingToFloor;
        public readonly int startWaitingAt;
        
        /// <summary> 
        /// Instantiation of a Passenger object
        /// </summary>
        public Passenger (int personID, int atFloor, int goingToFloor, int time)
        {
            this.id = personID;
            this.atFloor = atFloor;
            this.goingToFloor = goingToFloor;
            this.startWaitingAt = time; 
        }
    }
}
