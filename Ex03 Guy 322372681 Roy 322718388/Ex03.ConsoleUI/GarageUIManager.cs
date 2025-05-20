using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ex03.ConsoleUI
{
    public class GarageUIManager
    {
        private GarageManager m_GarageManager = new GarageManager();
        public bool UserDecidedToExit { get; set; }

        private enum eUserOptions
        {
            LoadVehiclesFromDb = 1,
            InsertNewVehicle,
            ShowLicensePlates,
            ChangeVehicleStatus,
            InflateTiresToMax,
            RefuelFuelVehicle,
            RechargeElectricVehicle,
            ShowFullVehicleData,
            Exit
        }

        public void Run()
        {
            greetUser();
            while (!UserDecidedToExit)
            {
                try
                {
                    eUserOptions choice = askForOption();
                    handleChoice(choice);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            sayGoodbyeToUser();
        }

        private eUserOptions askForOption()
        {
            Console.WriteLine(@"Please choose your option:
1. Load the Vehicles from our DataBase.
2. Insert a new vehicle to our Garage.
3. Show all vheicles license plates.
4. Change the status of a vehicle.
5. Inflate tires.
6. Refuel a fuelable vehicle.
7. Recharge an electric vehicle.
8. Show all the data for a specific vehicle.
9. Exit our Garage.");

            string input = Console.ReadLine();

            if (Enum.TryParse(input, out eUserOptions option) && Enum.IsDefined(typeof(eUserOptions), option))
            {
                return option;
            }
            else
            {
                throw new ArgumentException("Invalid option selected. Please enter a number between 1 and 9.");
            }
        }

        private void greetUser() => Console.WriteLine("Hello, and welcome to Roy & Guy Garage!");
        private void sayGoodbyeToUser() => Console.WriteLine("Thank you for using our Garage, have a good day.");

        private void handleChoice(eUserOptions i_choice)
        {
            switch (i_choice)
            {
                case eUserOptions.LoadVehiclesFromDb:
                    try
                    {
                        m_GarageManager.LoadVehiclesFromDb("C:\\Users\\guyfi\\source\\repos\\uni-csharp-oop-dotnet-ex3\\Ex03 Guy 322372681 Roy 322718388\\Vehicles.db");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;

                case eUserOptions.InsertNewVehicle:
                    InsertNewVehicle();
                    break;

                case eUserOptions.ShowLicensePlates:
                case eUserOptions.ChangeVehicleStatus:
                case eUserOptions.InflateTiresToMax:
                case eUserOptions.RefuelFuelVehicle:
                case eUserOptions.RechargeElectricVehicle:
                case eUserOptions.ShowFullVehicleData:
                    Console.WriteLine("This feature is under construction.");
                    break;

                case eUserOptions.Exit:
                    UserDecidedToExit = true;
                    break;
            }
        }

        private void InsertNewVehicle()
        {
            string[] data = collectDbFormattedVehicleData();

            try
            {
                string vehicleType = data[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType];
                string licensePlate = data[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate];
                string modelName = data[(int)Vehicle.eGeneralDataIndicesInFile.ModelName];

                Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licensePlate, modelName);
                vehicle.InitVehicleInformation(data);
                

                Console.WriteLine("Vehicle added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private string[] collectDbFormattedVehicleData()
        {
            string[] data = new string[10];

            Console.WriteLine("Enter vehicle type:");
            foreach (string type in VehicleCreator.SupportedTypes)
            {
                Console.WriteLine("- " + type);
            }

            string inputType = Console.ReadLine();
            string normalizedType = null;
            foreach (string type in VehicleCreator.SupportedTypes)
            {
                if (string.Equals(type, inputType, StringComparison.OrdinalIgnoreCase))
                {
                    normalizedType = type;
                    break;
                }
            }

            if (normalizedType == null)
            {
                throw new ArgumentException("Invalid vehicle type.");
            }

            data[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType] = normalizedType;

            Console.Write("Enter license plate: ");
            data[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate] = Console.ReadLine();
            Console.Write("Enter model name: ");
            data[(int)Vehicle.eGeneralDataIndicesInFile.ModelName] = Console.ReadLine();
            Console.Write("Enter energy percentage (0-100): ");
            data[(int)Vehicle.eGeneralDataIndicesInFile.EnergyPercentage] = Console.ReadLine();
            Console.Write("Enter owner name: ");
            data[(int)Vehicle.eGeneralDataIndicesInFile.OwnerName] = Console.ReadLine();
            Console.Write("Enter owner phone: ");
            data[(int)Vehicle.eGeneralDataIndicesInFile.OwnerPhone] = Console.ReadLine();

            Console.Write("Do you want to enter the same wheel info for all wheels? (yes/no): ");
            string answer = Console.ReadLine()?.ToLower();

            if (answer == "yes")
            {
                Console.Write("Enter wheel manufacturer: ");
                data[(int)Vehicle.eGeneralDataIndicesInFile.TierModel] = Console.ReadLine();

                Console.Write("Enter current air pressure: ");
                data[(int)Vehicle.eGeneralDataIndicesInFile.CurrAirPressure] = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Note: Our system only supports one wheel data for DB-style input.");
                Console.WriteLine("We'll collect the first wheel info as representative.");
                Console.Write("Enter wheel manufacturer for first wheel: ");
                data[(int)Vehicle.eGeneralDataIndicesInFile.TierModel] = Console.ReadLine();
                Console.Write("Enter air pressure for first wheel: ");
                data[(int)Vehicle.eGeneralDataIndicesInFile.CurrAirPressure] = Console.ReadLine();
            }

            if (normalizedType == "FuelCar" || normalizedType == "ElectricCar")
            {
                Console.Write("Enter car color (Yellow, Black, White, Silver): ");
                data[8] = Console.ReadLine();
                Console.Write("Enter number of doors (2–5): ");
                data[9] = Console.ReadLine();
            }
            else if (normalizedType == "FuelMotorcycle" || normalizedType == "ElectricMotorcycle")
            {
                Console.Write("Enter permit type (A, A2, AB, B2): ");
                data[8] = Console.ReadLine();
                Console.Write("Enter engine volume: ");
                data[9] = Console.ReadLine();
            }
            else if (normalizedType == "Truck")
            {
                Console.Write("Is hazardous cargo loaded? (true/false): ");
                data[8] = Console.ReadLine();
                Console.Write("Enter cargo volume: ");
                data[9] = Console.ReadLine();
            }

            return data;
        }
    }
}
