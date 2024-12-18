using static System.Runtime.InteropServices.JavaScript.JSType;

namespace secondhand_car_app
{
    internal class Program
    {

        // declare file path variables
        const string csvFile = @"C:\Users\tulio\OneDrive\Área de Trabalho\secondHandCars.csv";
        const string txtFile = @"C:\Users\tulio\OneDrive\Área de Trabalho\reportSummary.txt";

        // declare arrays based on csv file headers
        static string[] registerPlates;
        static string[] carMakers;
        static string[] models;
        static double[] prices;
        static double[] mileages;

        static void Main(string[] args)
        {
            string[] csvFileLines = ReadCsvFile();
            int totalCars = csvFileLines.Length - 1;

            SizeArrays(totalCars);
            PopulateArrays(csvFileLines);
            DisplayReport();
        }

        static string[] ReadCsvFile()
        {
            try
            {
                string[] csvFileLines = File.ReadAllLines(csvFile);
                //Console.WriteLine("STATUS: FILE READ SUCCESSFULLY!!");
                return csvFileLines;
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR MESSAGE: {error.Message}");
                return null;
            }
        }

        static void WriteTxtFile(string[] reportLines)
        {
            try
            {
                File.WriteAllLines(txtFile, reportLines);
                Console.WriteLine("\n\tSTATUS: REPORT TXT FILE CREATED SUCCESSFULLY!!");
            }
            catch (Exception error)
            {
                Console.WriteLine($"ERROR MESSAGE: {error.Message}");
            }
        }

        static void SizeArrays(int totalCars)
        {
            registerPlates = new string[totalCars];
            carMakers = new string[totalCars];
            models = new string[totalCars];
            prices = new double[totalCars];
            mileages = new double[totalCars];
        }

        static void PopulateArrays(string[] csvFileLines)
        {
            for (int i = 1; i < csvFileLines.Length; i++)
            {
                //Console.WriteLine(csvFileLines[i]); // for testing
                string csvLine = csvFileLines[i];
                string[] csvDataLines = csvLine.Split(",");

                string registerPlate = csvDataLines[0];
                string carMake = csvDataLines[1];
                string model = csvDataLines[2];
                double price = Convert.ToDouble(csvDataLines[3]);
                double mileage = Convert.ToDouble(csvDataLines[4]);

                registerPlates[i - 1] = registerPlate;
                carMakers[i - 1] = carMake;
                models[i - 1] = model;
                prices[i - 1] = price;
                mileages[i - 1] = mileage;
            }
        }

        static void DisplayReport()
        {
            int totalCars = ReadCsvFile().Length - 1;
            double totalCarPrice = CalculateTotalCarPrice();
            double averageCarPrice = CalculateTotalCarPrice() / totalCars;
            int mostExpensiveCar = IndexMostExpensiveCar();
            int cheapestCar = IndexCheapestCar();
            double averageCarMileage = CalculateTotalCarMileage() / totalCars;

            string[] reportLines = new string[7];

            reportLines[0] = "******************** REPORT SUMMARY ********************";
            reportLines[1] = "\n\tThe total number of cars is: {} cars.";
            reportLines[2] = "\tThe total car price is: {} EUR.";
            reportLines[3] = "\tThe average car price is: {} EUR.";
            reportLines[4] = "\tMost expensive car info: {}.";
            reportLines[5] = "\tCheapest car info: {}.";
            reportLines[6] = "\tThe average car mileage: {} KMs.";

            for (int i = 0; i < reportLines.Length; i++) 
            {
                Console.WriteLine(reportLines[i]);
            }

            WriteTxtFile(reportLines);
        }

        static double CalculateTotalCarPrice()
        {
            // continue here
        }

        static int IndexMostExpensiveCar()
        {
            return 1;
        }

        static int IndexCheapestCar()
        {
            return 1;
        }

        static double CalculateTotalCarMileage()
        {
            return 1.1;
        }
    }
}
