using System;
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
<<<<<<< HEAD
=======

        public static int Main()
        {
            GarageManager garageManager = new GarageManager();

            garageManager.LoadVehiclesFromDb(
                "C:\\Users\\royda\\OneDrive - The Academic College of Tel-Aviv Jaffa - MTA\\myRepos\\uni-csharp-oop-dotnet-ex3\\Ex03 Guy 322372681 Roy 322718388\\Vehicles.db");

            return 0;
        }
>>>>>>> fcd71e67382da8bcd7f113f382d73d623846ab99
    }
}