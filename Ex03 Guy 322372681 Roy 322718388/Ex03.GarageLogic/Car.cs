﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle, IDetailedVehicle
    {
        protected const int k_NumberOfWheels = 5;
        protected const int k_MinNumOfDoors = 2;
        protected const int k_MaxNumOfDoors = 5;
        protected eCarColor e_Color;
        protected int m_NumberOfDoors;

        protected enum eCarColor
        {
            Yellow,
            Black,
            White,
            Silver
        }

        protected enum eSpecificDataIndicesInFile
        {
            CarColor = 8,
            NumberOfDoors = 9
        }

        protected Car(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        public override int NumberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public int NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                if (value < k_MinNumOfDoors || value > k_MaxNumOfDoors)
                {
                    throw new ValueRangeException(
                        $"Invalid number of car doors. Must be between {k_MinNumOfDoors} and {k_MaxNumOfDoors}.",
                        k_MinNumOfDoors,
                        k_MaxNumOfDoors);
                }

                m_NumberOfDoors = value;
            }
        }

        protected eCarColor Color
        {
            get
            {
                return e_Color;
            }
        }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            string energyPctStr = i_VehicleData[(int)eGeneralDataIndicesInFile.EnergyPercentage];
            string carColorStr = i_VehicleData[(int)eSpecificDataIndicesInFile.CarColor];
            string numberOfDoorsStr = i_VehicleData[(int)eSpecificDataIndicesInFile.NumberOfDoors];

            SetCurrentEnergyFromPercentage(energyPctStr);
            parseAndSetCarColor(carColorStr);
            parseAndSetNumberOfDoors(numberOfDoorsStr);
        }

        protected abstract void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr);

        private void parseAndSetCarColor(string i_CarColorStr)
        {
            if (Enum.TryParse(i_CarColorStr, out eCarColor carColor))
            {
                e_Color = carColor;
            }
            else
            {
                throw new FormatException($"Invalid Car Color: {i_CarColorStr}");
            }
        }

        private void parseAndSetNumberOfDoors(string i_NumberOfDoorsStr)
        {
            if (int.TryParse(i_NumberOfDoorsStr, out int numDoors))
            {
                NumberOfDoors = numDoors;
            }
            else
            {
                throw new FormatException($"Invalid Number Of Doors: {i_NumberOfDoorsStr}");
            }
        }

        public Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> vehicleSpecificData = new Dictionary<string, object>
            {
                { "Doors", NumberOfDoors },
                { "Color", Color }
            };

            return vehicleSpecificData;
        }
    }
}