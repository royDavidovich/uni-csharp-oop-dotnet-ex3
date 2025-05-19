using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        protected FuelVehicle m_Engine;
        protected const int k_NumberOfWheels = 12;
        protected const float k_MaxFuelAmount = 135f;
        protected const int k_MaxAirPressure = 27;
        protected const string k_GasType = "Soler";
        protected int m_NumberOfDoors;

        protected enum eSpecificDataIndicesInFile
        {
            IsHazardousCargoLoaded = 7,
            CargoVolume = 8
        }

        public bool IsHazardousCargoLoaded { get; set; }
        public float CargoVolume { get; set; }

        public Truck(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            if (bool.TryParse(
                    i_VehicleData[(int)eSpecificDataIndicesInFile.IsHazardousCargoLoaded],
                    out bool isHazardousCargoLoaded))
            {
                this.IsHazardousCargoLoaded = isHazardousCargoLoaded;
            }
            else
            {
                throw new ArgumentException("Invalid Hazardous Cargo Loaded Argument");
            }

            if (float.TryParse(
                    i_VehicleData[(int)eSpecificDataIndicesInFile.CargoVolume],
                    out float cargoVolume))
            {
                this.CargoVolume = cargoVolume;
            }
            else
            {
                throw new ArgumentException("Invalid Cargo Volume");
            }
        }

        protected override void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels)
        {
            string manufacturer = i_GalgalimData[(int)eGeneralDataIndicesInFile.TierModel];
            string pressureStr = i_GalgalimData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitWheelsFromDb(manufacturer, pressureStr, k_NumberOfWheels, k_MaxAirPressure, i_MyWheels);
        }
    }
}