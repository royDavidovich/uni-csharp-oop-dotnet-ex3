using System;

namespace Ex03.GarageLogic
{
    internal struct FuelVehicle
    {
        private const float k_MinFuelLevel = 0;
        private readonly float r_MaxFuelLevel;
        private float m_CurrentFuelLevel;
        private readonly eGasType r_GasType;

        public float CurrentFuelLevel
        {
            get
            {
                return m_CurrentFuelLevel;
            }
            set
            {
                if (value < k_MinFuelLevel || value > r_MaxFuelLevel)
                {
                    throw new ValueRangeException(
                        $"Fuel amount must be between 0 and {r_MaxFuelLevel}.",
                        k_MinFuelLevel,
                        r_MaxFuelLevel);
                }

                m_CurrentFuelLevel = value;
            }
        }

        public enum eGasType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public FuelVehicle(float i_MaxFuelLevel, string i_GasType) //copilot fix for building only!
        {
            r_MaxFuelLevel = i_MaxFuelLevel; // Assign readonly field first
            m_CurrentFuelLevel = 0; // Initialize non-readonly field
            if (Enum.TryParse(i_GasType, out eGasType parsedGasType))
            {
                r_GasType = parsedGasType; // Assign readonly field
            }
            else
            {
                throw new ArgumentException("Invalid gas type", nameof(i_GasType));
            }
        }

        public float CalculateEnergyPercentage()
        {
            return (CurrentFuelLevel / r_MaxFuelLevel) * 100;
        }
    }
}