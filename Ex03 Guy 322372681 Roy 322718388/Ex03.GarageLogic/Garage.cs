using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public class GarageItem // Changed from private to public
        {
            public string OwnerName { get; private set; }
            public string OwnerPhoneNumber { get; private set; }
            public eStateOfCar StateOfCar { get; set; }
            public Vehicle Vehicle { get; private set; }

            public enum eStateOfCar
            {
                InRepair,
                Repaired,
                Paid
            }

            public GarageItem(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
            {
                OwnerName = i_OwnerName;
                OwnerPhoneNumber = i_OwnerPhoneNumber;
                StateOfCar = eStateOfCar.InRepair;
                Vehicle = i_Vehicle;
            }
        }

        private Dictionary<string, GarageItem> m_GarageVehicles = new Dictionary<string, GarageItem>();

        public Dictionary<string, GarageItem> LocalGarage
        {
            get { return m_GarageVehicles; }
        }

        public void AddGarageEntry(Vehicle i_Vehicle, string i_OwnerName = "", string i_OwnerPhone = "")
        {
            GarageItem newVehicle = new GarageItem(i_OwnerName, i_OwnerPhone, i_Vehicle);
            this.m_GarageVehicles.Add(newVehicle.Vehicle.LicensePlate, newVehicle);
        }

        public List<string> GetLicensePlates(string i_SpecificVehiclesState = null)
        {
            List<string> licensePlates = new List<string>(this.m_GarageVehicles.Count);

            if (i_SpecificVehiclesState == null)
            {
                licensePlates.AddRange(m_GarageVehicles.Keys);
            }
            else
            {
                this.getVehiclesInGarageLicensePlatesByState(licensePlates, i_SpecificVehiclesState);
            }

            return licensePlates;
        }

        private void getVehiclesInGarageLicensePlatesByState(List<string> i_LicensePlates, string i_VehiclesState)
        {
            if (!Enum.TryParse(i_VehiclesState, true, out GarageItem.eStateOfCar stateOfVehicle))
            {
                throw new ArgumentException($"Unknown vehicle state: {i_VehiclesState}", nameof(i_VehiclesState));
            }

            i_LicensePlates.Clear();
            foreach (KeyValuePair<string, GarageItem> keyValuePair in m_GarageVehicles)
            {
                if (keyValuePair.Value.StateOfCar == stateOfVehicle)
                {
                    i_LicensePlates.Add(keyValuePair.Key);
                }
            }
        }

        public void UpdateVehicleStateInGarage(string i_VehicleLicensePlate, string i_NewVehicleState)
        {
            if (!Enum.TryParse(i_NewVehicleState, true, out GarageItem.eStateOfCar newStateOfVehicle))
            {
                throw new ArgumentException($"Unknown vehicle state: {i_NewVehicleState}", i_NewVehicleState);
            }

            if (!m_GarageVehicles.TryGetValue(i_VehicleLicensePlate, out GarageItem vehicleToUpdate))
            {
                throw new ArgumentException(
                    $"Unknown license plate or vehicle isn't in garage: {i_VehicleLicensePlate}",
                    i_VehicleLicensePlate);
            }

            vehicleToUpdate.StateOfCar = newStateOfVehicle;
        }
    }
}