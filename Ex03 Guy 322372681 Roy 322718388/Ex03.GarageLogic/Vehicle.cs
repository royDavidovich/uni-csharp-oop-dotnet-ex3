using System.Collections.Generic;
using System.Security.Permissions;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected enum eGeneralDataIndicesInFile
        {
            VehicleType = 0,
            LicensePlate = 1, 
            ModelName = 2,
            EnergyPercentage = 3,
            TierModel = 4, 
            CurrAirPressure = 5,
            MaxAirPressure = 6, 
            FuelType = 7, 
            CurrFuelAmount = 8
        }

        private List<Wheel> m_Wheels;

        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string EnergyLevels { get; set; }
        public void InitVehicleGeneralTypeInformation(string[] i_VehicleData)
        {
            this.RegistrationNumber = i_VehicleData[(int)eGeneralDataIndicesInFile.LicensePlate];
            this.Model = i_VehicleData[(int)eGeneralDataIndicesInFile.ModelName];
            this.EnergyLevels = i_VehicleData[(int)eGeneralDataIndicesInFile.EnergyPercentage];
            //this.m_Wheels = i_VehicleData[(int)eGeneralDataIndicesInFile.TierModel];
            //this.m_Wheels = i_VehicleData[(int)eGeneralDataIndicesInFile.CurrAirPressure];
            //this.m_Wheels = i_VehicleData[(int)eGeneralDataIndicesInFile.MaxAirPressure];
        }

        public abstract void InitVehicleSpecificInformation(string[] i_VehicleData);
        public abstract void InitVehicleGalgalimList(string[] i_GalgalimData);
    }
}