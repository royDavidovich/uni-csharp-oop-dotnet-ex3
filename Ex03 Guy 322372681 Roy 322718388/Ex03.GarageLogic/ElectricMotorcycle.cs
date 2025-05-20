using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        protected ElectricVehicle m_Battery;
        protected const float k_MaxFuelAmount = 3.2f;
        protected const int k_MaxAirPressure = 30;

        public ElectricMotorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_Battery = new ElectricVehicle(k_MaxFuelAmount);
        }

        //protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        //{
        //    if (float.TryParse(
        //            i_VehicleData[(int)Vehicle.eGeneralDataIndicesInFile.CurrFuelAmount],
        //            out float currentBatteryPower))
        //    {
        //        m_Battery.CurrentBatteryPower = currentBatteryPower;
        //        this.r_EnergyPercentage = m_Battery.CalculateEnergyPercentage();
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Invalid fuel amount");
        //    }

        //    if (Enum.TryParse(
        //            i_VehicleData[(int)Motorcycle.eSpecificDataIndicesInFile.PermitType],
        //            out Motorcycle.ePermitTypes permitType))
        //    {
        //        this.m_PermitType = permitType;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Invalid Permit Type");
        //    }

        //    if (int.TryParse(
        //            i_VehicleData[(int)Motorcycle.eSpecificDataIndicesInFile.EngineVolume],
        //            out int engineVolume))
        //    {
        //        this.EngineVolume = engineVolume;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Invalid Engine Volume");
        //    }
        //}

        protected override void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels)
        {
            string manufacturer = i_GalgalimData[(int)eGeneralDataIndicesInFile.TierModel];
            string pressureStr = i_GalgalimData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitWheelsFromDb(manufacturer, pressureStr, k_NumberOfWheels, k_MaxAirPressure, i_MyWheels);
        }
    }
}