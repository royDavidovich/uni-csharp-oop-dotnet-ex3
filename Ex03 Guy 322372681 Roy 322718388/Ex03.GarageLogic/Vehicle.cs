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

        protected abstract void InitVehicleSpecificInformation(string[] i_VehicleData);

        protected abstract void InitVehicleGalgalimList(string[] i_GalgalimData, List<Wheel> i_MyWheels);

        protected void InitWheelsFromDb(string i_Manufacturer, string i_CurrentAirPressureStr, int i_NumberOfWheels,
                                        float i_MaxAirPressure, List<Wheel> i_Wheels)
        {
            if (!float.TryParse(i_CurrentAirPressureStr, out float currentPressure))
            {
                throw new ArgumentException($"Invalid air pressure value: '{i_CurrentAirPressureStr}'", nameof(i_CurrentAirPressureStr));
            }

            if (currentPressure < 0 || currentPressure > i_MaxAirPressure)
            {
                throw new ArgumentException($"Air pressure {currentPressure} must be between 0 and {i_MaxAirPressure}", nameof(i_CurrentAirPressureStr));
            }

            i_Wheels.Clear();

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

        public void SetIdenticalWheels(int i_NumWheels, Wheel i_Template)
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

        public void SetWheelsIndividually(List<Wheel> i_Wheels)
        {
            m_Wheels.Clear();
            m_Wheels.AddRange(i_Wheels);
        }
    }
}