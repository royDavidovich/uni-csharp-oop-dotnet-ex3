﻿using System;
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
                    $"Unknown vehicle type: {currentVehicleTypeFromDB}",
                    currentVehicleTypeFromDB);
            }

            Vehicle currentVehicleFromDB = VehicleCreator.CreateVehicle(
                currentVehicleTypeFromDB,
                vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.LicensePlate],
                vehicleInformation[(int)Vehicle.eGeneralDataIndicesInFile.ModelName]);

            currentVehicleFromDB.InitVehicleInformation(vehicleInformation, Galgalim);
            AddUsersGarageEntry(currentVehicleFromDB);
        }

        public void AddUsersGarageEntry(Vehicle i_CurrentUserVehicle)
        {
            r_MyGarage.AddGarageEntry(i_CurrentUserVehicle);
        }

        public List<string> GetVehiclesInGarageLicensePlates(string i_SpecificVehiclesState = null)
        {
            return r_MyGarage.GetLicensePlates(i_SpecificVehiclesState);
        }

        public bool IsRefillable(string i_LicensePlate)
        {
            if (!r_MyGarage.LocalGarage.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException(
                    $"Unknown license plate: {i_LicensePlate}",
                    nameof(i_LicensePlate));
            }

            return r_MyGarage.LocalGarage[i_LicensePlate].Vehicle is IFillable;
        }

        public void RefuelVehicle(string i_LicensePlate, string i_AmountToAddStr, string i_FuelTypeStr)
        {
            if (!r_MyGarage.LocalGarage.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException(
                    $"Unknown license plate: {i_LicensePlate}",
                    nameof(i_LicensePlate));
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
                throw new ArgumentException($"Unknown license plate: {i_LicensePlate}", nameof(i_LicensePlate));
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
                throw new ArgumentException($"Unknown license plate: {i_LicensePlate}", nameof(i_LicensePlate));
            }

            r_MyGarage.LocalGarage[i_LicensePlate].Vehicle.InflateAllWheelsToMaxAirPressure();
        }
    }
}