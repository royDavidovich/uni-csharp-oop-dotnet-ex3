using System;

namespace Ex03.GarageLogic
{
    internal struct FuelVehicle
    {
        private const float k_MinFuelLevel = 0;
        private float m_CurrentFuelLevel;
        private readonly float r_MaxFuelLevel;
        private readonly eGasType r_GasType;
        private const float k_PercentageMultiplier = 100f;
        

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

        public FuelVehicle(float i_MaxFuelLevel, string i_GasType)
        {
            CurrentFuelLevel = 0;
            r_MaxFuelLevel = i_MaxFuelLevel;
            if (Enum.TryParse(i_GasType, out eGasType parsedGasType))
            {
                r_GasType = parsedGasType;
            }
            else
            {
                throw new ArgumentException("Invalid gas type", nameof(i_GasType));
            }
        }

         public float CalculateCurrentFuelAmount(float i_Percentage)
        {
            return (i_Percentage / k_PercentageMultiplier) * r_MaxFuelLevel;
        }
    }
}