using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        protected FuelVehicle m_Engine;
        protected const float k_MaxFuelAmount = 5.8f;
        protected const int k_MaxAirPressure = 30;
        protected const string k_GasType = "Octan98";

        public FuelMotorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Engine = new FuelVehicle(k_MaxFuelAmount, k_GasType);
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

        protected override void InitVehiclesGalgalimList(string[] i_GalgalimData)
        {
            string manufacturer = i_GalgalimData[(int)eGeneralDataIndicesInFile.TierModel];
            string pressureStr = i_GalgalimData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitAndSetIdenticalWheelsFromDB(manufacturer, pressureStr, k_NumberOfWheels, k_MaxAirPressure);
        }
    }
}