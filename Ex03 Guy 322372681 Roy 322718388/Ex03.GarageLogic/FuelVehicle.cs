namespace Ex03.GarageLogic
{
    internal struct FuelVehicle
    {
        private enum eGasType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        private eGasType m_GasType;

        private float m_MaxFuelLevel;
        public float CurrentFuelLevel { get; set; }
    }
}