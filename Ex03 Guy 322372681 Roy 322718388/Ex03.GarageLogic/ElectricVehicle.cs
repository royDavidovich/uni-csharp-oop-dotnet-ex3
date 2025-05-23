using System;

using static Ex03.GarageLogic.FuelVehicle;

namespace Ex03.GarageLogic
{
    public interface IChargeable
    {
        void Recharge(float i_AmountToAddInMinuets);
    }

    internal class ElectricVehicle
    {
        private const float k_MinBatteryPower = 0;

        private float m_CurrentBatteryPowerInHours;
        private readonly float r_MaxBatteryPower;

        public float CurrentBatteryPowerInHours
        {
            get
            {
                return m_CurrentBatteryPowerInHours;
            }

            set
            {
                if (value < k_MinBatteryPower || value > r_MaxBatteryPower)
                {
                    throw new ValueRangeException(
                        $"Battery charge must be between {k_MinBatteryPower} and {r_MaxBatteryPower}.",
                        k_MinBatteryPower,
                        r_MaxBatteryPower);
                }

                m_CurrentBatteryPowerInHours = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return (CurrentBatteryPowerInHours / r_MaxBatteryPower) * 100;
            }
        }

        public ElectricVehicle(float i_MaxBatteryPower)
        {
            m_CurrentBatteryPowerInHours = k_MinBatteryPower;
            r_MaxBatteryPower = i_MaxBatteryPower;
        }

        public void Recharge(float i_AmountToAddInMinuets)
        {
            if (i_AmountToAddInMinuets <= 0f)
            {
                throw new ValueRangeException(
                    "Recharge amount must be a positive value.",
                    k_MinBatteryPower,
                    r_MaxBatteryPower);
            }

            if (CurrentBatteryPowerInHours >= r_MaxBatteryPower)
            {
                throw new ValueRangeException(
                    "Battery is already fully charged.",
                    k_MinBatteryPower,
                    r_MaxBatteryPower);
            }

            float amountToAddInHours = convertMinutesToHours(i_AmountToAddInMinuets);

            float newLevel = CurrentBatteryPowerInHours + amountToAddInHours;

            if (newLevel > r_MaxBatteryPower)
            {
                throw new ValueRangeException(
                    $"Cannot recharge by {amountToAddInHours}h: would exceed capacity (current {CurrentBatteryPowerInHours}h, max {r_MaxBatteryPower}h).",
                    k_MinBatteryPower,
                    r_MaxBatteryPower);
            }

            CurrentBatteryPowerInHours += amountToAddInHours;
        }

        private static float convertMinutesToHours(float i_Minutes)
        {
            float hours = i_Minutes / 60f;

            return hours;
        }
    }
}