using static Ex03.GarageLogic.FuelVehicle;
using System;

namespace Ex03.GarageLogic
{
    internal struct ElectricVehicle
    {
        private const float k_MinBatteryPower = 0;
        private float m_CurrentBatteryPower;
        private readonly float r_MaxBatteryPower;

        public float CurrentBatteryPower
        {
            get
            {
                return m_CurrentBatteryPower;
            }

            set
            {
                if (value < k_MinBatteryPower || value > r_MaxBatteryPower)
                {
                    throw new ValueRangeException(
                        $"Battery charge must be between 0 and {r_MaxBatteryPower}.",
                        k_MinBatteryPower,
                        r_MaxBatteryPower);
                }

                m_CurrentBatteryPower = value;
            }

        }

        public float EnergyPercentage
        {
            get
            {
                return ((CurrentBatteryPower / r_MaxBatteryPower) * 100);
            }
        }

        public ElectricVehicle(float i_MaxBatteryPower)
        {
            m_CurrentBatteryPower = 0;
            r_MaxBatteryPower = i_MaxBatteryPower;
        }
    }
}