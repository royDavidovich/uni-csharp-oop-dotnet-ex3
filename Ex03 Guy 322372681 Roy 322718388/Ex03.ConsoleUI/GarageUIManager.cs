using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class GarageUIManager
    {
        private const string k_DBFilePath = "Vehicles.db";
        private readonly GarageManager r_GarageManager = new GarageManager();
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
                    eUserOptions choice = getUserChoceOfAction();
                    handleUserChoice(choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            sayGoodbyeToUser();
        }

        private eUserOptions getUserChoceOfAction()
        {
            Console.WriteLine(@"Please choose your option:
1. Load the Vehicles from our DataBase.
2. Insert a new vehicle to our Garage.
3. Show all vehicles license plates.
4. Change the status of a vehicle.
5. Inflate tires.
6. Refuel a fuel-able vehicle.
7. Recharge an electric vehicle.
8. Show all the data for a specific vehicle.
9. Exit our Garage.");

            string userInput = Console.ReadLine();

            if (Enum.TryParse(userInput, out eUserOptions userOption) && Enum.IsDefined(typeof(eUserOptions), userOption))
            {
                return userOption;
            }
            else
            {
                throw new ArgumentException("Invalid option selected. Please enter a number between 1 and 9.");
            }
        }

        private void greetUser()
        {
            Console.WriteLine("Hello, and welcome to \"Roy & Guy\'s\" Garage!");
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
                r_GarageManager.LoadVehiclesFromDB(k_DBFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void insertNewVehicle()
        {
            string[] vehicleData = collectFormattedVehicleData(out List<string> wheelData);
            string vehicleDataStr = string.Join(",", vehicleData);

            try
            {
                r_GarageManager.LoadVehiclesFromUser(vehicleDataStr, wheelData);
                Console.WriteLine("Vehicle added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private string[] collectFormattedVehicleData(out List<string> o_Galgalim)
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

            //can use contains on VehicleCreator.SupportedTypes
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
            int wheelsCount;
            switch (i_Type)
            {
                case "Truck":
                    wheelsCount = k_TruckWheels;
                    break;
                case "FuelCar":
                    wheelsCount = k_CarWheels;
                    break;
                case "ElectricCar":
                    wheelsCount = k_CarWheels;
                    break;
                case "FuelMotorcycle":
                    wheelsCount = k_MotorcycleWheels;
                    break;
                case "ElectricMotorcycle":
                    wheelsCount = k_MotorcycleWheels;
                    break;
                default:
                    wheelsCount = 0;
                    break;
            }
            
            return wheelsCount;
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
