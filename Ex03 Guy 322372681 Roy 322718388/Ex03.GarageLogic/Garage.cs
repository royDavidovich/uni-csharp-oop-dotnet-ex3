using System.Collections.Generic;

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
    }
}