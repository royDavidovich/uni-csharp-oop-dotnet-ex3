using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        protected const int k_NumberOfWheels = 2;
        private int m_EngineVolume;
        protected ePermitTypes m_PermitType;

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

        protected Motorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(
                        $"Engine volume must be a positive integer; got {value}.");
                }

                m_EngineVolume = value;
            }
        }

        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            string energyPctStr = i_VehicleData[(int)eGeneralDataIndicesInFile.EnergyPercentage];
            string permitTypeStr = i_VehicleData[(int)eSpecificDataIndicesInFile.PermitType];
            string engineVolumeStr = i_VehicleData[(int)eSpecificDataIndicesInFile.EngineVolume];

            SetCurrentEnergyFromPercentage(energyPctStr);
            parseAndSetPermitType(permitTypeStr);
            parseAndSetEngineVolume(engineVolumeStr);
        }

        protected abstract void SetCurrentEnergyFromPercentage(string i_CurrentPercentageStr);

        private void parseAndSetPermitType(string i_PermitTypeStr)
        {
            if (Enum.TryParse(i_PermitTypeStr, out ePermitTypes permitType))
            {
                this.m_PermitType = permitType;
            }
            else
            {
                throw new ArgumentException($"Invalid Permit Type: {permitType}");
            }
        }

        private void parseAndSetEngineVolume(string i_EngineVolumeStr)
        {
            if (int.TryParse(i_EngineVolumeStr, out int engineVolume))
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