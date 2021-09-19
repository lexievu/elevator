using Microsoft.VisualBasic.FileIO;
using System;
using PassengerNS;
using System.Collections.Generic;
using System.IO;

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

        public static string WriteResults(int currentTime, List<int> peopleInLift, double currentFloor, List<int> floorQueue, string filePath = "/Users/thienhuongvu/Projects/elevator/Elevator/output.csv") 
        {
            if (!File.Exists(filePath)) {
                // Create a file to write to
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Current Time,People In Lift,Current Floor,Floor Queue"); 
                }
            }
            string textOutput = currentTime.ToString() + "," + String.Join(";", peopleInLift) + "," + string.Format("{0:N2}", currentFloor) + "," + String.Join(";", floorQueue) + "\n"; 
            File.AppendAllText(filePath, textOutput);

            return filePath;
        }
    }
}