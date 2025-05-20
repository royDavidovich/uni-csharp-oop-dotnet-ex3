using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class GarageUIManager
    {
        private GarageManager m_GarageManager = new GarageManager();
        public bool UserDecidedToExit { get; set; }

        private const int k_CarWheels = 5;
        private const int k_MotorcycleWheels = 2;
        private const int k_TruckWheels = 12;

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
                    handleUserChoice(choice);
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

            string userInput = Console.ReadLine();

            if (Enum.TryParse(userInput, out eUserOptions option) && Enum.IsDefined(typeof(eUserOptions), option))
            {
                return option;
            }
            else
            {
                throw new ArgumentException("Invalid option selected. Please enter a number between 1 and 9.");
            }
        }

        private void greetUser()
        {
            Console.WriteLine("Hello, and welcome to Roy & Guy Garage!");
        }

        private void sayGoodbyeToUser()
        {
            Console.WriteLine("Thank you for using our Garage, have a good day.");
        }

        private void handleUserChoice(eUserOptions i_Choice)
        {
            switch (i_Choice)
            {
                case eUserOptions.LoadVehiclesFromDb:
                    loadVehiclesFromDatabase();
                    break;

                case eUserOptions.InsertNewVehicle:
                    insertNewVehicle();
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

        private void loadVehiclesFromDatabase()
        {
            try
            {
                m_GarageManager.LoadVehiclesFromDb("C:\\Users\\guyfi\\source\\repos\\uni-csharp-oop-dotnet-ex3\\Ex03 Guy 322372681 Roy 322718388\\Vehicles.db");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void insertNewVehicle()
        {
            string[] vehicleData = collectDbFormattedVehicleData(out List<string> wheelData);
            string vehicleDataStr = string.Join(",", vehicleData);

            try
            {
                m_GarageManager.LoadVehiclesFromUser(vehicleDataStr, wheelData);
                Console.WriteLine("Vehicle added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private string[] collectDbFormattedVehicleData(out List<string> o_Galgalim)
        {
            string[] vehicleData = new string[10];
            o_Galgalim = null;

            string vehicleType = askForVehicleType();
            vehicleData[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType] = vehicleType;

            collectBasicVehicleInfo(vehicleData);

            int wheelCount = getWheelCountForType(vehicleType);
            collectWheelInfo(vehicleData, out o_Galgalim, wheelCount);

            collectTypeSpecificData(vehicleData, vehicleType);

            return vehicleData;
        }

        private string askForVehicleType()
        {
            Console.WriteLine("Enter vehicle type:");

            foreach (string type in VehicleCreator.SupportedTypes)
            {
                Console.WriteLine("- " + type);
            }

            string inputType = Console.ReadLine();

            foreach (string type in VehicleCreator.SupportedTypes)
            {
                if (string.Equals(type, inputType, StringComparison.OrdinalIgnoreCase))
                {
                    return type;
                }
            }

            throw new ArgumentException("Invalid vehicle type.");
        }

        private void collectBasicVehicleInfo(string[] io_VehicleData)
        {
            Console.Write("Enter license plate: ");
            io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate] = Console.ReadLine();
            Console.Write("Enter model name: ");
            io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.ModelName] = Console.ReadLine();
            Console.Write("Enter energy percentage (0-100): ");
            io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.EnergyPercentage] = Console.ReadLine();
            Console.Write("Enter owner name: ");
            io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.OwnerName] = Console.ReadLine();
            Console.Write("Enter owner phone: ");
            io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.OwnerPhone] = Console.ReadLine();
        }

        private void collectWheelInfo(string[] io_VehicleData, out List<string> o_WheelData, int i_NumOfWheels)
        {
            o_WheelData = null;
            Console.Write("Do you want to enter the same wheel info for all wheels? (yes/no): ");
            string answer = Console.ReadLine()?.Trim().ToLower();

            if (answer == "yes")
            {
                Console.Write("Enter wheel manufacturer: ");
                io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.TierModel] = Console.ReadLine();
                Console.Write("Enter current air pressure: ");
                io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.CurrAirPressure] = Console.ReadLine();
            }
            else
            {
                io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.TierModel] = "0";
                io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.CurrAirPressure] = "0";

                o_WheelData = new List<string>();

                for (int i = 0; i < i_NumOfWheels; i++)
                {
                    Console.WriteLine($"Wheel {i + 1}:");
                    Console.Write("Enter manufacturer: ");
                    o_WheelData.Add(Console.ReadLine());
                    Console.Write("Enter current air pressure: ");
                    o_WheelData.Add(Console.ReadLine());
                }
            }
        }

        private int getWheelCountForType(string i_Type)
        {
            switch (i_Type)
            {
                case "Truck":
                    return k_TruckWheels;
                case "FuelCar":
                case "ElectricCar":
                    return k_CarWheels;
                case "FuelMotorcycle":
                case "ElectricMotorcycle":
                    return k_MotorcycleWheels;
                default:
                    return 0;
            }
        }

        private void collectTypeSpecificData(string[] io_VehicleData, string i_Type)
        {
            if (i_Type == "FuelCar" || i_Type == "ElectricCar")
            {
                Console.Write("Enter car color (Yellow, Black, White, Silver): ");
                io_VehicleData[8] = Console.ReadLine();
                Console.Write("Enter number of doors (2–5): ");
                io_VehicleData[9] = Console.ReadLine();
            }
            else if (i_Type == "FuelMotorcycle" || i_Type == "ElectricMotorcycle")
            {
                Console.Write("Enter permit type (A, A2, AB, B2): ");
                io_VehicleData[8] = Console.ReadLine();
                Console.Write("Enter engine volume: ");
                io_VehicleData[9] = Console.ReadLine();
            }
            else if (i_Type == "Truck")
            {
                Console.Write("Is hazardous cargo loaded? (true/false): ");
                io_VehicleData[8] = Console.ReadLine();
                Console.Write("Enter cargo volume: ");
                io_VehicleData[9] = Console.ReadLine();
            }
        }
    }
}
