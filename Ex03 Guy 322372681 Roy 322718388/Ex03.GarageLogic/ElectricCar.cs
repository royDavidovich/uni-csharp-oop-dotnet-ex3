using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        protected ElectricVehicle m_Battery;
        protected const float k_MaxFuelAmount = 4.8f;
        protected const int k_MaxAirPressure = 32;

        public ElectricCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        protected override void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels)
        {
            string manufacturer = i_GalgalimData[(int)eGeneralDataIndicesInFile.TierModel];
            string pressureStr = i_GalgalimData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitWheelsFromDb(manufacturer, pressureStr, k_NumberOfWheels, k_MaxAirPressure, i_MyWheels);
        }

        protected override void SetCurrentEnergyAmount(string i_CurrentAmountStr)
        {
            if (float.TryParse(i_CurrentAmountStr, out float amount))
            {
                m_Battery.CurrentBatteryPower = amount;
                //TODO HANDLE ENERGY PERCANTAGE
            }
            else
            {
                throw new ArgumentException($"Invalid fuel amount: {amount}");
            }
        }
    }
}