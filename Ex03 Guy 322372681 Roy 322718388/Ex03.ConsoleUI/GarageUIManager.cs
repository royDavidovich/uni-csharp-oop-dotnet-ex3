using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class GarageUIManager
    {
        private const string k_DBFilePath = "Vehicles.db";
        private const string k_ProvidePlateNumberMsg = "Please provide your wanted vehicle plate number: ";
        private readonly GarageManager r_GarageManager = new GarageManager();
        public bool UserDecidedToExit { get; set; }

        private const int k_CarWheels = 5;
        private const int k_MotorcycleWheels = 2;
        private const int k_TruckWheels = 12;
        private const int k_FirstSpecialIndexInData = 8;
        private const int k_SecondSpecialIndexInData = 9;

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
            Console.WriteLine("Hello, and welcome to \"Roy & Guy\'s\" Garage!");

            while (!UserDecidedToExit)
            {
                try
                {
                    eUserOptions choice = getUserChoiceOfAction();
                    handleUserChoice(choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Thank you for using our Garage, have a good day.");
        }

        private eUserOptions getUserChoiceOfAction()
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

            eUserOptions userOption;
            bool isChoiceGood;

            do
            {
                string userInput = Console.ReadLine();

                if (Enum.TryParse(userInput, out userOption) && Enum.IsDefined(typeof(eUserOptions), userOption))
                {
                    isChoiceGood = true;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter a number between 1 and 9.");
                    isChoiceGood = false;
                }

            } while (!isChoiceGood);

            return userOption;
        }


        private void handleUserChoice(eUserOptions i_Choice) //SPACES BETWEEN CASES?
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
                    showLicensePlates();
                    break;
                case eUserOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eUserOptions.InflateTiresToMax: //TODO 
                    inflateTiresToMax();
                    break;
                case eUserOptions.RefuelFuelVehicle: //TODO
                    refuelVehicle();
                    break;
                case eUserOptions.RechargeElectricVehicle: //TODO 
                    reChargeElectricVehicle();
                    break;
                case eUserOptions.ShowFullVehicleData:
                    Console.WriteLine("This feature is under construction."); //TODO LONG!
                    break;
                case eUserOptions.Exit:
                    UserDecidedToExit = true;
                    break;
            }
        }

        private void inflateTiresToMax()
        {
            string userProvidedPlateNumber;
            Console.WriteLine(k_ProvidePlateNumberMsg);
            userProvidedPlateNumber = Console.ReadLine();

            //TODO METHOD IN GARAGE
        }

        private void showLicensePlates()
        {
            string userChoice;
            string stateFilter = null;

            Console.WriteLine("Would you like to filter the license plates by vehicle state? (yes/no): ");
            userChoice = Console.ReadLine()?.Trim().ToLower();

            if (userChoice == "yes")
            {
                Console.WriteLine("Please enter the state to filter by (InRepair, Repaired, Paid): ");
                stateFilter = Console.ReadLine();
            }

            try
            {
                List<string> licensePlates = r_GarageManager.GetVehiclesInGarageLicensePlates(stateFilter);

                if (licensePlates.Count == 0)
                {
                    Console.WriteLine("No vehicles found with the specified filter.");
                }
                else
                {
                    Console.WriteLine("License plates in the garage:");
                    foreach (string plate in licensePlates)
                    {
                        Console.WriteLine($"- {plate}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void reChargeElectricVehicle()
        {
            string userProvidedPlateNumber;
            string userProvidedAmountToChargeInMinutes;

            bool vehicleIsChargeable;
            do
            {

                Console.WriteLine(k_ProvidePlateNumberMsg);
                try
                {
                    userProvidedPlateNumber = Console.ReadLine();
                    vehicleIsChargeable = r_GarageManager.IsRefuelable(userProvidedPlateNumber) ? true : false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    vehicleIsChargeable = false;
                }
            }
            while (!vehicleIsChargeable);

            Console.WriteLine("Please provide your amount of minutes to be charge into the vehicle: ");
            userProvidedAmountToChargeInMinutes = Console.ReadLine();

            //TODO RECHARGE METHOD IN GARAGE
        }

        private void refuelVehicle()
        {
            string userProvidedPlateNumber;
            string userProvidedFuelType;
            string userProvidedAmountToFuel;
            bool vehicleIsRefuelable;
            do
            {
                Console.WriteLine(k_ProvidePlateNumberMsg);
                userProvidedPlateNumber = Console.ReadLine();
                vehicleIsRefuelable = r_GarageManager.IsRefuelable(userProvidedPlateNumber) ? true : false;
            }
            while (!vehicleIsRefuelable);

            Console.WriteLine(@"Please provide your fuel type:
Octan 95
Octan 96
Octan 98
Soler");
            userProvidedFuelType = Console.ReadLine();
            Console.WriteLine(@"Please provide your chosen amount to be fueled: ");
            userProvidedAmountToFuel = Console.ReadLine();
            try
            {
                r_GarageManager.RefuelVehicle(userProvidedPlateNumber, userProvidedAmountToFuel, userProvidedFuelType);
                Console.WriteLine("SUCCESS"); //TODO PROVIDE MORE DETAILS LATER
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }   
        }

        private void changeVehicleStatus() 
        {
            string providedPlateNumber;
            string providedNewState;

            Console.WriteLine(k_ProvidePlateNumberMsg);
            providedPlateNumber = Console.ReadLine();
            Console.WriteLine(@"Please provide your wanted state - 
in repair
repaired
paid");
            providedNewState = Console.ReadLine();
            try
            {
              //  r_GarageManager.UpdateVehicleStateInGarage(providedPlateNumber, providedNewState);
                Console.WriteLine("Vehicle number {0} state was changed to {1} state!", providedPlateNumber, providedNewState);
            }
            catch (Exception e)
            { 
                Console.WriteLine(e); 
            }
            
            Console.WriteLine();
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

            if(VehicleCreator.SupportedTypes.Contains(inputType))
            {
                return inputType;
            }
            else
            {
                throw new ArgumentException("Invalid vehicle type.");
            }
               
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

        private int getWheelCountForType(string i_Type, string i_LicensePlate = "temp", string i_ModelName = "temp")
        {
            Vehicle tempVehicle = VehicleCreator.CreateVehicle(i_Type, i_LicensePlate, i_ModelName);
            
            return tempVehicle == null ? 0 : tempVehicle.GetNumberOfWheels();
        }

        private void collectTypeSpecificData(string[] io_VehicleData, string i_Type)
        {
            if (i_Type == "FuelCar" || i_Type == "ElectricCar")
            {
                Console.Write("Enter car color (Yellow, Black, White, Silver): ");
                io_VehicleData[k_FirstSpecialIndexInData] = Console.ReadLine();
                Console.Write("Enter number of doors (2–5): ");
                io_VehicleData[k_SecondSpecialIndexInData] = Console.ReadLine();
            }
            else if (i_Type == "FuelMotorcycle" || i_Type == "ElectricMotorcycle")
            {
                Console.Write("Enter permit type (A, A2, AB, B2): ");
                io_VehicleData[k_FirstSpecialIndexInData] = Console.ReadLine();
                Console.Write("Enter engine volume: ");
                io_VehicleData[k_SecondSpecialIndexInData] = Console.ReadLine();
            }
            else if (i_Type == "Truck")
            {
                Console.Write("Is hazardous cargo loaded? (true/false): ");
                io_VehicleData[k_FirstSpecialIndexInData] = Console.ReadLine();
                Console.Write("Enter cargo volume: ");
                io_VehicleData[k_SecondSpecialIndexInData] = Console.ReadLine();
            }
        }
    }
}
