﻿using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle, IChargeable
    {
        protected const float k_MaxFuelAmountInHours = 3.2f;
        protected const float k_MaxAirPressure = 30f;
        protected ElectricVehicle m_Battery;

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
                return m_Battery.EnergyPercentage;
            }
        }

        public ElectricMotorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Battery = new ElectricVehicle(k_MaxFuelAmountInHours);
        }

        public void Recharge(float i_AmountToAddInMinuets)
        {
            m_Battery.Recharge(i_AmountToAddInMinuets);
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
    }
}