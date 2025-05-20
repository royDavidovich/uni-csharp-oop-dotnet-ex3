using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        protected enum ePermitTypes
        {
            A,
            A2,
            AB,
            B2
        }

        protected enum eSpecificDataIndicesInFile
        {
            PermitType = 8,
            EngineVolume = 9
        }

        protected const int k_NumberOfWheels = 2;
        protected ePermitTypes m_PermitType;
        public int EngineVolume { get; set; }

        protected Motorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            if (Enum.TryParse(
                    i_VehicleData[(int)eSpecificDataIndicesInFile.PermitType],
                    out ePermitTypes permitType))
            {
                this.m_PermitType = permitType;
            }
            else
            {
                throw new ArgumentException($"Invalid Permit Type: {permitType}");
            }

            if (int.TryParse(
                    i_VehicleData[(int)eSpecificDataIndicesInFile.EngineVolume],
                    out int engineVolume))
            {
                this.EngineVolume = engineVolume;
            }
            else
            {
                throw new ArgumentException($"Invalid Engine Volume: {engineVolume}");
            }
        }
    }
}