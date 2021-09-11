using Microsoft.VisualBasic.FileIO;
using System;
using PassengerNS;
using System.Collections.Generic;

namespace CSVFileNS
{
    public class CSVFile
    {
        public static List<Passenger> readPassengersCSV()
        {
            var passengers = new List<Passenger>(); 
            var path = @"/Users/thienhuongvu/elevator/data.csv"; // Person ID,At Floor,Going to Floor,Time
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields(); // Person ID,At Floor,Going to Floor,Time
                    int personID = Int32.Parse(fields[0]);
                    int atFloor = Int32.Parse(fields[1]);
                    int goingToFloor = Int32.Parse(fields[2]);
                    int time = Int32.Parse(fields[3]);
                    passengers.Add(new Passenger(personID, atFloor, goingToFloor, time));
                    // Console.WriteLine(personID.ToString() + " " + atFloor.ToString() + " " + goingToFloor.ToString() + " " + time.ToString());
                }
            }

            return passengers;
        }
    }
}