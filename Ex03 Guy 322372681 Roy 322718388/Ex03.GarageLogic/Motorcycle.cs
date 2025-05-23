using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle, IDetailedVehicle
    {
        protected const int k_NumberOfWheels = 2;
        private int m_EngineVolume;
        protected ePermitTypes e_PermitType;

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

        public override int NumberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        protected ePermitTypes PermitType
        {
            get
            {
                return e_PermitType;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Engine volume must be a positive integer; you entered {value}.");
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
                this.e_PermitType = permitType;
            }
            else
            {
                throw new FormatException($"Invalid Permit Type: {i_PermitTypeStr}");
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
                throw new FormatException($"Invalid Engine Volume: {i_EngineVolumeStr}");
            }
        }

        public Dictionary<string, object> GetDetails()
        {
            Dictionary<string, object> vehicleSpecificData = new Dictionary<string, object>
            {
                { "Engine Volume", EngineVolume },
                { "Permit Type", PermitType }
            };

            return vehicleSpecificData;
        }
    }
}