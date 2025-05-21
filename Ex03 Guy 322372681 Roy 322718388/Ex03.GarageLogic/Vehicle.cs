using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly List<Wheel> r_Wheels = new List<Wheel>();

        protected string OwnerName { get; set; }

        protected string OwnerPhone { get; set; }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        protected abstract int NumberOfWheels { get; }

        protected abstract float MaxAirPressure { get; }

        protected Vehicle(string i_LicensePlate, string i_ModelName)
        {
            this.r_LicensePlate = i_LicensePlate;
            this.r_ModelName = i_ModelName;
        }

        public void InitVehicleInformation(string[] i_VehicleData, List<string> i_Galgalim = null)
        {
            this.OwnerName = i_VehicleData[(int)eGeneralDataIndicesInFile.OwnerName];
            this.OwnerPhone = i_VehicleData[(int)eGeneralDataIndicesInFile.OwnerPhone];
            InitVehicleSpecificInformation(i_VehicleData);
            if(i_Galgalim != null && i_Galgalim.Any())
            {
                InitVehicleGalgalimListIndividually(i_Galgalim);
            }
            else
            {
                InitVehiclesGalgalimListCollectively(i_VehicleData);
            }
        }

        protected abstract void InitVehicleSpecificInformation(string[] i_VehicleData);

        protected void InitVehiclesGalgalimListCollectively(string[] i_VehicleData)
        {
            string manufacturer = i_VehicleData[(int)eGeneralDataIndicesInFile.TierModel];
            string currentAirPressureStr = i_VehicleData[(int)eGeneralDataIndicesInFile.CurrAirPressure];

            InitAndSetWheelsCollectively(manufacturer, currentAirPressureStr, NumberOfWheels, MaxAirPressure);
        }

        private void InitAndSetWheelsCollectively(string i_Manufacturer, string i_CurrentAirPressureStr, int i_NumberOfWheels,
                                        float i_MaxAirPressure)
        {
            r_Wheels.Clear();
            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                InitAndSetWheelsIndividually(
                    i_Manufacturer,
                    i_CurrentAirPressureStr,
                    i_NumberOfWheels,
                    i_MaxAirPressure);
            }
        }

        protected void InitVehicleGalgalimListIndividually(List<string> i_GalgalimData)
        {
            const int k_Manufacturer = 0;
            const int k_CurrentAirPressureStr = 1;

            r_Wheels.Clear();
            foreach (string line in i_GalgalimData)
            {
                string[] currentWheelInformation = line.Split(',');
                string manufacturer = currentWheelInformation[k_Manufacturer];
                string currentAirPressureStr = currentWheelInformation[k_CurrentAirPressureStr];

                InitAndSetWheelsIndividually(manufacturer, currentAirPressureStr, NumberOfWheels, MaxAirPressure);
            }
        }

        private void InitAndSetWheelsIndividually(string i_Manufacturer, string i_CurrentAirPressureStr, int i_NumberOfWheels,
                                                    float i_MaxAirPressure)
        {
            if (!float.TryParse(i_CurrentAirPressureStr, out float currentPressure))
            {
                throw new FormatException($"Invalid current air pressure value: {i_CurrentAirPressureStr}");
            }

            r_Wheels.Add(
                new Wheel
                    {
                        Manufacturer = i_Manufacturer,
                        MaxAirPressure = i_MaxAirPressure,
                        CurrentAirPressure = currentPressure
                    });
        }

        protected void InflateAllWheelsToMaxAirPressure()
        {
            if (r_Wheels.Count == 0)
            {
                throw new ArgumentException($"No wheels found for vehicle! Please init vehicle's galgalim list");
            }

            foreach(Wheel wheel in r_Wheels)
            {
                float inflateDelta = (MaxAirPressure - wheel.CurrentAirPressure);
                wheel.InflateWheel(inflateDelta);
            }
        }
    }
}