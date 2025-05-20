using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        protected FuelVehicle m_Engine;
        protected const float k_MaxFuelAmount = 48f;
        protected const int k_MaxAirPressure = 32;
        protected const string k_GasType = "Octan95"; //where enum

        public FuelCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Engine = new FuelVehicle(k_MaxFuelAmount, k_GasType);
        }

        protected override void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr)
        {
            if (!float.TryParse(i_CurrentPercentageStr, out float energyPercentage))
            {
<<<<<<< HEAD
                m_Engine.CurrentFuelLevel = amount;
                //TODO HANDLE PERCANTAGE
            }
            else
            {
                throw new ArgumentException($"Invalid fuel amount: {amount}");
=======
                throw new ArgumentException(
                    $"Invalid energy percentage: {i_CurrentPercentageStr}",
                    i_CurrentPercentageStr);
>>>>>>> 7579dc63b1f919879e9ddab0313ba6e483ff06a1
            }

            float liters = (energyPercentage / 100f * k_MaxFuelAmount);

            m_Engine.CurrentFuelLevel = liters;
        }

        protected override void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels)
        {
            string manufacturer = i_GalgalimData[(int)eGeneralDataIndicesInFile.TierModel];
            string pressureStr = i_GalgalimData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitWheelsFromDb(manufacturer, pressureStr, k_NumberOfWheels, k_MaxAirPressure, i_MyWheels);
        }
    }
}