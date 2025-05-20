using Ex03.GarageLogic;
using System;

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

        // m_GarageManager.LoadVehiclesFromDb("C:\\Users\\guyfi\\source\\repos\\uni-csharp-oop-dotnet-ex3\\Ex03" +
         //       " Guy 322372681 Roy 322718388\\Vehicles.db");
        public void Run()
        {
            greetUser();
            while (true)
            {
                eUserOptions usersActionChoice;
                try
                {
                    usersActionChoice = askForOption();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    continue;
                }

                handleChoice(usersActionChoice);
                if (UserDecidedToExit)
                {
                    break;
                }
            }

            sayGoodbyeToUser();
        }

        private eUserOptions askForOption()
        {
            string message = String.Format(
@"Please choose your option:
1. Load the Vehicles from our DataBase.
2. Insert a new vehicle to our Garage.
3. Show all vheicles license plates.
4. Change the status of a vehicle.
5. Inflate tires.
6. Refuel a fuelable vehicle.
7. Recharge an electric vehicle.
8. Show all the data for a specific vehicle.
9. exit our Garage.");

            Console.WriteLine(message);
            string userInput = Console.ReadLine();

            if (Enum.TryParse<eUserOptions>(userInput, out eUserOptions option) && Enum.IsDefined(typeof(eUserOptions), option))
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

        private void handleChoice(eUserOptions i_choice)
        {
            switch (i_choice)
            {
                case eUserOptions.LoadVehiclesFromDb:
                    try
                    {
                        m_GarageManager.LoadVehiclesFromDb("C:\\Users\\guyfi\\source\\repos\\uni-csharp-oop-dotnet-ex3\\Ex03 Guy 322372681 Roy 322718388\\Vehicles.db");
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine(ex.Message); 
                    }
                    break;

                case eUserOptions.InsertNewVehicle:
                    //TODO
                    break;

                case eUserOptions.ShowLicensePlates:
                    //TODO
                    break;

                case eUserOptions.ChangeVehicleStatus:
                    //TODO
                    break;

                case eUserOptions.InflateTiresToMax:
                    //TODO
                    break;

                case eUserOptions.RefuelFuelVehicle:
                    //TODO
                    break;

                case eUserOptions.RechargeElectricVehicle:
                    //TODO
                    break;

                case eUserOptions.ShowFullVehicleData:
                    //TODO
                    break;

                case eUserOptions.Exit:
                    UserDecidedToExit = true;
                    break;
            }
        }

    }
}
