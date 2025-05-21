using System;

namespace Ex03.GarageLogic
{
    internal interface IFuelable
    {
        void Refuel(float amountToAdd, string fuelType);
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

        public FuelVehicle(float i_MaxFuelLevel, string i_GasType)
        {
            m_CurrentFuelLevel = 0;
            r_MaxFuelLevel = i_MaxFuelLevel;
            if (Enum.TryParse(i_GasType, out eGasType parsedGasType))
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
                return ((m_CurrentFuelLevel / r_MaxFuelLevel) * 100);
            }
        }

        public void Refuel(float i_AmountToAdd, string i_FuelTypeStr)
        {
            if (!Enum.TryParse(i_FuelTypeStr, out eGasType requestedType))
            {
                throw new ArgumentException($"Unknown fuel type: {i_FuelTypeStr}", i_FuelTypeStr);
            }

            if (requestedType != r_GasType)
            {
                throw new ArgumentException(
                    $"Cannot fill with {requestedType}; this engine requires {r_GasType}.",
                    i_FuelTypeStr);
            }

            CurrentFuelLevel += i_AmountToAdd;
        }
    }
}