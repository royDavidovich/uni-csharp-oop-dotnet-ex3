using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle, IFillable, IDetailedVehicle
    {
        protected const int k_NumberOfWheels = 12;
        protected const float k_MaxFuelAmount = 135f;
        protected const float k_MaxAirPressure = 27f;
        protected const string k_GasType = "Soler";

        protected enum eSpecificDataIndicesInFile
        {
            IsHazardousCargoLoaded = 8,
            CargoVolume = 9
        }

        public FuelVehicle m_FuelEngine;

        public Truck(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_FuelEngine = new FuelVehicle(k_MaxFuelAmount, k_GasType);
        }

        public override int NumberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        protected override float MaxAirPressure
        {
            get
            {
                return k_MaxAirPressure;
            }
        }

        public bool IsHazardousCargoLoaded { get; set; }

        public float CargoVolume { get; set; }

        public override float EnergyPercentage
        {
            get
            {
                return m_FuelEngine.EnergyPercentage;
            }
        }

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

            m_FuelEngine.CurrentFuelLevel = liters;
        }

        private void parseAndSetIsHazardousCargo(string i_IsHazardousStr)
        {
            if(!bool.TryParse(i_IsHazardousStr, out bool isHazardous))
            {
                throw new FormatException($"Invalid Hazardous Cargo value: {i_IsHazardousStr}");
            }

            this.IsHazardousCargoLoaded = isHazardous;
        }

        private void parseAndSetCargoVolume(string i_CargoVolumeStr)
        {
            if(!float.TryParse(i_CargoVolumeStr, out float volume))
            {
                throw new FormatException($"Invalid Cargo Volume: {i_CargoVolumeStr}");
            }

            this.CargoVolume = volume;
        }

        public void Refuel(float i_AmountToAdd, string i_FuelType)
        {
            m_FuelEngine.Refuel(i_AmountToAdd, i_FuelType);
        }

        public string GetFuelType()
        {
            return k_GasType;
        }

        public Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> vehicleSpecificData = new Dictionary<string, object>
            {
                { "Is Hazardous Cargo Loaded", IsHazardousCargoLoaded },
                { "Cargo Volume", CargoVolume }
            };

            return vehicleSpecificData;
        }
    }
}