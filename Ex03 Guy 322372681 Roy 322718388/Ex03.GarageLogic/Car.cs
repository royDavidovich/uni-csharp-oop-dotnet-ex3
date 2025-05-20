﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
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
            get { return e_Color; }
        }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
<<<<<<< HEAD
            string carColorStr = i_VehicleData[(int)eSpecificDataIndicesInFile.CarColor];
            string numberOfDoorsStr = i_VehicleData[(int)eSpecificDataIndicesInFile.NumberOfDoors];

            //SetCurrentEnergyAmount(); TODO - FIX
=======
            string energyPctStr = i_VehicleData[(int)eGeneralDataIndicesInFile.EnergyPercentage];
            string carColorStr = i_VehicleData[(int)eSpecificDataIndicesInFile.CarColor];
            string numberOfDoorsStr = i_VehicleData[(int)eSpecificDataIndicesInFile.NumberOfDoors];

            SetCurrentEnergyFromPercentage(energyPctStr);
>>>>>>> 7579dc63b1f919879e9ddab0313ba6e483ff06a1
            parseAndSetCarColor(carColorStr);
            parseAndSetNumberOfDoors(numberOfDoorsStr);
        }

        protected abstract void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr);

        private void parseAndSetCarColor(string i_CarColorStr)
        {
            if (Enum.TryParse(i_CarColorStr, out eCarColor carColor))
            {
                this.e_Color = carColor;
            }
            else
            {
<<<<<<< HEAD
                throw new ArgumentException($"Invalid Car Color: {i_CarColorStr}", nameof(i_CarColorStr)); 
=======
                throw new ArgumentException($"Invalid Car Color: {i_CarColorStr}", 
                    (i_CarColorStr));
>>>>>>> 7579dc63b1f919879e9ddab0313ba6e483ff06a1
            }
        }

        private void parseAndSetNumberOfDoors(string i_NumberOfDoorsStr)
        {
            if (int.TryParse(i_NumberOfDoorsStr, out int numDoors))
            {
                this.NumberOfDoors = numDoors;
            }
            else
            {
                throw new ArgumentException($"Invalid Number Of Doors: {i_NumberOfDoorsStr}");
            }
        }
    }
}