using System;

namespace Ex03.GarageLogic
{
    public interface IFillable
    {
        void Refuel(float i_AmountToAdd, string i_FuelType);
        string GetFuelType();
    }

    internal class FuelVehicle
    {
        private const float k_MinFuelLevel = 0;
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

        public FuelVehicle(float i_MaxFuelLevel, string i_FuelType)
        {
            m_CurrentFuelLevel = k_MinFuelLevel;
            r_MaxFuelLevel = i_MaxFuelLevel;
            if (Enum.TryParse(i_FuelType, out eGasType parsedGasType))
            {
                r_GasType = parsedGasType;
            }
            else
            {
                throw new FormatException("Invalid gas type");
            }
        }

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
                        $"Fuel amount must be between {k_MinFuelLevel} and {r_MaxFuelLevel}.",
                        k_MinFuelLevel,
                        r_MaxFuelLevel);
                }

                m_CurrentFuelLevel = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return (m_CurrentFuelLevel / r_MaxFuelLevel) * 100;
            }
        }

        public void Refuel(float i_AmountToAdd, string i_FuelTypeStr)
        {
            if (!Enum.TryParse(i_FuelTypeStr, out eGasType requestedFuelType))
            {
                throw new ArgumentException($"Unknown fuel type: {i_FuelTypeStr}");
            }

            if (requestedFuelType != r_GasType)
            {
                throw new ArgumentException(
                    $"Cannot fill with {requestedFuelType}; this vehicle requires {r_GasType}.");
            }

            if (i_AmountToAdd <= 0f)
            {
                throw new ValueRangeException(
                    "Refuel amount must be a positive value.",
                    k_MinFuelLevel,
                    r_MaxFuelLevel);
            }

            if (m_CurrentFuelLevel >= r_MaxFuelLevel)
            {
                throw new ValueRangeException(
                    "Tank is already full.",
                    k_MinFuelLevel,
                    r_MaxFuelLevel);
            }

            float newLevel = m_CurrentFuelLevel + i_AmountToAdd;

            if (newLevel > r_MaxFuelLevel)
            {
                throw new ValueRangeException(
                    $"Cannot refuel by {i_AmountToAdd}l: would exceed capacity (current {m_CurrentFuelLevel}l, max {r_MaxFuelLevel}l).",
                    k_MinFuelLevel,
                    r_MaxFuelLevel);
            }

            CurrentFuelLevel = newLevel;
        }
    }
}