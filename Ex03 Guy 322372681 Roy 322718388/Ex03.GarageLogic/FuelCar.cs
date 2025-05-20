using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        protected FuelVehicle m_Engine;
        protected const float k_MaxFuelAmount = 5.8f;
        protected const int k_MaxAirPressure = 32;
        protected const string k_GasType = "Octan95"; //where enum

        public FuelCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        protected override void SetCurrentEnergyAmount(string i_CurrentAmountStr)
        {
            if (float.TryParse(i_CurrentAmountStr, out float amount))
            {
                m_Engine.CurrentFuelLevel = amount;
                //TODO HANDLE PERCANTAGE
            }
            else
            {
                throw new ArgumentException($"Invalid fuel amount: {amount}");
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