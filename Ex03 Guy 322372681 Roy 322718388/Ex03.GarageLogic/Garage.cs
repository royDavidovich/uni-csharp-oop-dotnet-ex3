using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    internal class Garage
    {
        private class GarageItem
        {
            public string OwnerName { get; private set; }
            public string OwnerPhoneNumber { get; private set; }
            public eStateOfCar StateOfCar { get; private set; }
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

        public void AddGarageEntry(Vehicle i_Vehicle, string i_OwnerName = "", string i_OwnerPhone = "")
        {
            GarageItem newVehicle = new GarageItem(i_OwnerName, i_OwnerPhone, i_Vehicle);
            this.m_GarageVehicles.Add(newVehicle.Vehicle.LicensePlate, newVehicle);
        }

        public List<string> GetVehiclesInGarageLicensePlates(string i_SpecificVehiclesState = null)
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
            if (!Enum.TryParse(i_VehiclesState, true, out GarageItem.eStateOfCar stateOfCar))
            {
                throw new ArgumentException($"Unknown vehicle state: {i_VehiclesState}", nameof(i_VehiclesState));
            }

            i_LicensePlates.Clear();
            foreach (KeyValuePair<string, GarageItem> keyValuePair in m_GarageVehicles)
            {
                if (keyValuePair.Value.StateOfCar == stateOfCar)
                {
                    i_LicensePlates.Add(keyValuePair.Key);
                }
            }
        }
    }
}