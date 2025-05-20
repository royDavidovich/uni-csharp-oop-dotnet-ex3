using System;
using System.Collections.Generic;
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

        protected string ModelName
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

        public void InitVehicleInformation(string[] i_VehicleData, List<string> i_Galgalim = null)
        {
            this.OwnerName = i_VehicleData[(int)eGeneralDataIndicesInFile.OwnerName];
            this.OwnerPhone = i_VehicleData[(int)eGeneralDataIndicesInFile.OwnerPhone];
            InitVehicleSpecificInformation(i_VehicleData);
            if(i_Galgalim == null)
            {
                InitVehiclesGalgalimList(i_VehicleData);
            }
            else
            {
                //InitVehicleGalgalimListIndividually(Galgalim, r_Wheels);
            }
        }

        protected abstract void InitVehicleSpecificInformation(string[] i_VehicleData);

        protected abstract void InitVehiclesGalgalimList(string[] i_GalgalimData);

        protected void InitAndSetIdenticalWheelsFromDB(string i_Manufacturer, string i_CurrentAirPressureStr, int i_NumberOfWheels,
                                        float i_MaxAirPressure)
        {
            if (!float.TryParse(i_CurrentAirPressureStr, out float currentPressure))
            {
                throw new FormatException($"Invalid current air pressure value: {i_CurrentAirPressureStr}");
            }

            SetIdenticalWheels(i_Manufacturer, currentPressure, i_NumberOfWheels, i_MaxAirPressure);
        }

        public void SetIdenticalWheels(string i_Manufacturer, float i_CurrentPressure, int i_NumberOfWheels,
                                       float i_MaxAirPressure)
        {
            r_Wheels.Clear();

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                r_Wheels.Add(new Wheel
                                 {
                                     Manufacturer = i_Manufacturer,
                                     MaxAirPressure = i_MaxAirPressure,
                                     CurrentAirPressure = i_CurrentPressure
                });
            }
        }

        public void SetWheelsIndividually(List<Wheel> i_Wheels)
        {
            r_Wheels.Clear();
            r_Wheels.AddRange(i_Wheels);
        }
    }
}