using static Ex03.GarageLogic.FuelVehicle;
using System;

namespace Ex03.GarageLogic
{
    internal struct ElectricVehicle
    {
        private readonly float r_MaxBatteryPower;
        public float CurrentBatteryPower { get; set; }

        public ElectricVehicle(float i_MaxBatteryPower)
        {
            CurrentBatteryPower = 0;
            r_MaxBatteryPower = i_MaxBatteryPower;
        }

        public float CalculateEnergyPercentage()
        {
            return (CurrentBatteryPower / r_MaxBatteryPower) * 100;
        }
    }
}