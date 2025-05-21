using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle, IChargeable
    {
        protected ElectricVehicle m_Battery;
        protected const float k_MaxFuelAmount = 3.2f;
        protected const float k_MaxAirPressure = 30f;

        public ElectricMotorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Battery = new ElectricVehicle(k_MaxFuelAmount);
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

            float hours = (energyPercentage / 100f * k_MaxFuelAmount);

            m_Battery.CurrentBatteryPower = hours;
        }

        public void Recharge(float amountToAdd)
        {
            m_Battery.Recharge(amountToAdd);
        }
    }
}