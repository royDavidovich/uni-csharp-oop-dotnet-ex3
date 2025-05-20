using System;
using System.Collections.Generic;
using System.IO;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Garage r_MyGarage = new Garage();

        public void LoadVehiclesFromDb(string i_FilePath)
        {
            string[] lines = File.ReadAllLines(i_FilePath);
            foreach (string line in lines)
            {
                string[] vehicleInformation = line.Split(',');
                string currentVehicleTypeFromDB = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType];

                if (string.IsNullOrWhiteSpace(line)
                   || !(VehicleCreator.SupportedTypes.Contains(currentVehicleTypeFromDB)))
                {
                    continue;       //continue if empty line or doesn't fit format description
                }

                Vehicle currentVehicleFromDb = VehicleCreator.CreateVehicle(
                    currentVehicleTypeFromDB,
                    vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate],
                    vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.ModelName]);
                currentVehicleFromDb.InitVehicleInformation(vehicleInformation);
                r_MyGarage.AddGarageEntry(currentVehicleFromDb);
            }
        }

        public void LoadVehiclesFromUser(string i_CurrentUserVehicleData, List<string> Galgalim = null)
        {
            string[] vehicleInformation = i_CurrentUserVehicleData.Split(',');
            string currentVehicleTypeFromDB = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType];

            if (string.IsNullOrWhiteSpace(i_CurrentUserVehicleData)
                || !(VehicleCreator.SupportedTypes.Contains(currentVehicleTypeFromDB)))
            {
                throw new ArgumentException(
                    $"Unknown fuel type: {currentVehicleTypeFromDB}",
                    currentVehicleTypeFromDB);
            }

            Vehicle currentVehicleFromDb = VehicleCreator.CreateVehicle(
                currentVehicleTypeFromDB,
                vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate],
                vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.ModelName]);
            currentVehicleFromDb.InitVehicleInformation(vehicleInformation, Galgalim);
            AddUsersGarageEntry(currentVehicleFromDb);
        }

        public void AddUsersGarageEntry(Vehicle i_CurrentUserVehicle)
        {
            r_MyGarage.AddGarageEntry(i_CurrentUserVehicle);
        }

       // public static int Main()
       // {
       //     GarageManager garageManager = new GarageManager();

        //   garageManager.LoadVehiclesFromDb(
        //        "C:\\Users\\royda\\OneDrive - The Academic College of Tel-Aviv Jaffa - MTA\\myRepos\\uni-csharp-oop-dotnet-ex3\\Ex03 Guy 322372681 Roy 322718388\\Vehicles.db");

        //    return 0;
        //}
    }
}