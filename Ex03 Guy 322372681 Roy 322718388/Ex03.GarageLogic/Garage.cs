using System.Collections.Generic;
namespace Ex03.GarageLogic
{
    internal class Garage
    {
        private readonly Dictionary<string, GarageItem> r_GarageItems;

        public Garage()
        {
            r_GarageItems = new Dictionary<string, GarageItem>();
        }

        private class GarageItem
        {
            public string OwnerName { get; private set; }
            public string OwnerPhoneNumber { get; private set; }
            public eStateOfCar StateOfCar { get; private set; }
            public Vehicle Vehicle { get; private set; }

            public enum eStateOfCar
            {
                repairing,
                repared,
                paid
            }

            public GarageItem(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
            {
                OwnerName = i_OwnerName;
                OwnerPhoneNumber = i_OwnerPhoneNumber;
                StateOfCar = eStateOfCar.repairing;
                Vehicle = i_Vehicle;
            }
        }
    }

}
