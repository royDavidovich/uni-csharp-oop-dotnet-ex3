using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private FuelVehicle m_Engine;
        private const float k_MaxFuelAmount = 5.8f;
        private const int k_MaxAirPressure = 30;
        private const string k_GasType = "Octan98";

        public FuelMotorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Engine = new FuelVehicle(k_MaxFuelAmount, k_GasType);
        }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            if (float.TryParse(
                   i_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.CurrFuelAmount],
                   out float currFuelAmount))
            {
                m_Engine.CurrentFuelLevel = currFuelAmount;
                this.r_EnergyPercentage = m_Engine.CalculateEnergyPercentage();
            }
            else
            {
                throw new ArgumentException("Invalid fuel amount");
            }

            if (Enum.TryParse(
                    i_VehicleData[(int)Motorcycle.eSpecificDataIndicesInFile.PermitType],
                    out Motorcycle.ePermitTypes permitType))
            {
                this.m_PermitType = permitType;
            }
            else
            {
                throw new ArgumentException("Invalid Permit Type");
            }

            if (int.TryParse(
                    i_VehicleData[(int)Motorcycle.eSpecificDataIndicesInFile.EngineVolume],
                    out int engineVolume))
            {
                this.EngineVolume = engineVolume;
            }
            else
            {
                throw new ArgumentException("Invalid Engine Volume");
            }
        }
    }
}