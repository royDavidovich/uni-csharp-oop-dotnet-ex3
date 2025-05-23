using System;
using System.Collections.Generic;
using System.IO;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Garage r_MyGarage = new Garage();

        public void LoadVehiclesFromDB(string i_FilePath)
        {
            string[] lines = File.ReadAllLines(i_FilePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] vehicleInformation = line.Split(',');
                string currentVehicleTypeFromDB = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType];

                if (!VehicleCreator.SupportedTypes.Contains(currentVehicleTypeFromDB))
                {
                    continue;
                }

                string currentVehicleOwnersName = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.OwnerName];
                string currentVehicleOwnersPhone = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.OwnerPhone];

                Vehicle currentVehicleFromDb = VehicleCreator.CreateVehicle(
                    currentVehicleTypeFromDB,
                    vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate],
                    vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.ModelName]);

                currentVehicleFromDb.InitVehicleInformation(vehicleInformation);
                r_MyGarage.AddGarageEntry(currentVehicleFromDb, currentVehicleOwnersName, currentVehicleOwnersPhone);
            }
        }

        public void LoadVehiclesFromUser(string i_CurrentUserVehicleData, List<string> i_Galgalim = null)
        {
            string[] vehicleInformation = i_CurrentUserVehicleData.Split(',');
            string currentVehicleTypeFromDB = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.VehicleType];
            string currentVehicleOwnersName = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.OwnerName];
            string currentVehicleOwnersPhone = vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.OwnerPhone];

            if (string.IsNullOrWhiteSpace(i_CurrentUserVehicleData)
                || !VehicleCreator.SupportedTypes.Contains(currentVehicleTypeFromDB))
            {
                throw new ArgumentException(
                    $"Unknown vehicle type: {currentVehicleTypeFromDB}",
                    currentVehicleTypeFromDB);
            }

            Vehicle currentVehicleFromDb = VehicleCreator.CreateVehicle(
                currentVehicleTypeFromDB,
                vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate],
                vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.ModelName]);

            currentVehicleFromDb.InitVehicleInformation(vehicleInformation, i_Galgalim);
            r_MyGarage.AddGarageEntry(currentVehicleFromDb, currentVehicleOwnersName, currentVehicleOwnersPhone);
        }

        public List<string> GetVehiclesInGarageLicensePlates(string i_SpecificVehiclesState = null)
        {
            return r_MyGarage.GetLicensePlates(i_SpecificVehiclesState);
        }

        public bool IsRefillable(string i_LicensePlate)
        {
            if (!r_MyGarage.LocalGarage.TryGetValue(i_LicensePlate, out Garage.GarageItem garageItem))
            {
                throw new ArgumentException($"Unknown license plate: {i_LicensePlate}", i_LicensePlate);
            }

            return garageItem.Vehicle is IFillable;
        }

        public void RefuelVehicle(string i_LicensePlate, string i_AmountToAddStr, string i_FuelTypeStr)
        {
            if (!r_MyGarage.LocalGarage.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException($"Unknown license plate: {i_LicensePlate}", i_LicensePlate);
            }

            if (!float.TryParse(i_AmountToAddStr, out float amountToAdd))
            {
                throw new FormatException($"Invalid energy percentage: {i_AmountToAddStr}");
            }

            (r_MyGarage.LocalGarage[i_LicensePlate].Vehicle as IFillable).Refuel(amountToAdd, i_FuelTypeStr);
        }

        public void RechargeVehicle(string i_LicensePlate, string i_AmountToAddInMinutesStr)
        {
            if (!r_MyGarage.LocalGarage.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException($"Unknown license plate: {i_LicensePlate}", i_LicensePlate);
            }

            if (!float.TryParse(i_AmountToAddInMinutesStr, out float amountToAdd))
            {
                throw new FormatException($"Invalid energy percentage: {i_AmountToAddInMinutesStr}");
            }

            (r_MyGarage.LocalGarage[i_LicensePlate].Vehicle as IChargeable).Recharge(amountToAdd);
        }

        public void InflateAllWheelsToMaxAirPressure(string i_LicensePlate)
        {
            if (!r_MyGarage.LocalGarage.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException($"Unknown license plate: {i_LicensePlate}", i_LicensePlate);
            }

            r_MyGarage.LocalGarage[i_LicensePlate].Vehicle.InflateAllWheelsToMaxAirPressure();
        }

        public void UpdateVehicleStateInGarage(string i_VehicleLicensePlate, string i_NewVehicleState)
        {
            r_MyGarage.UpdateVehicleState(i_VehicleLicensePlate, i_NewVehicleState);
        }

        public Garage.GarageItem GetGarageItem(string i_LicensePlate)
        {
            if (!r_MyGarage.LocalGarage.TryGetValue(i_LicensePlate, out Garage.GarageItem garageItem))
            {
                throw new ArgumentException($"Unknown license plate: {i_LicensePlate}", i_LicensePlate);
            }

            return garageItem;
        }
    }
}