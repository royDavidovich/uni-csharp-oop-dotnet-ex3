using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car , IChargeable
    {
        protected ElectricVehicle m_Battery;
        protected const float k_MaxFuelAmountInHours = 4.8f;
        protected const float k_MaxAirPressure = 32f;

        public ElectricCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Battery = new ElectricVehicle(k_MaxFuelAmountInHours);
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

            float hours = (energyPercentage / 100f * k_MaxFuelAmountInHours);

            m_Battery.CurrentBatteryPowerInHours = hours;
        }

        public void Recharge(float amountToAdd)
        {
            m_Battery.Recharge(amountToAdd);
        }
    }
}