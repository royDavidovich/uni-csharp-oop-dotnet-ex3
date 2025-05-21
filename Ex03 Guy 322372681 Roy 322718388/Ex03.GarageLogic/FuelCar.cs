using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        protected FuelVehicle m_Engine;
        protected const float k_MaxFuelAmount = 48f;
        protected const float k_MaxAirPressure = 32f;
        protected const string k_GasType = "Octan95";

        public FuelCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Engine = new FuelVehicle(k_MaxFuelAmount, k_GasType);
        }

        protected override float MaxAirPressure
        {
            get
            {
                return k_MaxAirPressure;
            }
        }

        protected override void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr)
        {
            if (!float.TryParse(i_CurrentPercentageStr, out float energyPercentage))
            {
                throw new FormatException($"Invalid energy percentage: {i_CurrentPercentageStr}");
            }

            float liters = (energyPercentage / 100f * k_MaxFuelAmount);

            m_Engine.CurrentFuelLevel = liters;
        }
    }
}