using System;
using System.IO;

namespace Ex03.GarageLogic
{
    internal class GarageManager
    {
        private Garage m_MyGarage = new Garage();
        private const int k_VehicleTypeIndex = 0;

        public void LoadVehiclesFromDb(string i_FilePath)
        {
            string[] lines = File.ReadAllLines(i_FilePath);
            foreach (string line in lines)
            {
                string[] vehicleInformation = line.Split(',');
                string currentVehicleTypeFromDB = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType];

                if(string.IsNullOrWhiteSpace(line)
                   || !(VehicleCreator.SupportedTypes.Contains(currentVehicleTypeFromDB)))
                {
                    continue;       //continue if empty line or doesn't fit format description
                }

                Vehicle currentVehicleFromDb = VehicleCreator.CreateVehicle(
                    currentVehicleTypeFromDB,
                    vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate],
                    vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.ModelName]);
                currentVehicleFromDb.InitVehicleToGarage(vehicleInformation);
                m_MyGarage.AddGarageEntry(currentVehicleFromDb);
            }
        }

        public static int Main()
        {
            GarageManager garageManager = new GarageManager();

            garageManager.LoadVehiclesFromDb(
                "C:\\Users\\royda\\OneDrive - The Academic College of Tel-Aviv Jaffa - MTA\\myRepos\\uni-csharp-oop-dotnet-ex3\\Ex03 Guy 322372681 Roy 322718388\\Vehicles.db");


            return 0;
        }
    }
}