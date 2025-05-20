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

        protected enum eSpecificDataIndicesInFile
        {
            IsHazardousCargoLoaded = 8,
            CargoVolume = 9
        }

        public Truck(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Engine = new FuelVehicle(k_MaxFuelAmount, k_GasType);
        }

        public bool IsHazardousCargoLoaded { get; set; }

        public float CargoVolume { get; set; }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            string energyPctStr = i_VehicleData[(int)eGeneralDataIndicesInFile.EnergyPercentage];
            string isHazardousCargoLoadedStr = i_VehicleData[(int)eSpecificDataIndicesInFile.IsHazardousCargoLoaded];
            string cargoVolumeStr = i_VehicleData[(int)eSpecificDataIndicesInFile.CargoVolume];

            SetCurrentEnergyFromPercentage(energyPctStr);
            parseAndSetIsHazardousCargo(isHazardousCargoLoadedStr);
            parseAndSetCargoVolume(cargoVolumeStr);
        }

        protected void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr)
        {
            if (!float.TryParse(i_CurrentPercentageStr, out float energyPercentage))
            {
                throw new FormatException($"Invalid energy percentage: {i_CurrentPercentageStr}");
            }

            float liters = (energyPercentage / 100f * k_MaxFuelAmount);

            m_Engine.CurrentFuelLevel = liters;
        }

        private void parseAndSetIsHazardousCargo(string i_IsHazardousStr)
        {
            if (bool.TryParse(i_IsHazardousStr, out bool isHazardous))
            {
                this.IsHazardousCargoLoaded = isHazardous;
            }
            else
            {
                throw new FormatException($"Invalid Hazardous Cargo value: {i_IsHazardousStr}");
            }
        }

        private void parseAndSetCargoVolume(string i_CargoVolumeStr)
        {
            if (float.TryParse(i_CargoVolumeStr, out float volume))
            {
                this.CargoVolume = volume;
            }
            else
            {
                throw new FormatException($"Invalid Cargo Volume: {i_CargoVolumeStr}");
            }
        }

        protected override void InitVehiclesGalgalimList(string[] i_GalgalimData)
        {
            string manufacturer = i_GalgalimData[(int)eGeneralDataIndicesInFile.TierModel];
            string pressureStr = i_GalgalimData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitIdenticalWheelsFromDB(manufacturer, pressureStr, k_NumberOfWheels, k_MaxAirPressure);
        }
    }
}