using Ex03.ConsoleUI;
using System;
using static Ex03.ConsoleUI.GarageUIManager;

namespace Ex03.ConsoleUI
{
    public class TruckUIDataCollector : IUITypeDataCollector
    {
        public void CollectData(string[] io_VehicleData)
        {
            Console.Write("Is hazardous cargo loaded? (true/false): ");
            string hazardous = Console.ReadLine();
            InputValidator.ValidateNonEmptyString(hazardous, "Hazardous cargo flag");
            io_VehicleData[k_FirstSpecialIndexInData] = hazardous;

            string volume = GetValidatedNumberInput("Enter cargo volume: ", "Cargo volume");
            io_VehicleData[k_SecondSpecialIndexInData] = volume;
        }
    }

    public class CarUIDataCollector : IUITypeDataCollector
    {
        public void CollectData(string[] io_VehicleData)
        {
            Console.Write("Enter car color (Yellow, Black, White, Silver): ");
            string color = Console.ReadLine();
            InputValidator.ValidateEnum(color, typeof(eCarColorUI), "Car color");
            io_VehicleData[k_FirstSpecialIndexInData] = color;

            string doors = GetValidatedNumberInput("Enter number of doors (2–5): ", "Number of doors");
            io_VehicleData[k_SecondSpecialIndexInData] = doors;
        }
    }

    public class MotorcycleUIDataCollector : IUITypeDataCollector
    {
        public void CollectData(string[] io_VehicleData)
        {
            Console.Write("Enter permit type (A, A2, AB, B2): ");
            string permit = Console.ReadLine();
            InputValidator.ValidateEnum(permit, typeof(ePermitTypeUI), "Permit type");
            io_VehicleData[k_FirstSpecialIndexInData] = permit;

            string volume = GetValidatedNumberInput("Enter engine volume: ", "Engine volume");
            io_VehicleData[k_SecondSpecialIndexInData] = volume;
        }

    }
}
