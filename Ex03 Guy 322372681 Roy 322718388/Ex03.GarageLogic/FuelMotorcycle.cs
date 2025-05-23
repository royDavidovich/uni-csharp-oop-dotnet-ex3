using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle, IFillable
    {
        public FuelVehicle m_FuelEngine;
        protected const float k_MaxFuelAmount = 5.8f;
        protected const float k_MaxAirPressure = 30f;
        protected const string k_GasType = "Octan98";

        public FuelMotorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_FuelEngine = new FuelVehicle(k_MaxFuelAmount, k_GasType);
        }

        protected override float MaxAirPressure
        {
            get
            {
                return k_MaxAirPressure;
            }
        }

        public override float EnergyPercentage
        {
            get
            {
                return m_FuelEngine.EnergyPercentage;
            }
        }

        protected override void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr)
        {
            if (!float.TryParse(i_CurrentPercentageStr, out float energyPercentage))
            {
                throw new FormatException($"Invalid energy percentage: {i_CurrentPercentageStr}");
            }

            float liters = (energyPercentage / 100f * k_MaxFuelAmount);

            m_FuelEngine.CurrentFuelLevel = liters;
        }

        public void Refuel(float amountToAdd, string fuelType)
        {
            m_FuelEngine.Refuel(amountToAdd, fuelType);
        }
    }
}