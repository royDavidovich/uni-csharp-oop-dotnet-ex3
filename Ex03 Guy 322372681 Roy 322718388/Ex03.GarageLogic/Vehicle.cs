using System.Collections.Generic;
using System.Security.Permissions;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eGeneralDataIndicesInFile
        {
            VehicleType = 0,
            LicensePlate = 1,
            ModelName = 2,
            EnergyPercentage = 3,
            TierModel = 4,
            CurrAirPressure = 5,
            CurrFuelAmount = 6
        }

        protected readonly string r_LicensePlate;
        protected readonly string r_ModelName;
        protected float r_EnergyPercentage;
        private List<Wheel> m_Wheels = new List<Wheel>();

        protected Vehicle(string i_LicensePlate, string i_ModelName)
        {
            this.r_LicensePlate = i_LicensePlate;
            this.r_ModelName = i_ModelName;
        }

        public string LicensePlate
        {
            get { return r_LicensePlate; }
        }

        public void InitVehicleToGarage(string[] i_VehicleData)
        {
            InitVehicleSpecificInformation(i_VehicleData);
            InitVehicleGalgalimList(i_VehicleData, m_Wheels);
        }

        //private void initVehicleGeneralTypeInformation(string[] i_VehicleData)
        //{
        //    this.r_LicensePlate = i_VehicleData[(int)eGeneralDataIndicesInFile.LicensePlate];
        //    this.r_ModelName = i_VehicleData[(int)eGeneralDataIndicesInFile.ModelName];
        //    this.r_EnergyPercentage = i_VehicleData[(int)eGeneralDataIndicesInFile.EnergyPercentage];

        //    //this.m_Wheels = i_VehicleData[(int)eGeneralDataIndicesInFile.TierModel];
        //    //this.m_Wheels = i_VehicleData[(int)eGeneralDataIndicesInFile.CurrAirPressure];
        //    //this.m_Wheels = i_VehicleData[(int)eGeneralDataIndicesInFile.MaxAirPressure];
        //}

        protected abstract void InitVehicleSpecificInformation(string[] i_VehicleData);

        protected abstract void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels);
    }
}