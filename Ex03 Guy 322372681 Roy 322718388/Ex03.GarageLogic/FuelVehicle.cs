using System;

namespace Ex03.GarageLogic
{
    internal struct FuelVehicle
    {
        private readonly float r_MaxFuelLevel;
        private readonly eGasType r_GasType;

        public float CurrentFuelLevel { get; set; }

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

        public float CalculateEnergyPercentage()
        {
            return (CurrentFuelLevel / r_MaxFuelLevel) * 100;
        }
    }
}