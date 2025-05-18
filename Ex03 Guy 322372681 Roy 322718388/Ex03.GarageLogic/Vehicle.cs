using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private List<Wheel> m_Wheels;

        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string EnergyLevels { get; set; }
    }
}