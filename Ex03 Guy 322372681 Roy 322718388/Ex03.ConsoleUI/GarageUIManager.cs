using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Ex03.ConsoleUI
{
    public interface IUITypeDataCollector
    {
        void CollectData(string[] io_VehicleData);
    }

    public class GarageUIManager
    {
        public enum eFuelTypeUI
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public enum eCarColorUI
        {
            Yellow,
            Black,
            White,
            Silver
        }

        public enum ePermitTypeUI
        {
            A,
            A2,
            AB,
            B2
        }

        private const string k_DBFilePath = "Vehicles.db";
        private const string k_ProvidePlateNumberMsg = "Please provide your wanted vehicle plate's number: ";
        private readonly GarageManager r_GarageManager = new GarageManager();

        // A dictionary used to delegate vehicle-specific data collection, promoting polymorphic behavior and separation of concerns.
        private readonly Dictionary<string, IUITypeDataCollector> r_DataCollectors =
            new Dictionary<string, IUITypeDataCollector>
                {
                    { "FuelCar", new CarUIDataCollector() },
                    { "ElectricCar", new CarUIDataCollector() },
                    { "FuelMotorcycle", new MotorcycleUIDataCollector() },
                    { "ElectricMotorcycle", new MotorcycleUIDataCollector() },
                    { "Truck", new TruckUIDataCollector() }
                };

        public bool UserDecidedToExit { get; set; }
        public const int k_FirstSpecialIndexInData = 8;
        public const int k_SecondSpecialIndexInData = 9;

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
            Console.WriteLine(@"Hello, and welcome to Roy & Guy's Garage!
");

            while (!UserDecidedToExit)
            {
                try
                {
                    eUserOptions userChoice = getUserChoiceOfAction(); 
                    handleUserChoice(userChoice);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($@"Error: {ex.Message}
");
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
5. Inflate tires (to max air pressure).
6. Refuel a fuel vehicle.
7. Recharge an electric vehicle.
8. Show all the data for a specific vehicle.
9. Exit our Garage.
");

            bool isValidChoice = false;
            eUserOptions parsedOption = eUserOptions.Exit;

            while (!isValidChoice)
            {
                string inputOption = Console.ReadLine();
                InputValidator.ValidateNonEmptyString(inputOption, "Menu option"); 

                if (Enum.TryParse(inputOption, out parsedOption) && Enum.IsDefined(typeof(eUserOptions), parsedOption))
                {
                    isValidChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter a number between 1 and 9.");
                }
            }

            return parsedOption;
        }

        private void handleUserChoice(eUserOptions i_UserChoice)
        {
            switch (i_UserChoice)
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
                case eUserOptions.InflateTiresToMax:
                    inflateTiresToMax();
                    break;
                case eUserOptions.RefuelFuelVehicle:
                    refuelVehicle();
                    break;
                case eUserOptions.RechargeElectricVehicle:
                    rechargeElectricVehicle();
                    break;
                case eUserOptions.ShowFullVehicleData:
                    getAndShowFullVehicleData();
                    break;
                case eUserOptions.Exit:
                    UserDecidedToExit = true;
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice between 1-9.");
                    Console.WriteLine();
                    break;
            }
        }

        private void inflateTiresToMax()
        {
            bool isDone = false;
            string exitCode = ((int)eUserOptions.Exit).ToString();

            while (!isDone)
            {
                Console.WriteLine($"{k_ProvidePlateNumberMsg} (or press {exitCode} to cancel)");
                string licensePlateInput = Console.ReadLine()?.Trim();

                if (licensePlateInput == exitCode)
                {
                    Console.WriteLine(@"Inflation cancelled. Returning to main menu.
");
                    isDone = true;
                }
                else
                {
                    try
                    {
                        InputValidator.ValidateNonEmptyString(licensePlateInput, "License plate"); 
                        r_GarageManager.InflateAllWheelsToMaxAirPressure(licensePlateInput);
                        Console.WriteLine(@"ALL TIRES HAVE BEEN INFLATED TO MAXIMUM PRESSURE
");
                        isDone = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine();
                    }
                }
            }
        }

        private void showLicensePlates()
        {
            bool validAnswer = false;
            string stateFilter = null;
            string filterChoice = string.Empty;

            //Console.WriteLine("Would you like to filter the license plates by vehicle state? <y/n>: ");
            //string filterChoice = Console.ReadLine()?.Trim().ToLower();

            //if (filterChoice == "yes")
            //{
            //    Console.WriteLine("Please enter the state to filter by (InRepair, Repaired, Paid): ");
            //    stateFilter = Console.ReadLine();
            //    InputValidator.ValidateEnum(stateFilter, typeof(Garage.GarageItem.eStateOfCar), "Vehicle state"); 
            //}

            while (!validAnswer)
            {
                Console.WriteLine("Would you like to filter the license plates by vehicle state? <y/n>: ");
                filterChoice = Console.ReadLine()?.Trim().ToLower();

                if (filterChoice == "y" || filterChoice == "n")
                {
                    validAnswer = true;
                    if (filterChoice == "y")
                    {
                        validAnswer = false;
                        while(!validAnswer)
                        {
                            Console.WriteLine("Please enter the state to filter by (InRepair, Repaired, Paid): ");
                            stateFilter = Console.ReadLine();
                            if(stateFilter == "InRepair" || stateFilter == "Repaired" || stateFilter == "Paid")
                            {
                                validAnswer = true;
                            }
                            else
                            {
                                Console.WriteLine("Entered unknown state");
                            }
                        }

                        InputValidator.ValidateEnum(stateFilter, typeof(Garage.GarageItem.eStateOfCar), "Vehicle state");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter y for 'YES' or n for 'NO'.");
                }
            }

            try
            {
                List<string> licensePlates = r_GarageManager.GetVehiclesInGarageLicensePlates(stateFilter);

                if (licensePlates.Count == 0)
                {
                    Console.WriteLine($"No vehicles found with \"{stateFilter}\" filter.");
                }
                else
                {
                    Console.WriteLine("License plates in the garage:");
                    int index = 1;
                    foreach (string plate in licensePlates)
                    {
                        Console.WriteLine($"{index}) {plate}");
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();
        }

        private string getValidatedLicensePlateWithExit(string i_ExitCode, string i_ActionLabel, bool i_RequireRefillable = false, bool i_RequireRechargeable = false)
        {
            string validatedPlate = null;
            bool isDone = false;

            while (!isDone)
            {
                Console.WriteLine($"{k_ProvidePlateNumberMsg} (or press {i_ExitCode} to cancel {i_ActionLabel})");
                string input = Console.ReadLine()?.Trim();

                if (input == i_ExitCode)
                {
                    Console.WriteLine($@"{i_ActionLabel} cancelled. Returning to main menu.
");
                    isDone = true;
                }
                else
                {
                    try
                    {
                        InputValidator.ValidateNonEmptyString(input, "License plate");

                        Vehicle vehicle = r_GarageManager.GetGarageItem(input).Vehicle;

                        if (i_RequireRefillable && !(vehicle is IFillable))
                        {
                            Console.WriteLine("This vehicle is not a fuel-based vehicle and cannot be refueled.");
                        }
                        else if (i_RequireRechargeable && !(vehicle is IChargeable))
                        {
                            Console.WriteLine("This vehicle is not an electric vehicle and cannot be recharged.");
                        }
                        else
                        {
                            validatedPlate = input;
                            isDone = true;
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Console.WriteLine();
                }
            }

            return validatedPlate;
        }

        public static string ValidatedFloatNumberInput(string i_Prompt, string i_FieldName)
        {
            Console.WriteLine(i_Prompt);
            string userInput = Console.ReadLine();

            InputValidator.ValidateFloat(userInput, i_FieldName);

            return userInput;
        }

        private void rechargeElectricVehicle()
        {
            string exitCode = ((int)eUserOptions.Exit).ToString();
            string licensePlate = getValidatedLicensePlateWithExit(exitCode, "Recharge", i_RequireRechargeable: true);

            if (licensePlate != null)
            {
                try
                {
                    string minutes = ValidatedFloatNumberInput("Please provide minutes to charge:", "Charge minutes");
                    r_GarageManager.RechargeVehicle(licensePlate, minutes);
                    Console.WriteLine("Recharge successful!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Input error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during recharging: {ex.Message}");
                }
            }
        }

        private void getAndShowFullVehicleData()
        {
            string exitCode = ((int)eUserOptions.Exit).ToString();
            Console.WriteLine(
                $"{k_ProvidePlateNumberMsg}(or press {exitCode} to cancel)");
            string input = Console.ReadLine()?.Trim();

            if (input == exitCode)
            {
                Console.WriteLine(@"Operation cancelled.
");
                return;
            }

            try
            {
                InputValidator.ValidateNonEmptyString(input, "License plate");

                // Retrieve the full garage entry
                Garage.GarageItem garageItem = r_GarageManager.GetGarageItem(input);

                // Basic info
                Console.WriteLine();
                Console.WriteLine($"License Plate: {garageItem.Vehicle.LicensePlate}");
                Console.WriteLine($"Model: {garageItem.Vehicle.ModelName}");
                Console.WriteLine($"Owner: {garageItem.OwnerName} ({garageItem.OwnerPhoneNumber})");
                Console.WriteLine($"Status: {garageItem.StateOfCar}");
                if(garageItem.Vehicle is IFillable fuelGarageItemVehicle)
                {
                    Console.WriteLine($"Fuel type: {fuelGarageItemVehicle.GetFuelType()}");
                }

                Console.WriteLine($"Energy Remaining: {garageItem.Vehicle.EnergyPercentage:0.##}%");

                // Print each wheel
                Console.WriteLine("Wheels:");
                foreach (Wheel wheel in garageItem.Vehicle.Wheels)
                {
                    Console.WriteLine(
                        $"   {wheel.Manufacturer} — {wheel.CurrentAirPressure}/{wheel.MaxAirPressure}");
                }

                // Print any subtype-specific details
                if (garageItem.Vehicle is IDetailedVehicle detailed)
                {
                    Dictionary<string, object> vehicleSpecificData = detailed.GetDetails();

                    foreach (string specificDetail in vehicleSpecificData.Keys)
                    {
                        Console.WriteLine($"{specificDetail}: {vehicleSpecificData[specificDetail]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error: {ex.Message}
");
            }

            Console.WriteLine();
        }

        private void refuelVehicle()
        {
            string exitCode = ((int)eUserOptions.Exit).ToString();
            string plateNumber = getValidatedLicensePlateWithExit(exitCode, "Refuel", i_RequireRefillable: true);

            if (plateNumber != null)
            {
                Console.WriteLine("Please provide your fuel type: Octan95 / Octan96 / Octan98 / Soler");
                string fuelTypeInput = Console.ReadLine();
                Console.WriteLine("Please provide your chosen amount to be fueled (Liters):");
                string fuelAmountInput = Console.ReadLine();

                try
                {
                    InputValidator.ValidateEnum(fuelTypeInput, typeof(eFuelTypeUI), "Fuel type");
                    InputValidator.ValidateFloat(fuelAmountInput, "Fuel amount");
                    r_GarageManager.RefuelVehicle(plateNumber, fuelAmountInput, fuelTypeInput);
                    Console.WriteLine("SUCCESS");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Input error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }

        private void changeVehicleStatus()
        {
            string exitCode = ((int)eUserOptions.Exit).ToString();
            string licensePlate = getValidatedLicensePlateWithExit(exitCode, "Status Change");
            string stateInput = string.Empty;

            if (licensePlate != null)
            {
                bool validAnswer = false;

                while (!validAnswer)
                {
                    Console.WriteLine("Please provide your wanted state - InRepair  /  Repaired  /  Paid");
                    stateInput = Console.ReadLine();

                    if(stateInput == "InRepair" || stateInput == "Repaired" || stateInput == "Paid")
                    {
                        validAnswer = true;
                    }
                    else
                    {
                        Console.WriteLine("Entered unknown state.");
                    }
                }

                try
                {
                    InputValidator.ValidateEnum(stateInput, typeof(Garage.GarageItem.eStateOfCar), "Vehicle state");
                    r_GarageManager.UpdateVehicleStateInGarage(licensePlate, stateInput);
                    Console.WriteLine($"Vehicle number {licensePlate} state was changed to \"{stateInput}\" state!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Input error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($@"Error: {ex.Message}
");
                }

                Console.WriteLine();
            }
        }

        private void loadVehiclesFromDatabase()
        {
            try
            {
                r_GarageManager.LoadVehiclesFromDB(k_DBFilePath);
                Console.WriteLine("DATABASE LOADED SUCCESSFULLY!");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                if(e is ArgumentException && e.Message.StartsWith("An item"))
                {
                    Console.WriteLine(@"You tried to load an exiting vehicle.
Please try again.
");
                }
                else
                {
                    Console.WriteLine($@"{e.Message}
");
                }
            }
        }

        private void insertNewVehicle()
        {
            try
            {
                string[] vehicleData = collectFormattedVehicleData(out List<string> wheelData);
                string vehicleDataStr = string.Join(",", vehicleData);

                r_GarageManager.LoadVehiclesFromUser(vehicleDataStr, wheelData);
                Console.WriteLine("Vehicle added successfully!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private string[] collectFormattedVehicleData(out List<string> o_WheelData)
        {
            string[] vehicleData = new string[10];
            o_WheelData = null;

            string vehicleType = askForVehicleType();
            vehicleData[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType] = vehicleType;

            collectBasicVehicleInfo(vehicleData);

            int wheelCount = getWheelCountForType(vehicleType);
            collectWheelInfo(vehicleData, out o_WheelData, wheelCount);

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

            string inputType = null;
            bool isValid = false;

            while (!isValid)
            {
                inputType = Console.ReadLine()?.Trim();
                InputValidator.ValidateNonEmptyString(inputType, "Vehicle type");

                if (VehicleCreator.SupportedTypes.Contains(inputType))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid vehicle type. Please enter one of the listed types.");
                }
            }

            return inputType;
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
            string answer = null;
            bool validAnswer = false;

            while (!validAnswer)
            {
                Console.Write("Do you want to enter the same wheel info for all wheels? <y/n>: ");
                answer = Console.ReadLine()?.Trim().ToLower();

                if (answer == "y" || answer == "n")
                {
                    validAnswer = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter y for 'YES' or n for 'NO'.");
                }
            }

            if (answer == "y")
            {
                Console.Write("Enter wheel manufacturer: ");
                string manufacturer = Console.ReadLine();
                InputValidator.ValidateNonEmptyString(manufacturer, "Wheel manufacturer");
                io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.TierModel] = manufacturer;

                string pressurePrompt = "Enter current air pressure: ";
                string airPressure = ValidatedFloatNumberInput(pressurePrompt, "Current air pressure");
                io_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.CurrAirPressure] = airPressure;
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
                    string manufacturer = Console.ReadLine();
                    InputValidator.ValidateNonEmptyString(manufacturer, $"Manufacturer (Wheel {i + 1})");
                    o_WheelData.Add(manufacturer);

                    string pressurePrompt = "Enter current air pressure: ";
                    string airPressure = ValidatedFloatNumberInput(pressurePrompt, $"Air pressure (Wheel {i + 1})");
                    o_WheelData.Add(airPressure);
                }
            }
        }

        private void collectTypeSpecificData(string[] io_VehicleData, string i_Type)
        {
            if (r_DataCollectors.TryGetValue(i_Type, out IUITypeDataCollector collector))
            {
                collector.CollectData(io_VehicleData);
            }
            else
            {
                Console.WriteLine("No specific data collector found for this vehicle type.");
            }
        }

        private int getWheelCountForType(string i_Type, string i_LicensePlate = "temp", string i_ModelName = "temp")
        {
            Vehicle tempVehicle = VehicleCreator.CreateVehicle(i_Type, i_LicensePlate, i_ModelName);

            return tempVehicle?.NumberOfWheels ?? 0;
        }
    }
}