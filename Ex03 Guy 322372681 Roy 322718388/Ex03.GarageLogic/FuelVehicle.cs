using System;

namespace Ex03.GarageLogic
{
    internal struct FuelVehicle
    {
        private const float k_MinFuelLevel = 0;
        private const float k_PercentageMultiplier = 100f;
        private float m_CurrentFuelLevel;
        private readonly float r_MaxFuelLevel;
        private readonly eGasType r_GasType;

        public enum eGasType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public FuelVehicle(float i_MaxFuelLevel, string i_GasType)
        {
            if (!Enum.TryParse(i_GasType, out eGasType parsedGasType))
            {
                throw new ArgumentException("Invalid gas type", nameof(i_GasType));
            }

            r_MaxFuelLevel = i_MaxFuelLevel;
            m_CurrentFuelLevel = 0;
            r_GasType = parsedGasType;
        }

        public float CurrentFuelLevel
        {
            get => m_CurrentFuelLevel;
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

        public float EnergyPercentage => (m_CurrentFuelLevel / r_MaxFuelLevel) * k_PercentageMultiplier;

        public float CalculateCurrentFuelAmount(float i_Percentage)
        {
            return (i_Percentage / k_PercentageMultiplier) * r_MaxFuelLevel;
        }
    }
}