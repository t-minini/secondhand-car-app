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
        static int[] prices;
        static int[] mileages;

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
            prices = new int[totalCars];
            mileages = new int[totalCars];
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
                int price = Convert.ToInt32(csvDataLines[3]);
                int mileage = Convert.ToInt32(csvDataLines[4]);

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
            int mostExpensiveCarIndex = IndexMostExpensiveCar();
            int cheapestCarIndex = IndexCheapestCar();
            int averageCarMileage = CalculateTotalCarMileage() / totalCars;

            string[] reportLines = new string[7];

            reportLines[0] = "******************** REPORT SUMMARY ********************";
            reportLines[1] = $"\n\tTotal number of cars: {totalCars} cars.";
            reportLines[2] = $"\tTotal car price: {totalCarPrice.ToString("N0").Replace(",", ".")} EUR.";
            reportLines[3] = $"\tAverage car price: {averageCarPrice.ToString("N0").Replace(",", ".")} EUR.";
            reportLines[4] =
                $"\tMost expensive car info:" +
                $"\n\t\t\tReg. No.: {registerPlates[mostExpensiveCarIndex]}" +
                $"\n\t\t\tCar Make: {carMakers[mostExpensiveCarIndex]}" +
                $"\n\t\t\tCar Model: {models[mostExpensiveCarIndex]}" +
                $"\n\t\t\tPrice: {prices[mostExpensiveCarIndex].ToString("N0").Replace(",",".")} EUR" +
                $"\n\t\t\tMileage: {mileages[mostExpensiveCarIndex].ToString("N0").Replace(",",".")} KMs";
            reportLines[5] =
               $"\tCheapest car info:" +
               $"\n\t\t\tReg. No.: {registerPlates[cheapestCarIndex]}" +
               $"\n\t\t\tCar Make: {carMakers[cheapestCarIndex]}" +
               $"\n\t\t\tCar Model: {models[cheapestCarIndex]}" +
               $"\n\t\t\tPrice: {prices[cheapestCarIndex].ToString("N0").Replace(",", ".")} EUR" +
               $"\n\t\t\tMileage: {mileages[cheapestCarIndex].ToString("N0").Replace(",", ".")} KMs";
            reportLines[6] = $"\tAverage car mileage: {averageCarMileage.ToString("N0").Replace(",",".")} KMs.";

            for (int i = 0; i < reportLines.Length; i++) 
            {
                Console.WriteLine(reportLines[i]);
            }

            WriteTxtFile(reportLines);
        }

        static int CalculateTotalCarPrice()
        {
            int totalPrice = 0;
            for (int i = 0; i < prices.Length; i++) 
            { 
                totalPrice += prices[i];
            }
            return totalPrice;
        }

        static int IndexMostExpensiveCar()
        {
            int indexMostExpensive = 0;
            int mostExpensive = prices[0];
            for (int i = 1; i < prices.Length; i++) 
            {
                if (prices[i] > mostExpensive) 
                {
                    mostExpensive = prices[i];
                    indexMostExpensive = i;
                }
            }
            return indexMostExpensive;
        }

        static int IndexCheapestCar()
        {
            int indexCheapest = 0;
            int cheapest = prices[0];
            for (int i = 1; i < prices.Length; i++) 
            {
                if (prices[i] < cheapest)
                {
                    cheapest = prices[i];
                    indexCheapest = i;
                }
            }
            return indexCheapest;
        }

        static int CalculateTotalCarMileage()
        {
            int totalMileage = 0;
            for (int i = 0; i < mileages.Length; i++)
            {
                totalMileage = totalMileage + mileages[i];
            }
            return totalMileage;
        }
    }
}
