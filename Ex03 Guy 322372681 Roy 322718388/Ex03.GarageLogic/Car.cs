using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        protected const int k_NumberOfWheels = 5;
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
                if (value < 2 || value > 5)
                {
                    throw new ValueRangeException("Car door number must be between 2 and 5.");
                }

                m_NumberOfDoors = value;
            }
        }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            if (Enum.TryParse(
                    i_VehicleData[(int)eSpecificDataIndicesInFile.CarColor],
                    out eCarColor carColor))
            {
                this.e_Color = carColor;
            }
            else
            {
                throw new ArgumentException("Invalid Car Color");
            }

            if (int.TryParse(
                    i_VehicleData[(int)eSpecificDataIndicesInFile.NumberOfDoors],
                    out int numberOfDoors))
            {
                this.NumberOfDoors = numberOfDoors;
            }
            else
            {
                throw new ArgumentException("Invalid Number Of Doors");
            }
        }
    }
}