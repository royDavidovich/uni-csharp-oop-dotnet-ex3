using System;
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
            OwnerName = 6,
            OwnerPhone = 7
        }

        protected readonly string r_LicensePlate;
        protected readonly string r_ModelName;

        public float EnergyPercentage { get; set; }

        private List<Wheel> m_Wheels = new List<Wheel>();

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        public string ModelName
        {
            get
            {
                return r_ModelName;
            }
        }

        protected Vehicle(string i_LicensePlate, string i_ModelName)
        {
            this.r_LicensePlate = i_LicensePlate;
            this.r_ModelName = i_ModelName;
        }

        public void InitVehicleInformation(string[] i_VehicleData)
        {
            InitVehicleSpecificInformation(i_VehicleData);
            InitVehicleGalgalimList(i_VehicleData, m_Wheels);
        }

        protected abstract void InitVehicleSpecificInformation(string[] i_VehicleData);

        protected abstract void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels);

        protected void InitWheelsFromDb(string i_Manufacturer, string i_CurrentAirPressureStr, int i_NumberOfWheels,
                                        float i_MaxAirPressure, List<Wheel> i_Wheels)
        {
            if (!float.TryParse(i_CurrentAirPressureStr, out float currentPressure))
            {
                throw new ArgumentException(
                    $"Invalid air pressure value: '{i_CurrentAirPressureStr}'",
                    i_CurrentAirPressureStr);
            }

            if (currentPressure < 0 || currentPressure > i_MaxAirPressure)
            {
                throw new ArgumentException(
                    $"Air pressure {currentPressure} must be between 0 and {i_MaxAirPressure}",
                    i_CurrentAirPressureStr);
            }

            i_Wheels.Clear();  //why clear

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                i_Wheels.Add(new Wheel
                                 {
                                     Manufacturer = i_Manufacturer,
                                     MaxAirPressure = i_MaxAirPressure,
                                     CurrentAirPressure = currentPressure
                                 });
            }
        }

        public void SetIdenticalWheels(int i_NumWheels, Wheel i_Template)  //TODO: set exception
        {
            m_Wheels.Clear();

            for (int i = 0; i < i_NumWheels; i++)
            {
                m_Wheels.Add(new Wheel
                                 {
                                     Manufacturer = i_Template.Manufacturer,
                                     MaxAirPressure = i_Template.MaxAirPressure,
                                     CurrentAirPressure = i_Template.CurrentAirPressure
                                 });
            }
        }

        public void SetWheelsIndividually(List<Wheel> i_Wheels) //explanation pls
        {
            m_Wheels.Clear();
            m_Wheels.AddRange(i_Wheels);
        }
    }
}