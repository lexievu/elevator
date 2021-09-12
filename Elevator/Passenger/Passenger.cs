using System;

namespace PassengerNS
{
    public class Passenger
    {
        public readonly int id;
        public readonly int atFloor;
        public readonly int goingToFloor;
        public readonly int startWaitingAt;

        public Passenger (int personID, int atFloor, int goingToFloor, int time)
        {
            this.id = personID;
            this.atFloor = atFloor;
            this.goingToFloor = goingToFloor;
            this.startWaitingAt = time; 
        }
    }
}
