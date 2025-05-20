using System.Dynamic;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float k_MinAirPressure = 0;
        private float m_CurrentAirPressure;

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value < k_MinAirPressure || value > MaxAirPressure)
                {
                    throw new ValueRangeException(
                        $"Air pressure must be between {k_MinAirPressure} and {MaxAirPressure}.",
                        k_MinAirPressure,
                        MaxAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        public string Manufacturer { get; set; }

        public float MaxAirPressure { get; set; }

        public void InflateWheel(float i_AirAmountToInflate)
        {
            float newAirPressure = m_CurrentAirPressure + i_AirAmountToInflate;
            
            if (newAirPressure > MaxAirPressure)
            {
                throw new ValueRangeException(
                    $"Air pressure must be between {k_MinAirPressure} and {MaxAirPressure}.",
                    k_MinAirPressure,
                    MaxAirPressure);
            }

            m_CurrentAirPressure = newAirPressure;
        }
    }
}