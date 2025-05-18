namespace Ex03.GarageLogic
{
    internal struct FuelVehicle
    {
        public enum eGasType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public eGasType GasType { get; set; }

        public float MaxFuelLevel { get; set; }
        public float CurrentFuelLevel { get; set; }
    }
}