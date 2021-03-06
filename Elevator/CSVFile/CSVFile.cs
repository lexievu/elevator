using Microsoft.VisualBasic.FileIO;
using System;
using PassengerNS;
using System.Collections.Generic;
using System.IO;

namespace CSVFileNS
{
    /// <summary>
    /// The CSVFile class focuses on reading the passengers' information from a csv file and writing the results to a output.csv file.
    /// </summary>
    public class CSVFile
    {
        /// <summary>
        /// This function reads from the input file with the passengers information. 
        /// It returns a list of all passengers listed in the input csv file.
        /// </summary>
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
                }
            }

            return passengers;
        }

        /// <summary>
        /// Given the current time, the list of IDs of the passengers in lift, the current floor and the floorqueue, 
        /// this function writes the result as a new line in the output.csv file
        /// </summary>
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