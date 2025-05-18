using System;
using System.IO;

namespace Ex03.GarageLogic
{
    internal class GarageManager
    {
        private Garage m_MyGarage;
        private const int k_VehicleTypeIndex = 0;

        public void LoadVehiclesFromDb(string i_FilePath)
        {
            string[] lines = File.ReadAllLines(i_FilePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("*****"))
                {
                    break; // stop when empty lines or format description
                }

                string[] vehicleInformation = line.Split(',');

                Vehicle currentVehicleFromDb = VehicleCreator.CreateVehicle(vehicleInformation[k_VehicleTypeIndex]);
                currentVehicleFromDb.InitVehicleToGarage(vehicleInformation);
                m_MyGarage.AddGarageEntry(currentVehicleFromDb);
            }
        }
    }
}