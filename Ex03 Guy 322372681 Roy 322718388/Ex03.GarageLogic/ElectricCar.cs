using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        protected ElectricVehicle m_Battery;
        protected const float k_MaxBatteryHours = 4.8f;
        protected const int k_MaxAirPressure = 32;

        public ElectricCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Battery = new ElectricVehicle(k_MaxBatteryHours);
        }

        protected override void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr)
        {
            if (!float.TryParse(i_CurrentPercentageStr, out float energyPercentage))
            {
                throw new ArgumentException($"Invalid energy percentage: {i_CurrentPercentageStr}", nameof(i_CurrentPercentageStr));
            }

            float hours = (energyPercentage / 100f * k_MaxBatteryHours);
            m_Battery.CurrentBatteryPower = hours;
        }

        protected override void SetCurrentEnergyAmount(string i_CurrentAmountStr)
        {
            if (!float.TryParse(i_CurrentAmountStr, out float amount))
            {
                throw new ArgumentException($"Invalid energy amount: {i_CurrentAmountStr}", nameof(i_CurrentAmountStr));
            }

            m_Battery.CurrentBatteryPower = amount;
        }

        protected override void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels)
        {
            string manufacturer = i_GalgalimData[(int)eGeneralDataIndicesInFile.TierModel];
            string pressureStr = i_GalgalimData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitWheelsFromDb(manufacturer, pressureStr, k_NumberOfWheels, k_MaxAirPressure, i_MyWheels);
        }
    }
}
