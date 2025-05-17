namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        protected enum eCarGasType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        protected eCarGasType m_GasType;

        private float m_MaxFuelLevel;
        public float CurrentFuelLevel { get; set; }
    }
}