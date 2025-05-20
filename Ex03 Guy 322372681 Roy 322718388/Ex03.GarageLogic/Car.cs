using System;
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
            CarColor = 7,
            NumberOfDoors = 8
        }

        public Car(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        protected eCarColor Color
        {
            get { return e_Color; }
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

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            string carColorStr = i_VehicleData[(int)eSpecificDataIndicesInFile.CarColor];
            string numberOfDoorsStr = i_VehicleData[(int)eSpecificDataIndicesInFile.NumberOfDoors];

            parseAndSetCarColor(carColorStr);
            parseAndSetNumberOfDoors(numberOfDoorsStr);
        }

        private void parseAndSetCarColor(string i_CarColorStr)
        {
            if (Enum.TryParse(i_CarColorStr, out eCarColor carColor))
            {
                this.e_Color = carColor;
            }
            else
            {
                throw new ArgumentException($"Invalid Car Color: {i_CarColorStr}", nameof(i_CarColorStr));
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
                throw new ArgumentException($"Invalid Number Of Doors: {i_NumberOfDoorsStr}", nameof(i_NumberOfDoorsStr));
            }
        }
    }
}