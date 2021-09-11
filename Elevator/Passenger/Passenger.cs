using System;

namespace PassengerNS
{
    public class Passenger
    {
        public readonly int id;
        public int atFloor;
        public int goingToFloor;
        public int startWaitingAt;

        public Passenger (int personID, int atFloor, int goingToFloor, int time)
        {
            this.id = personID;
            this.atFloor = atFloor;
            this.goingToFloor = goingToFloor;
            this.startWaitingAt = time; 
        }
    }
}
